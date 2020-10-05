using Altkom.DotnetCore.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Altkom.DotnetCore.ReceiverConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string url = "http://localhost:5000/signalr/customers";

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Hello Signal-R Receiver!");

            // dotnet add package Microsoft.AspNetCore.SignalR.Client

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            Console.WriteLine("Connecting...");
            await connection.StartAsync();
            Console.WriteLine("Connected.");

            connection.On<Customer>("AddedCustomer", 
                customer => Console.WriteLine($"Received {customer.FirstName} {customer.LastName}"));


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();
        }
    }
}
