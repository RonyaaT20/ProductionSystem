using System.Collections.Generic;

namespace ProductionSystem.Domain.DTOs
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CreatedAt { get; set; }
        public List<string> Permissions { get; set; }
    }

    public class CreateRoleDto
    {
        public string Title { get; set; }
        public List<string> Permissions { get; set; }
    }

    public class EditRoleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> Permissions { get; set; }
    }
}