using System;
using System.Threading.Tasks;
using JusTalk.DomainModel;
using JusTalk.Web.Contracts.v1;
using JusTalk.Web.Contracts.v1.Requests.Identity;
using JusTalk.Web.Controllers.v1;
using Microsoft.AspNetCore.Mvc;

namespace JusTalk.Web.Controllers
{
    public class IdentityController : ApiController
    {
        private IAuthService _authService;
        
        public IdentityController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] PhoneNumberRequest phoneNumberRequest)
        {
            if (phoneNumberRequest == null) throw new ArgumentNullException(nameof(phoneNumberRequest));

            var phoneNumber = phoneNumberRequest.PhoneNumber;

            var authResult = await _authService.GetVerificationCodeAsync(phoneNumber);

            if (!authResult.Succeeded)
                return BadRequest();

            var confirmationCode = authResult.ConfirmationCode;
            
            // _smsService.sendSmsAsync(phoneNumber, confirmationCode);

            return Ok("code send to email " + confirmationCode);
        }

        [HttpPost(ApiRoutes.Identity.Confirm)]
        public async Task<IActionResult> ConfirmAuth([FromBody] ConfirmPhoneRequest confirmPhoneRequest)
        {
            if (confirmPhoneRequest == null) throw new ArgumentNullException(nameof(confirmPhoneRequest));

            var phoneNumber = confirmPhoneRequest.PhoneNumber;
            var codeVerification = confirmPhoneRequest.CodeVerification;
            
            // var authenticationResult = await _authService.GetAccessTokenAsync(phoneNumber, codeVerification);

            return Ok();
        }
    }
}