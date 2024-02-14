﻿using Week3Lab.Models;

namespace Week3Lab.Services
{
    public class TodoRepository : ITodoRepository
    {
        private List<Todo> _todoList;
        private int _idCount = 15;
        public TodoRepository()
        {
            _todoList =
            [
                new Todo { Id = 1, Title = "CarWash", IsDone = false },
                new Todo { Id = 2, Title = "Mow Lawn", IsDone = false },
                new Todo { Id = 3, Title = "Trim Edges", IsDone = false },
                new Todo { Id = 4, Title = "Repair Fence", IsDone = false },
                new Todo { Id = 5, Title = "Pickup Kids", IsDone = false },
                new Todo { Id = 6, Title = "Cook Dinner", IsDone = false },
                new Todo { Id = 7, Title = "Book Ticket", IsDone = false },
                new Todo { Id = 8, Title = "Buy Eggs", IsDone = false },
                new Todo { Id = 9, Title = "Buy Orange Juice", IsDone = false },
                new Todo { Id = 10, Title = "Pay Housekeeper", IsDone = false },
                new Todo { Id = 11, Title = "Call Mom", IsDone = true },
                new Todo { Id = 12, Title = "Run 3 miles", IsDone = true },
                new Todo { Id = 13, Title = "Clean Cat litter", IsDone = true },
                new Todo { Id = 14, Title = "Doctor Appointment", IsDone = true },
            ];
        }

        public List<Todo> GetAllTodos()
        {
            return _todoList;
        }

        public Todo? GetTodoById(int id)
        {
            return _todoList.FirstOrDefault(t => t.Id == id);
        }

        public Todo Add(Todo todo)
        {
            todo.Id = _idCount++;
            _todoList.Add(todo);
            return todo;
        }

        public bool Delete(int id)
        {
            // Find the modifiedTodo with the given id and delete it
            var todo = _todoList.FirstOrDefault(t => t.Id == id);
            if (todo != null)
            {
                _todoList.Remove(todo);
                return true;
            }
            return false;
        }

        public bool Update(Todo modifiedTodo)
        {
            // Find the modifiedTodo with the given id and update it
            var todo = _todoList.FirstOrDefault(t => t.Id == modifiedTodo.Id);
            if (todo != null)
            {
                todo.Title = modifiedTodo.Title;
                if (todo.IsDone == false && modifiedTodo.IsDone)
                {
                    todo.CompletedOn = DateTime.Now;
                }
                else if (todo.IsDone && modifiedTodo.IsDone == false)
                {
                    todo.CompletedOn = null;
                }

                todo.IsDone = modifiedTodo.IsDone;
                todo.DueOn = modifiedTodo.DueOn;
                todo.Notes = modifiedTodo.Notes;
                todo.LastModifiedOn = DateTime.Now;
                return true;
            }
            // If the todo is not found, return false
            return false;
        }

    }
}