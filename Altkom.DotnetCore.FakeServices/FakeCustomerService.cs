using Altkom.DotnetCore.IServices;
using Altkom.DotnetCore.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.DotnetCore.FakeServices
{

   

    public class FakeCustomerService : ICustomerService
    {
        private readonly ICollection<Customer> customers;

        public FakeCustomerService(Faker<Customer> faker)
        {
            customers = faker.Generate(100);
        }

        public void Add(Customer customer)
        {
            customers.Add(customer);
        }

        public IEnumerable<Customer> Get()
        {
            return customers;
        }

        public Customer Get(Guid id)
        {
            return customers.SingleOrDefault(c => c.Id == id);
        }

        public void Remove(Guid id)
        {
            customers.Remove(Get(id));
        }

        public void Update(Customer customer)
        {
            Remove(customer.Id);
            Add(customer);
        }
    }
}
