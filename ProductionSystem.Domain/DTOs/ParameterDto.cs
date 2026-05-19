using System.Collections.Generic;

namespace ProductionSystem.Domain.DTOs
{
    public class ParameterDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int? UnitId { get; set; }
        public string UnitTitle { get; set; }
        public List<string> Values { get; set; }
    }

    public class CreateParameterDto
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public int? UnitId { get; set; }
        public List<string> Values { get; set; }
    }

    public class EditParameterDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int? UnitId { get; set; }
        public List<string> Values { get; set; }
    }
}