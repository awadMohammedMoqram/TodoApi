using TodoApi.Data;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetAllTodosAsync();
        Task<TodoItem?> GetTodoByIdAsync(int id);
        Task<IEnumerable<TodoItem>> GetTodosByCategoryAsync(string category);
        Task<IEnumerable<TodoItem>> GetCompletedTodosAsync();
        Task<IEnumerable<TodoItem>> GetPendingTodosAsync();
        Task<TodoItem> CreateTodoAsync(CreateTodoDto todoDto);
        Task<TodoItem?> UpdateTodoAsync(int id, UpdateTodoDto todoDto);
        Task<bool> DeleteTodoAsync(int id);
        Task<TodoItem?> ToggleCompleteAsync(int id);
        Task<Dictionary<string, object>> GetStatisticsAsync();
    }

    public class TodoService : ITodoService
    {
        private readonly TodoContext _context;

        public TodoService(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodosAsync()
        {
            return await _context.TodoItems
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<TodoItem?> GetTodoByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<IEnumerable<TodoItem>> GetTodosByCategoryAsync(string category)
        {
            return await _context.TodoItems
                .Where(t => t.Category == category)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetCompletedTodosAsync()
        {
            return await _context.TodoItems
                .Where(t => t.IsCompleted)
                .OrderByDescending(t => t.CompletedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetPendingTodosAsync()
        {
            return await _context.TodoItems
                .Where(t => !t.IsCompleted)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<TodoItem> CreateTodoAsync(CreateTodoDto todoDto)
        {
            var todo = new TodoItem
            {
                Title = todoDto.Title,
                Description = todoDto.Description,
                Priority = todoDto.Priority,
                Category = todoDto.Category,
                CreatedAt = DateTime.UtcNow
            };

            _context.TodoItems.Add(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<TodoItem?> UpdateTodoAsync(int id, UpdateTodoDto todoDto)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
                return null;

            if (!string.IsNullOrEmpty(todoDto.Title))
                todo.Title = todoDto.Title;

            if (todoDto.Description != null)
                todo.Description = todoDto.Description;

            if (!string.IsNullOrEmpty(todoDto.Priority))
                todo.Priority = todoDto.Priority;

            if (todoDto.Category != null)
                todo.Category = todoDto.Category;

            if (todoDto.IsCompleted.HasValue)
            {
                todo.IsCompleted = todoDto.IsCompleted.Value;
                todo.CompletedAt = todoDto.IsCompleted.Value ? DateTime.UtcNow : null;
            }

            todo.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
                return false;

            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TodoItem?> ToggleCompleteAsync(int id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
                return null;

            todo.IsCompleted = !todo.IsCompleted;
            todo.CompletedAt = todo.IsCompleted ? DateTime.UtcNow : null;
            todo.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<Dictionary<string, object>> GetStatisticsAsync()
        {
            var totalTodos = await _context.TodoItems.CountAsync();
            var completedTodos = await _context.TodoItems.CountAsync(t => t.IsCompleted);
            var pendingTodos = totalTodos - completedTodos;

            var categoriesCount = await _context.TodoItems
                .Where(t => t.Category != null)
                .GroupBy(t => t.Category)
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Category!, x => x.Count);

            var prioritiesCount = await _context.TodoItems
                .GroupBy(t => t.Priority)
                .Select(g => new { Priority = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Priority, x => x.Count);

            return new Dictionary<string, object>
            {
                { "total", totalTodos },
                { "completed", completedTodos },
                { "pending", pendingTodos },
                { "completionRate", totalTodos > 0 ? (double)completedTodos / totalTodos * 100 : 0 },
                { "categories", categoriesCount },
                { "priorities", prioritiesCount }
            };
        }
    }
}