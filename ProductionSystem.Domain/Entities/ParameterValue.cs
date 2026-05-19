using System;

namespace ProductionSystem.Domain.Entities
{
    public class ParameterValue : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int ProductParameterId { get; set; }
        public ProductParameter ProductParameter { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
