namespace ChatSystem.MessageHistoryAPI
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using System.Collections.Generic;

    public static class DependencyInjection
    {
        private const string AppName = "Chat System";
        private const string Authorisation = "Auhorisation";
        private const string AuthorisationType = "Basic";
        private const string Version = "v1";

        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(Version, new OpenApiInfo
                {
                    Version = Version,
                    Title = AppName,
                    Description = "Simple chat system."
                });

                x.AddSecurityDefinition(AuthorisationType, new OpenApiSecurityScheme
                {
                    Name = Authorisation, 
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                    {
                        Scheme = AuthorisationType,
                        Name = AuthorisationType,
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = AuthorisationType
                        }
                    },
                    new List<string>()
                    }
                });
            });

            return services;
        }
    }
}
