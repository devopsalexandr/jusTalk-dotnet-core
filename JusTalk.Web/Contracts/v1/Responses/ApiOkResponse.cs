using System.Net;

namespace JusTalk.Web.Contracts.v1.Responses
{
    public class ApiOkResponse : ApiResponse
    {
        public object Result { get; }

        public ApiOkResponse(string message = null) : base(HttpStatusCode.OK, message)
        {
        }

        public ApiOkResponse(object result, string message = null) : base(HttpStatusCode.OK, message)
        {
            Result = result;
        }
    }
}