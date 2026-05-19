using System.Collections.Generic;

namespace ProductionSystem.Models
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> Permissions { get; set; }
    }
}