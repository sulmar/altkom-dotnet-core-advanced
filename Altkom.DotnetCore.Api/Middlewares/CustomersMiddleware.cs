using Altkom.DotnetCore.IServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Altkom.DotnetCore.Api.Middlewares
{
    public class CustomersMiddleware
    {
        private readonly RequestDelegate next;

        private readonly ICustomerService customerService;

        public CustomersMiddleware(RequestDelegate next, ICustomerService customerService)
        {
            this.customerService = customerService;
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var customers = customerService.Get();

            string content = JsonSerializer.Serialize(customers);

            context.Response.StatusCode = (int) HttpStatusCode.OK;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(content);
        }
    }

    public static class CustomersMiddlewareExtensions
    {
        public static IEndpointConventionBuilder MapCustomers(this IEndpointRouteBuilder endpoints, string pattern)
        {
            return endpoints.MapGet(pattern, endpoints.CreateApplicationBuilder()
                .UseMiddleware<CustomersMiddleware>()
                .Build());
        }
    }
}
