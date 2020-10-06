using Altkom.DotnetCore.IServices;
using Altkom.DotnetCore.Models;
using Altkom.DotnetCore.Models.SearchCriteria;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Policy;

namespace Altkom.DotnetCore.WebApi.Controllers
{


    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        // GET api/customers
        [HttpGet]
        public IActionResult Get()
        {
            var customers = customerService.Get();

            return Ok(customers);
        }

        // GET api/customers/4234234237647
        // https://docs.microsoft.com/pl-pl/aspnet/core/fundamentals/routing?view=aspnetcore-3.1#route-constraint-reference
        [HttpGet("{id:guid}", Name = nameof(GetById))]
        public IActionResult GetById(Guid id)
        {
            var customer = customerService.Get(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }


        // tworzenie wlasnej reguly -> IActionConstraint
        [HttpGet("{pesel}")]
        public IActionResult Get(string pesel)
        {
            throw new NotImplementedException();
        }

        // GET api/customers?firstname=Ma&lastname=S
        public IActionResult Get([FromQuery] CustomerSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer, [FromServices] IMessageService messageService)
        {
            customerService.Add(customer);

            // TODO:
            // Send email

            // Send sms
            messageService.Send($"Dziękujemy {customer.FirstName} {customer.LastName}");

            return CreatedAtRoute(nameof(GetById), new { Id = customer.Id }, customer);
        }

        // PUT /api/customers/53453457347

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();

            customerService.Update(customer);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(Guid id, [FromBody] Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();

            // http://jsonpatch.com/
            customerService.Update(customer);

            return NoContent();
        }

        // DELETE api/customers/4374283742
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var customer = customerService.Get(id);

            if (customer == null)
                return NotFound();

            customerService.Remove(id);

            return NoContent();
        }
    }
}
