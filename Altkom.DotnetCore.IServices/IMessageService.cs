using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.DotnetCore.IServices
{
    public interface IMessageService
    {
        void Send(string message);
    }
}
