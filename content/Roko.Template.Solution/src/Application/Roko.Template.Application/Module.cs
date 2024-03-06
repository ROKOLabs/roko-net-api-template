namespace Roko.Template.Application
{
    using Roko.Template.Application.Internal.Behaviors;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class Module
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddApplicationConfiguration(Assembly.GetExecutingAssembly());

            return services;
        }
        public static IServiceCollection AddApplicationConfiguration(
            this IServiceCollection services,
            params Assembly[] assemblies)
        {
            services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assemblies);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            return services;
        }
    }
}
