using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altkom.DotnetCore.Fakers;
using Altkom.DotnetCore.FakeServices;
using Altkom.DotnetCore.IServices;
using Altkom.DotnetCore.Models;
using Altkom.DotnetCore.WebApi.HealthChecks;
using Bogus;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;

namespace Altkom.DotnetCore.WebApi
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
            services.AddSingleton<ICustomerService, FakeCustomerService>();
            services.AddSingleton<Faker<Customer>, CustomerFaker>();

            services.AddSingleton<IMessageService, FakeSmsService>();

            services.AddHealthChecks()
                .AddCheck<RandomHealthCheck>("random");

          //  services.AddHealthChecksUI();
            
            
            // dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter(camelCaseText: true));
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                }
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            //    endpoints.MapHealthChecksUI();

                // Microsoft.AspNetCore.Mvc.Formatters.Xml
                endpoints.MapControllers();

            });
        }
    }
}
