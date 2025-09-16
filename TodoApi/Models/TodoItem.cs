using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public enum TodoPriority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }

    public static class TodoCategory
    {
        public const string Setup = "Setup";
        public const string Development = "Development";
        public const string Documentation = "Documentation";
        public const string Testing = "Testing";
        public const string Deployment = "Deployment";
        public const string Maintenance = "Maintenance";
        public const string Research = "Research";
        public const string Planning = "Planning";
        public const string Review = "Review";
        public const string Other = "Other";

        public static readonly string[] AllCategories = {
            Setup, Development, Documentation, Testing, Deployment,
            Maintenance, Research, Planning, Review, Other
        };
    }

    public class TodoItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public TodoPriority Priority { get; set; } = TodoPriority.Medium;

        public string? Category { get; set; }
    }

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