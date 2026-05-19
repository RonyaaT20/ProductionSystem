namespace ProductionSystem.Domain.DTOs
{
    // برای نمایش داخل لیست
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Mobile { get; set; }
        public int RoleId { get; set; }
        public string RoleTitle { get; set; }
        public bool IsActive { get; set; }
        public string CreatedAt { get; set; }
    }

    // برای ایجاد کاربر جدید
    public class CreateUserDto
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }

    }

    // برای ویرایش کاربر
    public class EditUserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // اگه خالی بود تغییر نمیکنه
        public string Mobile { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
