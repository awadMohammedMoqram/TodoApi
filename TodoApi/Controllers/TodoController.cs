using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetAllTodos()
        {
            var todos = await _todoService.GetAllTodosAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodo(int id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodosByCategory(string category)
        {
            var todos = await _todoService.GetTodosByCategoryAsync(category);
            return Ok(todos);
        }

        [HttpGet("completed")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetCompletedTodos()
        {
            var todos = await _todoService.GetCompletedTodosAsync();
            return Ok(todos);
        }

        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetPendingTodos()
        {
            var todos = await _todoService.GetPendingTodosAsync();
            return Ok(todos);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodo(CreateTodoDto todoDto)
        {
            var todo = await _todoService.CreateTodoAsync(todoDto);
            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> UpdateTodo(int id, UpdateTodoDto todoDto)
        {
            var todo = await _todoService.UpdateTodoAsync(id, todoDto);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var result = await _todoService.DeleteTodoAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id}/toggle")]
        public async Task<ActionResult<TodoItem>> ToggleComplete(int id)
        {
            var todo = await _todoService.ToggleCompleteAsync(id);
            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<Dictionary<string, object>>> GetStatistics()
        {
            var stats = await _todoService.GetStatisticsAsync();
            return Ok(stats);
        }

        [HttpGet("priorities")]
        public ActionResult<object> GetPriorities()
        {
            var priorities = Enum.GetValues<TodoPriority>()
                .Select(p => new
                {
                    name = p.ToString(),
                    value = (int)p
                })
                .ToList();

            return Ok(priorities);
        }

        [HttpGet("categories")]
        public ActionResult<string[]> GetCategories()
        {
            return Ok(TodoCategory.AllCategories);
        }
    }
}