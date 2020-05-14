using System;
using System.Threading.Tasks;
using JusTalk.Web.Contracts.v1.Requests.Identity;
using JusTalk.Web.Controllers.v1;
using Microsoft.AspNetCore.Mvc;

namespace JusTalk.Web.Controllers
{
    public class IdentityController : ApiController
    {
        public async Task<IActionResult> Login([FromBody] PhoneNumberRequest phoneNumberRequest)
        {
            if (phoneNumberRequest == null) throw new ArgumentNullException(nameof(phoneNumberRequest));

            return Ok();
        }

        public async Task<IActionResult> ConfirmAuth([FromBody] ConfirmPhoneRequest confirmPhoneRequest)
        {
            if (confirmPhoneRequest == null) throw new ArgumentNullException(nameof(confirmPhoneRequest));

            return Ok();
        }
    }
}