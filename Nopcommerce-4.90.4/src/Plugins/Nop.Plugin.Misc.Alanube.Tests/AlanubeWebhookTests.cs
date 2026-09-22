using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nop.Plugin.Misc.Alanube.Api.Webhooks;
using Nop.Plugin.Misc.Alanube.Controllers;
using Nop.Plugin.Misc.Alanube.Domain;
using Nop.Plugin.Misc.Alanube.Services;
using Nop.Services.Logging;
using NUnit.Framework;

namespace Nop.Plugin.Misc.Alanube.Tests;

[TestFixture]
public sealed class AlanubeWebhookTests
{
    [TestCase(null)]
    [TestCase("wrong")]
    public async Task MissingOrIncorrectHeaderIsRejected(string suppliedSecret)
    {
        var controller = CreateController("correct", suppliedSecret, out var service);
        Assert.That(await controller.Documents(new() { Id = "doc" }, CancellationToken.None), Is.TypeOf<UnauthorizedResult>());
        service.Verify(x => x.ProcessEmissionFinishedAsync(It.IsAny<AlanubeDocumentWebhookDto>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task UnconfiguredSecretIsRejected()
    {
        var controller = CreateController(null, "anything", out _);
        Assert.That(await controller.Documents(new() { Id = "doc" }, CancellationToken.None), Is.TypeOf<UnauthorizedResult>());
    }

    [Test]
    public async Task DisabledWebhookIsRejectedEvenWhenTheSecretIsValid()
    {
        var controller = CreateController("correct", "correct", out var service);
        Assert.That(await controller.Documents(new() { Id = "doc" }, CancellationToken.None), Is.TypeOf<UnauthorizedResult>());
        service.Verify(x => x.ProcessEmissionFinishedAsync(It.IsAny<AlanubeDocumentWebhookDto>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task EnabledWebhookWithAValidSecretIsProcessed()
    {
        var service = new Mock<IAlanubeWebhookService>();
        service.Setup(x => x.ProcessEmissionFinishedAsync(It.IsAny<AlanubeDocumentWebhookDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AlanubeWebhookResult { Succeeded = true, DocumentFound = true });
        var logger = new Mock<ILogger>();
        var controller = new AlanubeWebhookController(new AlanubeSettings { Enabled = true, EnableWebhook = true, WebhookSecret = "correct" }, service.Object, logger.Object)
        { ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() } };
        controller.Request.Headers[AlanubeDefaults.WebhookHeaderName] = "correct";

        Assert.That(await controller.Documents(new() { Id = "doc" }, CancellationToken.None), Is.TypeOf<OkObjectResult>());
        service.Verify(x => x.ProcessEmissionFinishedAsync(It.IsAny<AlanubeDocumentWebhookDto>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task PayloadWithoutIdIsRejected()
    {
        var fixture = CreateService();
        var result = await fixture.Service.ProcessEmissionFinishedAsync(new());
        Assert.That(result.Succeeded, Is.False);
    }

    [Test]
    public async Task UnknownDocumentDoesNotCreateAnything()
    {
        var fixture = CreateService();
        fixture.Documents.Setup(x => x.GetByAlanubeIdAsync("missing")).ReturnsAsync((AlanubeDocument)null);
        var result = await fixture.Service.ProcessEmissionFinishedAsync(new() { Id = "missing" });
        Assert.That(result.DocumentFound, Is.False);
        fixture.Documents.Verify(x => x.InsertAsync(It.IsAny<AlanubeDocument>()), Times.Never);
    }

    [Test]
    public async Task ValidPayloadUpdatesDocument()
    {
        var fixture = CreateServiceWithDocument(ElectronicDocumentStatus.Processing);
        await fixture.Service.ProcessEmissionFinishedAsync(Payload("DGI_AUTHORIZED"));
        Assert.That(fixture.Document.DocumentStatus, Is.EqualTo(ElectronicDocumentStatus.Authorized));
        Assert.That(fixture.Document.DocumentNumber, Is.EqualTo("FE-1"));
        fixture.Logs.Verify(x => x.InsertAsync(It.IsAny<AlanubeDocumentLog>()), Times.Once);
    }

    [Test]
    public async Task RepeatedPayloadKeepsSameFinalState()
    {
        var fixture = CreateServiceWithDocument(ElectronicDocumentStatus.Processing);
        var payload = Payload("DGI_AUTHORIZED");
        await fixture.Service.ProcessEmissionFinishedAsync(payload);
        await fixture.Service.ProcessEmissionFinishedAsync(payload);
        Assert.That(fixture.Document.DocumentStatus, Is.EqualTo(ElectronicDocumentStatus.Authorized));
        Assert.That(fixture.Document.RetryCount, Is.Zero);
    }

    [Test]
    public async Task AuthorizedDocumentDoesNotRegressToProcessing()
    {
        var fixture = CreateServiceWithDocument(ElectronicDocumentStatus.Authorized);
        await fixture.Service.ProcessEmissionFinishedAsync(Payload("REGISTERED"));
        Assert.That(fixture.Document.DocumentStatus, Is.EqualTo(ElectronicDocumentStatus.Authorized));
    }

    [Test]
    public async Task DgiRejectedMapsToRejected()
    {
        var fixture = CreateServiceWithDocument(ElectronicDocumentStatus.Processing);
        await fixture.Service.ProcessEmissionFinishedAsync(Payload("DGI_REJECTED"));
        Assert.That(fixture.Document.DocumentStatus, Is.EqualTo(ElectronicDocumentStatus.Rejected));
    }

    [Test]
    public async Task ErrorPayloadIsStoredWithoutChangingRetryCount()
    {
        var fixture = CreateServiceWithDocument(ElectronicDocumentStatus.Processing);
        using var json = JsonDocument.Parse("{\"message\":\"rejected\"}");
        var payload = Payload("DGI_REJECTED");
        payload.Error = json.RootElement.Clone();
        await fixture.Service.ProcessEmissionFinishedAsync(payload);
        Assert.That(fixture.Document.ErrorCode, Is.EqualTo("ALANUBE_WEBHOOK_ERROR"));
        Assert.That(fixture.Document.ErrorMessage, Is.Not.Empty);
        Assert.That(fixture.Document.ErrorMessage, Does.Not.Contain("rejected"));
        Assert.That(fixture.Document.RetryCount, Is.Zero);
    }

    private static AlanubeWebhookController CreateController(string configuredSecret, string suppliedSecret, out Mock<IAlanubeWebhookService> service)
    {
        service = new Mock<IAlanubeWebhookService>();
        var logger = new Mock<ILogger>();
        logger.Setup(x => x.WarningAsync(It.IsAny<string>(), null, null)).Returns(Task.CompletedTask);
        var controller = new AlanubeWebhookController(new AlanubeSettings { WebhookSecret = configuredSecret }, service.Object, logger.Object)
        { ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() } };
        if (suppliedSecret != null)
            controller.Request.Headers[AlanubeDefaults.WebhookHeaderName] = suppliedSecret;
        return controller;
    }

    private static ServiceFixture CreateServiceWithDocument(ElectronicDocumentStatus status)
    {
        var fixture = CreateService();
        fixture.Document = new AlanubeDocument { Id = 1, AlanubeId = "doc", DocumentStatus = status };
        fixture.Documents.Setup(x => x.GetByAlanubeIdAsync("doc")).ReturnsAsync(fixture.Document);
        return fixture;
    }

    private static ServiceFixture CreateService()
    {
        var documents = new Mock<IAlanubeDocumentService>();
        var logs = new Mock<IAlanubeDocumentLogService>();
        var logger = new Mock<ILogger>();
        logger.Setup(x => x.WarningAsync(It.IsAny<string>(), null, null)).Returns(Task.CompletedTask);
        return new ServiceFixture(documents, logs, new AlanubeWebhookService(documents.Object, logs.Object, logger.Object));
    }

    private static AlanubeDocumentWebhookDto Payload(string legalStatus) => new()
    {
        Type = "documents.emissionFinished", Id = "doc", Status = "FINISHED", LegalStatus = legalStatus,
        DocumentNumber = "FE-1", StampDate = DateTimeOffset.UtcNow, SignatureDate = DateTimeOffset.UtcNow
    };

    private sealed class ServiceFixture(Mock<IAlanubeDocumentService> documents, Mock<IAlanubeDocumentLogService> logs, AlanubeWebhookService service)
    {
        public Mock<IAlanubeDocumentService> Documents { get; } = documents;
        public Mock<IAlanubeDocumentLogService> Logs { get; } = logs;
        public AlanubeWebhookService Service { get; } = service;
        public AlanubeDocument Document { get; set; }
    }
}
