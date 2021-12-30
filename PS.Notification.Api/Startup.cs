using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PS.Notification.Application.Extensions;
using PS.Notification.Application.Settings;
using PS.Notification.Configurations;
using PS.Notification.Infrastructure.Extensions;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;

namespace PS.Notification.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDatabase(Configuration.GetConnectionString("Default"));

            var mailSettings = Configuration.GetSection("MailSettings").Get<MailSettings>();
            var rabbitMqSettings = Configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>();

            services.AddApplication(mailSettings, rabbitMqSettings);



            services.AddHealthChecks()
                    .AddProcessAllocatedMemoryHealthCheck(maximumMegabytesAllocated: 200, tags: new[] { "memory" })
                    .AddNpgSql(npgsqlConnectionString: Configuration.GetConnectionString("Default"), tags: new string[] { "Notification Db" })
                    .AddRabbitMQ(rabbitConnectionString: rabbitMqSettings.Host)
                    .AddUrlGroup(new Uri("https://localhost:44341/Mail"));

            services.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.MaximumHistoryEntriesPerEndpoint(50);
                setup.SetApiMaxActiveRequests(1);
                setup.SetEvaluationTimeInSeconds(10); //Configures the UI to poll for healthchecks updates every 5 seconds
            }).AddInMemoryStorage();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("default", new OpenApiInfo { Title = "PS.Notification.Api", Version = "v1", Description = "This api provides operations for sending notifications." });
                c.DescribeAllParametersInCamelCase();
                c.UseInlineDefinitionsForEnums();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/default/swagger.json", "PS.Notification.Api v1");
                    c.DocumentTitle = "Procedurment Solutions - Notification Api";
                    c.DisplayRequestDuration();
                    c.EnableFilter();
                    c.DocExpansion(DocExpansion.None);
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("PS Notification API!");
                });

                endpoints.MapHealthChecks("healthz", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecksUI(setup =>
                {
                    setup.UIPath = "/show-health-ui"; // this is ui path in your browser
                    setup.ApiPath = "/health-ui-api"; // the UI ( spa app )  use this path to get information from the store ( this is NOT the healthz path, is internal ui api )
                });


                endpoints.MapControllers();
            });
        }
    }

}
