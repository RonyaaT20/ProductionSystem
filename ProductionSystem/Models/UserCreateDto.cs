namespace ProductionSystem.Models
{
    public class UserCreateDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
