using System;
using JusTalk.DomainModel.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace JusTalk.Web.Installers
{
    public class JwtInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var authConfig = configuration.GetSection("AuthConfig");
            services.Configure<JwtAuthOptions>(authConfig);
            
            var authOptions = authConfig.Get<JwtAuthOptions>();
            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
            
                    opt.SaveToken = false;
            
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = authOptions.SymmetricSecurityKey,
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.JwtIssuer,
                        ValidateAudience = true,
                        ValidAudience = authOptions.JwtAudience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
    }
}