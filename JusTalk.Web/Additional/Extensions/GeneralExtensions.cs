using System.Linq;
using Microsoft.AspNetCore.Http;

namespace JusTalk.Web
{
    public static class GeneralExtensions
    {
        public static string GetAuthUserId(this HttpContext httpContext)
        {
            return httpContext.User == null 
                ? string.Empty 
                : httpContext.User.Claims.Single(x => x.Type == "id").Value;
        }
    }
}