using System.ComponentModel.DataAnnotations;

namespace NZWalks_API.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Password { get; set; }

        public string Roles { get; set; }
    }
}
