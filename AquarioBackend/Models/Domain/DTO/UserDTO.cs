namespace AquarioBackend.Models.Domain.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
