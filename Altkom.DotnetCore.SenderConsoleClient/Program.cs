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
                .WithAutomaticReconnect()
                .Build();

            connection.Reconnected += connectionId =>
            {
                if (connection.State == HubConnectionState.Connected)
                {
                    // TODO: Przesylanie danych z kolejki
                }

                return Task.CompletedTask;
            };

            connection.Closed += error =>
            {
                if (connection.State == HubConnectionState.Disconnected)
                {
                    // TODO: Powiadomienie użytkownika
                }

                return Task.CompletedTask;
            };


            Console.WriteLine("Connecting...");
            await connection.StartAsync();
            Console.WriteLine("Connected.");

            CustomerFaker customerFaker = new CustomerFaker();

            // Customer customer = customerFaker.Generate();

            var customers = customerFaker.GenerateForever();

            foreach (var customer in customers)
            {
                Console.WriteLine($"Sending {customer.FirstName} {customer.LastName}");
                await connection.SendAsync("SendAddedCustomer", customer);
                Console.WriteLine($"Sent.");

                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }
    }
}
