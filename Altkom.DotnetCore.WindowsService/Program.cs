using System;
using Topshelf;

namespace Altkom.DotnetCore.WindowsService
{

    // dotnet add package TopShelf

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Windows Service with Topshelf!");

            HostFactory.Run(x => {
                x.Service<LoggerService>();
                x.SetDisplayName("Altkom Service");
                x.SetDescription("Opis Altkom Service");
                x.StartAutomatically();                
            });
        }
    }
}
