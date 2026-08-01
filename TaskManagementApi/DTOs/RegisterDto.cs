using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(50, ErrorMessage ="Maximum Length should be 50")]
        [MinLength(3, ErrorMessage = "Minimum Length should be 3")]

        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
    }
}
