using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JusTalk.Web.Installers
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}