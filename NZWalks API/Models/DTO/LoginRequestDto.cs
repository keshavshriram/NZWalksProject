using System.ComponentModel.DataAnnotations;

namespace NZWalks_API.Models.DTO
{
    public class LoginRequestDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
