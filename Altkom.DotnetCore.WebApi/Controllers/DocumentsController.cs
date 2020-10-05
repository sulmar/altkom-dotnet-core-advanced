using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.DotnetCore.WebApi.Controllers
{
    // GET api/customers/10/documents

    // GET api/documents?customerId=10

    [Route("api/documents")]
    public class DocumentsController : ControllerBase
    {
        // GET api/customers/10/documents

        [HttpGet("~/api/customers/{customerId}/documents")]
        public IActionResult GetByCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Upload(IList<IFormFile> files)
        {
            foreach (var file in files)
            {
                // todo: process file
            }

            long size = files.Sum(f => f.Length);

            return Accepted(new { count = files.Count, size });

        }
    }
}
