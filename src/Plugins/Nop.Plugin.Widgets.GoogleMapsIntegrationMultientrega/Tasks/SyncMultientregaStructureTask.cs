using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Services;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Tasks;
using System;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Tasks
{
    /// <summary>
    /// Represents an implementation of <see cref="IScheduleTask"/> for Multientrega's territorial structure sync.
    /// </summary>
    public sealed class SyncMultientregaStructureTask : IScheduleTask
    {
        private readonly ILogger _logger;
        private readonly IMultientregaAddressService _multientregaAddressService;
        private readonly INotificationService _notificationService;

        /// <summary>
        /// Initializea a new instance of <see cref="SyncMultientregaStructureTask"/>.
        /// </summary>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="multientregaAddressService">An implementation of <see cref="IMultientregaAddressService"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        public SyncMultientregaStructureTask(
            ILogger logger,
            IMultientregaAddressService multientregaAddressService,
            INotificationService notificationService)
        {
            _logger = logger;
            _multientregaAddressService = multientregaAddressService;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Executes the Multientrega's territorial structure sync schedule task.
        /// </summary>
        public void Execute()
        {
            try
            {
                _multientregaAddressService.RegisterMultientregaTerritorialStructure();
            }
            catch (Exception e)
            {
                _logger.Error($"An error has occurred while syncing Multientrega's structure. Error message: {e.Message}", e);
                _notificationService.ErrorNotification($"An error has occurred while syncing Multientrega's structure: {e.Message}");
            }
        }
    }
}
