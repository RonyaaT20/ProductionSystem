using ProductionSystem.Domain.Entities;

namespace ProductionSystem.Domain.DTOs
{
    public class UnitDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public UnitType UnitKind { get; set; }
        public int DecimalPlaces { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
    }

    public class CreateUnitDto
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public UnitType UnitKind { get; set; }
        public int DecimalPlaces { get; set; }
    }

    public class EditUnitDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public UnitType UnitKind { get; set; }
        public int DecimalPlaces { get; set; }
    }
}