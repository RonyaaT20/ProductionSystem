namespace ProductionSystem.Domain.Entities
{
    public class RolePermission : BaseEntity
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string PermissionKey { get; set; }

        public Role Role { get; set; }
    }
}
