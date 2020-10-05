using Altkom.DotnetCore.Models;
using System;
using System.Collections.Generic;

namespace Altkom.DotnetCore.IServices
{
    public interface ICustomerService
    {
        IEnumerable<Customer> Get();
        Customer Get(Guid id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Remove(Guid id);       
    }
}
