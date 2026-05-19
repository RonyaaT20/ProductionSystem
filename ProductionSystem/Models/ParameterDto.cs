using System.Collections.Generic;

namespace ProductionSystem.Models
{
    public class ParameterDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int? UnitId { get; set; }
        public List<string> Values { get; set; }
    }
}