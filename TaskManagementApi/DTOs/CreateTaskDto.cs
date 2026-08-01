using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.DTOs
{
    public class CreateTaskDto
    {

        [Required(ErrorMessage ="Title is Required")]

        [MaxLength(100, ErrorMessage = "Title can't exceed 100 characters")]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters")]
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}
