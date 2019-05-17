using System.ComponentModel.DataAnnotations;

namespace ParrotWings.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Please enter email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
    }
}