using JusTalk.Web.Installers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JusTalk.Web
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    // options.InvalidModelStateResponseFactory = actionContext => 
                    //     new UnprocessableEntityObjectResult(new ErrorResponse(actionContext.ModelState.ToErrorList()));
                    options.SuppressMapClientErrors = true; // disable 404 error details
                }).AddNewtonsoftJson();
        }
    }
}