using Week3Lab.Models;

namespace Week3Lab.Services
{
    /// <summary>
    /// Repository CRUD operations for Todo items
    /// </summary>
    public interface ITodoRepository
    {
        // CREATE
        Task<Todo> Add(Todo todo);

        // READ
        Task<List<Todo>> GetAllTodos();
        // GetAllCompletedTodos();
        // GetAllActiveTodos();

        Task<Todo?> GetTodoById(int id);

        // UPDATE
        Task<bool> Update(Todo todo);

        // DELETE
        Task<bool> Delete(int id);
    }
}