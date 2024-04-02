namespace Backend.Models
{
    public class AppUser
    {
        public string? Email { get; set; }
        
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Role { get; set; }
    }

    public class LoginUser
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
   
    public class RoleInfo
    {
        public string? Name { get; set; }
    }
   
    public class UserRole
    {
        public string? Email { get; set; }
        public string? RoleName { get; set; }
    }
}
