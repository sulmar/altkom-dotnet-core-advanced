﻿namespace Altkom.DotnetCore.Models
{

    public class Customer : Base
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsRemoved { get; set; }
    }
}
