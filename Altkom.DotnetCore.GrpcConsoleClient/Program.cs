using Altkom.DotnetCore.GrpcService;
using Bogus;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace Altkom.DotnetCore.GrpcConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello gRPC Client!");

            var locations = new Faker<AddLocationRequest>()
                .RuleFor(p => p.Name, f => f.Lorem.Word())
                .RuleFor(p => p.Latitude, f => f.Random.Float(-90f, 90f))
                .RuleFor(p => p.Longitude, f => f.Random.Float(-90f, 90f))
                .RuleFor(p => p.Speed, f => f.Random.Int(0, 140))
                .RuleFor(p => p.Direction, f => f.Random.Float(0, 1))
                .GenerateForever();

            const string url = "https://localhost:5001";

            GrpcChannel channel = GrpcChannel.ForAddress(url);
            var client = new TrackingService.TrackingServiceClient(channel);

            foreach (var location in locations)
            {
                await client.AddLocationAsync(location);

                Console.WriteLine($"Send {location.Name} {location.Latitude} {location.Longitude} {location.Speed}");

                await Task.Delay(TimeSpan.FromSeconds(0.01));
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();



        }
    }
}
