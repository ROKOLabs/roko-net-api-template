namespace Roko.Template.Presentation.Api.Internal.Swagger
{
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System.Linq;
    using System.Text.Json;

    /// <summary>
    /// Represents the Swagger/Swashbuckle operation filter used to document the implicit API version parameter.
    /// </summary>
    /// <remarks>
    /// <see cref="IOperationFilter"/> is only required due to bugs in <see cref="SwaggerGenerator"/>.
    /// Once they are fixed and published, this class can be removed.
    /// </remarks>
    public class SwaggerDefaultValues : IOperationFilter
    {
        /// <summary>
        /// Applies the filter to the specified operation using the given context.
        /// </summary>
        /// <param name="operation">The operation to apply the filter to.</param>
        /// <param name="context">The current operation filter context.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            ApiDescription apiDescription = context.ApiDescription;

            operation.Deprecated |= apiDescription.IsDeprecated();

            // References: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1752#issue=66991077
            foreach (ApiResponseType responseType in context.ApiDescription.SupportedResponseTypes)
            {
                var responseKey = responseType.IsDefaultResponse ? "default" : responseType.StatusCode.ToString();

                OpenApiResponse response = operation.Responses[responseKey];

                foreach (var contentType in response.Content.Keys)
                {
                    if (responseType.ApiResponseFormats.All(x => x.MediaType != contentType))
                    {
                        response.Content.Remove(contentType);
                    }
                }
            }

            if (operation.Parameters is null)
            {
                return;
            }

            /* References:
             * https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/412
             * https://github.com/domaindrivendev/Swashbuckle.AspNetCore/pull/413
             */

            foreach (var parameter in operation.Parameters)
            {
                ApiParameterDescription description = apiDescription
                    .ParameterDescriptions
                    .First(parameterDescription =>
                        parameterDescription.Name == parameter.Name);

                parameter.Description ??= description.ModelMetadata?.Description;

                if (parameter.Schema is null &&
                    description.DefaultValue is not null)
                {
                    var json = JsonSerializer.Serialize(description.DefaultValue, description.ModelMetadata!.ModelType);
                    parameter.Schema!.Default = OpenApiAnyFactory.CreateFromJson(json);
                }

                parameter.Required |= description.IsRequired;
            }
        }
    }
}
