namespace Roko.Template.Presentation.Api
{
    using Roko.Template.Blocks.Common.Exceptions;
    using Roko.Template.Presentation.Api.Internal.Mvc;
    using Roko.Template.Presentation.Api.Internal.Swagger;
    using Hellang.Middleware.ProblemDetails;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Linq;
    using ProblemDetailsOptions = Hellang.Middleware.ProblemDetails.ProblemDetailsOptions;

    public static class Module
    {
        public static IServiceCollection AddPresentationConfiguration(this IServiceCollection services, IHostEnvironment environment)
        {
            void RouteOptions(RouteOptions options) => options.LowercaseUrls = true;

            void ProblemDetailsOptions(ProblemDetailsOptions options) => SetProblemDetailsOptions(options, environment);

            void NewtonsoftOptions(MvcNewtonsoftJsonOptions options)
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }

            services
                .AddRouting(RouteOptions)
                .AddProblemDetails(ProblemDetailsOptions)
                .AddControllers()
                .EnableInternalControllers()
                .AddNewtonsoftJson(NewtonsoftOptions);

            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue; // if don't set default value is: 128 MB
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });

            services.AddSwaggerConfiguration();

            return services;
        }

        private static IMvcBuilder EnableInternalControllers(this IMvcBuilder builder)
        {
            builder.ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

            return builder;
        }

        private static void SetProblemDetailsOptions(ProblemDetailsOptions options, IHostEnvironment env)
        {
            Type[] knownExceptionTypes = [typeof(ServiceValidationException), typeof(ServiceAuthorizationException)];

            options.IncludeExceptionDetails = (_, exception) =>
                env.IsDevelopment() &&
                !knownExceptionTypes.Contains(exception.GetType());

            options.Map<ServiceValidationException>(exception =>
                new ValidationProblemDetails(exception.Errors)
                {
                    Title = exception.Title,
                    Detail = exception.Detail,
                    Status = StatusCodes.Status400BadRequest
                });

            options.Map<ServiceAuthorizationException>(exception =>
                new StatusCodeProblemDetails(StatusCodes.Status401Unauthorized));

            options.Map<ServiceResourceNotFoundException>(exception =>
                new StatusCodeProblemDetails(StatusCodes.Status404NotFound));
        }
    }
}
