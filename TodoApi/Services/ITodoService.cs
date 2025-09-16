using TodoApi.Models;

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
}
