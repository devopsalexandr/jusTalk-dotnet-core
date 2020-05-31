using System.Threading.Tasks;
using JusTalk.Web.Contracts.v1;
using JusTalk.Web.Controllers.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JusTalk.Web.Controllers.v1
{
    public class UserController : ApiController
    {
        [HttpGet(ApiRoutes.Profile.Show)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Show([FromRoute] int id)
        {
            return Ok();
        }
    }
}