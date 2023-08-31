using System.ComponentModel.DataAnnotations;

namespace AquarioBackend.Models.Domain.DTO
{
    public class RegisterRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType (DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
