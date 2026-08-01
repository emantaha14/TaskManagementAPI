using System.ComponentModel.DataAnnotations;
using TaskManagementApi.Models;

namespace TaskManagementApi.DTOs
{
    public class UpdateTaskDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Title can't exceed 100 characters")]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters")]
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
       
    }
}
