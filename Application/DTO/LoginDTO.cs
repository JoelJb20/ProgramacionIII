using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class LoginDTO
    {
        [Required]
        public required string Email { set; get; }

        [Required]
        public required string Password { get; set; }
    }

    public class TokenDTO
    {
        public string? Token { get; set; }
    }
}
