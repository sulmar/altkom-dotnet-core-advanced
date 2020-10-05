using Altkom.DotnetCore.Fakers;
using Altkom.DotnetCore.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Altkom.DotnetCore.SenderConsoleClient
{
    class Program
    {
        // >= C# 7.1
        static async Task Main(string[] args)
        {
            const string url = "http://localhost:5000/signalr/customers";

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Hello Signal-R Sender!");

            // dotnet add package Microsoft.AspNetCore.SignalR.Client

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            Console.WriteLine("Connecting...");
            await connection.StartAsync();
            Console.WriteLine("Connected.");

            CustomerFaker customerFaker = new CustomerFaker();
            Customer customer = customerFaker.Generate();

            Console.WriteLine($"Sending {customer.FirstName} {customer.LastName}");
            await connection.SendAsync("SendAddedCustomer", customer);
            Console.WriteLine($"Sent.");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }
    }
}
