using System;

namespace ProductionSystem.Domain.Entities 
{
    public enum UnitType
    {
        Quantitative, // مقداری
        Countable     // شمارشی
    }
    public class Unit : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DecimalPlaces { get; set; }
        public UnitType UnitKind { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDelete { get; set; } = false;
        public DateTime DeletedAt { get; set; }
    }
}
