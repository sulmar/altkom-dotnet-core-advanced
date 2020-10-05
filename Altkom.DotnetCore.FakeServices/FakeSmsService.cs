using Altkom.DotnetCore.IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Altkom.DotnetCore.FakeServices
{
    public class FakeSmsService : IMessageService
    {
        public void Send(string message)
        {
            Trace.WriteLine($"Send {message} via sms");
        }
    }
}
