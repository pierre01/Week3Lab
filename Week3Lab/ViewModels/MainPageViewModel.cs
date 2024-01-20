using System.Collections.ObjectModel;
using Week3Lab.Models;

namespace Week3Lab.ViewModels
{
    public class MainPageViewModel
    {
        public ObservableCollection<Todo> ActiveTodos { get; set; }
        public ObservableCollection<Todo> CompletedTodos { get; set; }

        public Todo SelectedActiveTodo { get; set; }
        public Todo SelectedCompletedTodo { get; set; }

        public MainPageViewModel()
        {
            ActiveTodos =
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
            ];
            CompletedTodos = [
                new Todo { Id = 11, Title = "Call Mom", IsDone = true },
                new Todo { Id = 12, Title = "Run 3 miles", IsDone = true },
                new Todo { Id = 13, Title = "Clean Cat litter", IsDone = true },
                new Todo { Id = 14, Title = "Doctor Appointment", IsDone = true },
            ];
        }

        public void CompleteActiveTodo()
        {
            var todo = SelectedActiveTodo;
            todo.IsDone = true;
            CompletedTodos.Add(todo);
            ActiveTodos.Remove(todo);
        }

        public void DeleteActiveTodo()
        {
            ActiveTodos.Remove(SelectedActiveTodo);
        }

        public void DeleteCompletedTodo()
        {
            CompletedTodos.Remove(SelectedCompletedTodo);
        }
    }
}
