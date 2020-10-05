using System;

namespace Altkom.DotnetCore.Models
{
    public abstract class BaseEntity : Base
    {
        public Guid Id { get; set; }
    }
}
