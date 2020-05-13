using JusTalk.DAL;
using JusTalk.Web.Installers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JusTalk.Web
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(opt =>
                opt.UseMySql(configuration.GetConnectionString("DBConnectionString")));
        }
    }
}