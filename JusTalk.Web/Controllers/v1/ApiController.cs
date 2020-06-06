using JusTalk.Web.Contracts.v1.Responses;
using Microsoft.AspNetCore.Mvc;

namespace JusTalk.Web.Controllers.v1
{
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected virtual OkObjectResult OkWithMessage(string message) =>
            Ok(new ApiOkResponse(message));

        protected virtual OkObjectResult OkWithResult(object result) =>
            Ok(new ApiOkResponse(result));
    }
}