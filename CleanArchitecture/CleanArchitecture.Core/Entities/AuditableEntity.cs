using System;

namespace CleanArchitecture.Core.Entities
{
    public class AuditableEntity : BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}