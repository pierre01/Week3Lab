using Week3Lab.Models;

namespace Week3Lab.Services
{
    /// <summary>
    /// Repository CRUD operations for Todo items
    /// </summary>
    public interface ITodoRepository
    {
        // CREATE
        Todo Add(Todo todo);

        // READ
        List<Todo> GetAllTodos();
        // GetAllCompletedTodos();
        // GetAllActiveTodos();

        Todo? GetTodoById(int id);

        // UPDATE
        bool Update(Todo todo);

        // DELETE
        bool Delete(int id);
    }
}