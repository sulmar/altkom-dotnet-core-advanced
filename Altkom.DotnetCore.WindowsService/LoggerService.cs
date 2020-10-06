using System;
using System.IO;
using Topshelf;

namespace Altkom.DotnetCore.WindowsService
{
    public class LoggerService : ServiceControl
    {
        private const string path = @"c:\temp\log.txt";
        private void Log(string message)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.AppendAllText(path, $"{DateTime.UtcNow} {message}" + Environment.NewLine);
        }

        public bool Start(HostControl hostControl)
        {
            Log("Started.");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Log("Stopped.");
            return true;
        }
    }
}
