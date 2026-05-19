using System;

namespace ProductionSystem.Domain.Entities
{
    public class Personnel : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Mobile { get; set; }

    }
}
