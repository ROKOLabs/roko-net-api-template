namespace Roko.Template
{
    using Roko.Template.Application;
    using Roko.Template.Infrastructure.Db.Mssql;
    using Roko.Template.Presentation.Api;
    using Roko.Template.Presentation.Api.Internal.Swagger;
    using Hellang.Middleware.ProblemDetails;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;

    internal sealed class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public PostgresSettings PostgresSettings =>
            this.Configuration
            .GetSection(PostgresSettings.Key)
            .Get<PostgresSettings>() ?? throw new ArgumentNullException(nameof(this.PostgresSettings));

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddInfrastructurePostgresConfiguration(this.PostgresSettings);
            services.AddApplicationLayer();
            services.AddPresentationConfiguration(this.Environment);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseProblemDetails();

            if (!this.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.MigrateMssqlDb();

            app.UseHttpsRedirection();

            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseSwaggerConfiguration();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
