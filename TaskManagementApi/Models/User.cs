using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
    }
}
