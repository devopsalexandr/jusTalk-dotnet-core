using System;
using JusTalk.DomainModel;
using Microsoft.AspNetCore.Http;

namespace JusTalk.Web
{
    public class SecurityService : ISecurityService
    {
        private readonly HttpContext _httpContext;

        public SecurityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor != null ? httpContextAccessor.HttpContext : throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string GetUserId()
        {
            return _httpContext.GetAuthUserId();
        }
    }
}