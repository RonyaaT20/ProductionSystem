using System;
using System.Collections.Generic;

namespace ProductionSystem.Domain.Entities
{
    public class Role : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
