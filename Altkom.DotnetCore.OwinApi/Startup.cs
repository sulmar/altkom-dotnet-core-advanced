using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Altkom.DotnetCore.OwinApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // dotnet add package Microsoft.AspNetCore.Owin

            app.UseOwin(pipeline => pipeline(environment => OwinHandler));           
        }

        private async Task OwinHandler(IDictionary<string, object> environment)
        {
            string requestMethod = (string) environment["owin.RequestMethod"];
            string requestPath = (string) environment["owin.RequestPath"];

            string response = "Hello World!";
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);

            Stream responseStream = (Stream)environment["owin.ResponseBody"];
            await responseStream.WriteAsync(responseBytes, 0, responseBytes.Length);
        }
    }
}
