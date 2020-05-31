using System.Threading.Tasks;
using JusTalk.Web.Contracts.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JusTalk.Web.Controllers.v1
{
    public class ProfileController : ApiController
    {
        public ProfileController()
        {
        }

        [HttpGet(ApiRoutes.Profile.Index)]
        public async Task<IActionResult> Index()
        {
            return Ok();
        }

        [HttpGet(ApiRoutes.Profile.Show)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Show([FromRoute] int id)
        {
            return Ok();
        }

        [HttpPut(ApiRoutes.Profile.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateAuthUserRequest request)
        {
            return Ok();
        }
    }
}