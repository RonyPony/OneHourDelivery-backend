using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Attributes;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using System;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Controllers
{
    /// <summary>
    /// Responsible for interactions with payments information entity.
    /// </summary>
    [Route("api/payments")]
    [ApiController]
    public sealed class PaymentController : Controller
    {
        #region Ctor

        /// <summary>
        /// Initializes an instance of <see cref="PaymentController"/>
        /// </summary>
        public PaymentController()
        {
        }

        #endregion

        /// <summary>
        /// Generates a payment token given credit card info.
        /// </summary>
        /// <param name="vendorId">vendor id</param>
        /// <returns>An instance of <see cref="GetStoreProductsByVendorId"/></returns>
        [HttpPost("tokens"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Exposes the Get GenerateToken functionality", typeof(string))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes the Get GenerateToken functionality", typeof(ErrorMessage))]
        public IActionResult GenerateToken([FromQuery] int creditCardId)
        {
            return Ok(

                     "aksRv" + DateTime.Now.Ticks.ToString()
                );
        }

        /// <summary>
        /// Registers a credit card info
        /// </summary>
        /// <param name="request">Register credit card request</param>
        /// <returns> OK </returns>
        [HttpPost("credit-cards"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes the Get RegisterCreditCard functionality", typeof(ErrorMessage))]
        public IActionResult RegisterCreditCard()
        {
            return Ok();
        }
    }

    public class RegisterCreditCardRequest
    {
        public int CustomerId { get; set; }

        public string CreditCard { get; set; }

        public int ExpirationYear { get; set; }

        public int ExpirationMonth { get; set; }

        public int Cvv { get; set; }

    }
}
