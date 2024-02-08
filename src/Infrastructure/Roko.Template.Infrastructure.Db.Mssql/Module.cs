namespace Roko.Template.Infrastructure.Db.Mssql
{
    using Roko.Template.Application.Contracts;
    using Roko.Template.Infrastructure.Db.Mssql.Internal;
    using Roko.Template.Infrastructure.Db.Mssql.Internal.Repositories;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public static class Module
    {
        public static IServiceCollection AddInfrastructureMssqlConfiguration(this IServiceCollection services, MssqlSettings settings)
        {
            services.AddDbContext<MssqlDbContext>(options => options.UseSqlServer(settings.ConnectionString));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IApplicationBuilder MigrateMssqlDb(this IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.CreateScope();

            using var dbContext = scope.ServiceProvider.GetService<MssqlDbContext>();

            if (dbContext is null)
            {
                throw new ApplicationException(nameof(dbContext));
            }

            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }

            return builder;
        }
    }

    public class MssqlSettings
    {
        public const string Key = nameof(MssqlSettings);
        public string? ConnectionString { get; set; } = default;
    }
}
