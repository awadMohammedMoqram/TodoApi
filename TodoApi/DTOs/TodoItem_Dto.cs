using System.ComponentModel.DataAnnotations;
using TodoApi.Models;

namespace TodoApi.DTOs
{
    public class CreateTodoDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public TodoPriority Priority { get; set; } = TodoPriority.Medium;

        public string? Category { get; set; }
    }

    public class UpdateTodoDto
    {
        [StringLength(200)]
        public string? Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool? IsCompleted { get; set; }

        public TodoPriority? Priority { get; set; }

        public string? Category { get; set; }
    }
}
