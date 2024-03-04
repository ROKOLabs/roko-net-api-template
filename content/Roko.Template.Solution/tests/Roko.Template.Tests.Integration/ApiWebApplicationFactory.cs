namespace Roko.Template.Tests.Integration
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Diagnostics.CodeAnalysis;
    using Testcontainers.PostgreSql;

    public class ApiWebApplicationFactory: WebApplicationFactory<Roko.Template.Startup>
    {
        private readonly PostgreSqlContainer _postgresSqlContainer = new PostgreSqlBuilder()
            .WithDatabase("RokoTemplateDatabase")
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            this._postgresSqlContainer.StartAsync().Wait();

            builder.ConfigureAppConfiguration(config =>
            {
                config.AddInMemoryCollection(
                [
                    new ("PostgresSettings:ConnectionString", this._postgresSqlContainer.GetConnectionString())
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