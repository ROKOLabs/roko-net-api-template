namespace Roko.Template.Tests.Integration
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Diagnostics.CodeAnalysis;

    public class ApiWebApplicationFactory: WebApplicationFactory<Roko.Template.Startup>
    {
#if( MyDb )
        private readonly Testcontainers.PostgreSql.PostgreSqlContainer _databaseContainer = new Testcontainers.PostgreSql.PostgreSqlBuilder()
            .WithDatabase("RokoTemplateDatabase")
            .Build();
#elif( Postgres )
        private readonly Testcontainers.PostgreSql.PostgreSqlContainer _databaseContainer = new Testcontainers.PostgreSql.PostgreSqlBuilder()
            .WithDatabase("RokoTemplateDatabase")
            .Build();
#elif( MsSql )
        private readonly Testcontainers.MsSql.MsSqlContainer _databaseContainer = new Testcontainers.MsSql.MsSqlBuilder().Build();
#else
    #error Database not supported, define project constant or template parameter with the right value
#endif

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            this._databaseContainer.StartAsync().Wait();

            builder.ConfigureAppConfiguration(config =>
            {
                config.AddInMemoryCollection(
                [
#if( MyDb )
                    new ("PostgresSettings:ConnectionString", this._databaseContainer.GetConnectionString())
#elif( Postgres )
                    new ("PostgresSettings:ConnectionString", this._databaseContainer.GetConnectionString())
#elif( MsSql )
                    new ("MssqlSettings:ConnectionString", this._databaseContainer.GetConnectionString().Replace("Database=master", "Database=RokoTemplateDatabase"))
#else
    #error Database not supported, define project constant or template parameter with the right value
#endif
                ]);
            });

            builder.ConfigureTestServices(services =>
            {
                services
                    .AddAuthentication("IntegrationTest")
                    .AddScheme<AuthenticationSchemeOptions, IntegrationTestAuthenticationHandler>(
                        "IntegrationTest",
                        options => { }
                    );
            });
        }
    }
}