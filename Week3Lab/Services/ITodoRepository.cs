using Week3Lab.Models;

namespace Week3Lab.Services
{

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