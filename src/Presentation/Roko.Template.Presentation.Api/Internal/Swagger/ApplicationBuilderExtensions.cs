namespace Roko.Template.Presentation.Api.Internal.Swagger
{
    using Asp.Versioning.ApiExplorer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Swashbuckle.AspNetCore.SwaggerUI;
    using System;

    /// <summary>
    /// Exstension class for <see cref="IApplicationBuilder"/>.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder builder, Action<SwaggerUIOptions>? swaggerOptions = null)
        {
            IApiVersionDescriptionProvider provider = builder.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            swaggerOptions ??= options =>
            {
                options.DisplayOperationId();
                options.DisplayRequestDuration();

                foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            };

            builder
                .UseSwagger()
                .UseSwaggerUI(swaggerOptions);

            return builder;
        }
    }
}
