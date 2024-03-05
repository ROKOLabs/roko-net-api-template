namespace Roko.Template
{
    using Roko.Template.Application;
    using Roko.Template.Infrastructure.Db.MyDb;
    using Roko.Template.Presentation.Api;
    using Roko.Template.Presentation.Api.Internal.Swagger;
    using Hellang.Middleware.ProblemDetails;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;

    public sealed class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        
#if (Postgres)
        public const string DbSettingsName = "PostgresSettings";
#elif (MsSql)
        public const string DbSettingsName = "MsSqlSettings";
#else
    #error Database not supported, define project constant or template parameter with the right value
#endif

        public MyDbSettings MyDbSettings =>
            this.Configuration
            .GetSection(MyDbSettings.Key)
            .Get<MyDbSettings>() ?? throw new ArgumentNullException(DbSettingsName,
                "The database connection settings are incorrect. Instructions on how to set up user secrets and other options can be found in README.md.");

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddInfrastructureMyDbConfiguration(this.MyDbSettings);
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

            app.MigrateMyDb();

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
