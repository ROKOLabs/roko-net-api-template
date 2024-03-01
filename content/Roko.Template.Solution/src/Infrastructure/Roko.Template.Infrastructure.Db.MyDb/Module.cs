namespace Roko.Template.Infrastructure.Db.MyDb
{
    using Roko.Template.Application.Contracts;
    using Roko.Template.Infrastructure.Db.MyDb.Internal;
    using Roko.Template.Infrastructure.Db.MyDb.Internal.Repositories;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public static class Module
    {
        public static IServiceCollection AddInfrastructureMyDbConfiguration(this IServiceCollection services, MyDbSettings settings)
        {
#if (Postgres)
            services.AddDbContext<MyDbContext>(options => options.UseNpgsql(settings.ConnectionString));
#elif (MsSql)
            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(settings.ConnectionString));
#else
    #error Database not supported, define project constant or template parameter with the right value
#endif

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IApplicationBuilder MigrateMyDb(this IApplicationBuilder builder)
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
            else
            {
                throw new InvalidOperationException(
                    $"""Create "InitialCreate" migration so that database can be created""");
            }

            return builder;
        }
    }

    public class MyDbSettings
    {
#if (Postgres)
        public const string Key = "PostgresSettings";
#elif (MsSql)
        public const string Key = "MsSqlSettings";
#else
    #error Database not supported, define project constant or template parameter with the right value
#endif
        public string? ConnectionString { get; set; } = default;
    }
}
