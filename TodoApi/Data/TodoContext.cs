using TodoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure TodoItem entity
            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Priority).HasMaxLength(50);
                entity.Property(e => e.Category).HasMaxLength(100);
                entity.HasIndex(e => e.IsCompleted);
                entity.HasIndex(e => e.CreatedAt);
                entity.HasIndex(e => e.Category);
            });

            // Seed initial data
            modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem
                {
                    Id = 1,
                    Title = "Setup PostgreSQL Database",
                    Description = "Install and configure PostgreSQL for the Todo application",
                    Priority = "High",
                    Category = "Setup",
                    CreatedAt = DateTime.UtcNow
                },
                new TodoItem
                {
                    Id = 2,
                    Title = "Create API Endpoints",
                    Description = "Implement CRUD operations for Todo items",
                    Priority = "High",
                    Category = "Development",
                    CreatedAt = DateTime.UtcNow
                },
                new TodoItem
                {
                    Id = 3,
                    Title = "Add Swagger Documentation",
                    Description = "Configure Swagger for API documentation",
                    Priority = "Medium",
                    Category = "Documentation",
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}