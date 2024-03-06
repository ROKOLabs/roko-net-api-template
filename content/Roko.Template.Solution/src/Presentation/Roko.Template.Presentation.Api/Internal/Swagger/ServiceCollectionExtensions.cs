namespace Roko.Template.Presentation.Api.Internal.Swagger
{
    using Asp.Versioning;
    using Asp.Versioning.ApiExplorer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.Filters;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Extension methods for registering swagger configuration.
    /// </summary>
    public static class ServiceCollecionExtensions
    {
        private const string Bearer = "Bearer";

        public static IServiceCollection AddSwaggerConfiguration(
            this IServiceCollection services,
            Action<ApiVersioningOptions>? versioningOptions = null,
            Action<ApiExplorerOptions>? explorerOptions = null,
            Action<SwaggerGenOptions>? swaggerOptions = null,
            bool useNewtonsoftSerialization = true,
            params Assembly[]? swaggerExampleAssemblies)
        {
            versioningOptions ??= options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            };

            explorerOptions ??= options =>
            {
                // Formats version as "'v'[major][.minor][-status]".
                options.GroupNameFormat = "'v'VV";
                options.SubstituteApiVersionInUrl = true;
            };

            swaggerOptions ??= options =>
            {
                options.EnableAnnotations();
                options.ExampleFilters();
                options.OperationFilter<SwaggerDefaultValues>();
                options.AddSecurityDefinition(name: Bearer, securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = Bearer,
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = Bearer
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                foreach (var xmlFile in Directory.GetFiles(AppContext.BaseDirectory, "*.xml"))
                {
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath, true);
                }
            };

            services
                .AddApiVersioning(versioningOptions)
                .AddApiExplorer(explorerOptions);

            services
                .AddSwaggerExamplesFromAssemblies(swaggerExampleAssemblies)
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
                .AddSwaggerGen(swaggerOptions);

            if (useNewtonsoftSerialization)
            {
                services.AddSwaggerGenNewtonsoftSupport();
            }

            return services;
        }
    }
}
