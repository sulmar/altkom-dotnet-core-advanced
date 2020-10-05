using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.DotnetCore.Models.SearchCriteria
{
    public class CustomerSearchCriteria
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
