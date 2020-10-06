using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.DotnetCore.HostedService
{
    public class HelloWorldHostedService : IHostedService
    {

        private readonly ILogger<HelloWorldHostedService> logger;
        private Timer timer;

        public HelloWorldHostedService(ILogger<HelloWorldHostedService> logger)
        {
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(HelloWorld, null, 0, 1000);

            // FileSystemWatcher

            return Task.CompletedTask;
        }

        private void HelloWorld(object state)
        {
            logger.LogInformation($"{Thread.CurrentThread.ManagedThreadId} Hello World!");
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
