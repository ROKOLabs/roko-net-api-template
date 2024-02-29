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
        public static IServiceCollection AddInfrastructurePostgresConfiguration(this IServiceCollection services, PostgresSettings settings)
        {
            services.AddDbContext<MyDbContext>(options => options.UseNpgsql(settings.ConnectionString));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IApplicationBuilder MigrateMssqlDb(this IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.CreateScope();

            using var dbContext = scope.ServiceProvider.GetService<MyDbContext>();

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

    public class PostgresSettings
    {
        public const string Key = nameof(PostgresSettings);
        public string? ConnectionString { get; set; } = default;
    }
}
