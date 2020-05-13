using System;
using System.Linq;
using JusTalk.Web.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JusTalk.Web
{
    public static class InstallerExtension
    {
        public static void InstallServicesInAssembly(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes
                .Where(x => typeof(IInstaller).IsAssignableFrom(x) && x.HasDefaultConstructor() && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList();
            
            installers.ForEach(installer => installer.InstallServices(serviceCollection, configuration));
        }
    }
}