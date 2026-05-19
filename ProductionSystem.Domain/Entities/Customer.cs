using System;

namespace ProductionSystem.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Mobile { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

    }
    
}
