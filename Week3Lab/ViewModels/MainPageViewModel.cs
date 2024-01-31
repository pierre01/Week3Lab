using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Week3Lab.Models;

namespace Week3Lab.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private Todo _selectedActiveTodo;
        private Todo _selectedCompletedTodo;
        private int _todoCounter;
        private string _newTodoText;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Todo> ActiveTodos { get; set; }
        public ObservableCollection<Todo> CompletedTodos { get; set; }
        public string NewTodoText
        {
            get { return _newTodoText; }
            set
            {
                _newTodoText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewTodoText)));
            }
        }

        public ICommand AddTodoCommand { get; set; }
        public ICommand DeleteActiveTodoCommand { get; set; }
        public ICommand DeleteCompletedTodoCommand { get; set; }
        public ICommand CompleteActiveTodoCommand { get; set; }
        public ICommand ReactivateCompletedTodoCommand { get; set; }

        public Todo SelectedActiveTodo
        {
            get => _selectedActiveTodo;
            set
            {
                _selectedActiveTodo = value;
                (CompleteActiveTodoCommand as Command)?.ChangeCanExecute();
                (DeleteActiveTodoCommand as Command)?.ChangeCanExecute();
            }
        }
        public Todo SelectedCompletedTodo
        {
            get { return _selectedCompletedTodo; }
            set
            {
                _selectedCompletedTodo = value;
                (ReactivateCompletedTodoCommand as Command)?.ChangeCanExecute();
                (DeleteCompletedTodoCommand as Command)?.ChangeCanExecute();
            }
        }
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

            AddTodoCommand = new Command(AddTodo, CanExecuteAdd);
            DeleteActiveTodoCommand = new Command(DeleteActiveTodo, IsActiveTodoSelected);
            DeleteCompletedTodoCommand = new Command(DeleteCompletedTodo, IsCompletedTodoSelected);
            CompleteActiveTodoCommand = new Command(CompleteActiveTodo, IsActiveTodoSelected);
            ReactivateCompletedTodoCommand = new Command(ReactivateCompletedTodo, IsCompletedTodoSelected);
        }

        private void AddTodo(object param)
        {
            // Note: param is the value passed from the UI but it is also NewTodoText - You can use either
            string todoTitle = param as string;
            if (!string.IsNullOrEmpty(todoTitle))
            {
                ActiveTodos.Insert(0, new() { Id = _todoCounter++, Title = todoTitle });
                NewTodoText = string.Empty;
            }
        }
        public bool CanExecuteAdd(object param)
        {
            return !string.IsNullOrEmpty(NewTodoText);
        }

        public bool IsActiveTodoSelected()
        {
            return SelectedActiveTodo != null;
        }

        public bool IsCompletedTodoSelected()
        {
            return SelectedCompletedTodo != null;
        }


        public void CompleteActiveTodo()
        {
            var todo = SelectedActiveTodo;
            if (todo != null)
            {
                todo.IsDone = true;
                CompletedTodos.Insert(0, todo);
                ActiveTodos.Remove(todo);
                SelectedActiveTodo = null;
                (CompleteActiveTodoCommand as Command)?.ChangeCanExecute();
            }

        }

        public void ReactivateCompletedTodo()
        {
            var todo = SelectedCompletedTodo;
            if (todo != null)
            {
                todo.IsDone = false;
                ActiveTodos.Insert(0, todo);
                CompletedTodos.Remove(todo);
                SelectedCompletedTodo = null;
                (ReactivateCompletedTodoCommand as Command)?.ChangeCanExecute();
            }
        }

        public void DeleteActiveTodo()
        {
            var todo = SelectedActiveTodo;
            if (todo != null)
            {
                ActiveTodos.Remove(todo);
                SelectedActiveTodo = null;
                (DeleteActiveTodoCommand as Command)?.ChangeCanExecute();
            }

        }

        public void DeleteCompletedTodo()
        {
            var todo = SelectedCompletedTodo;
            if (todo != null)
            {
                CompletedTodos.Remove(todo);
                SelectedCompletedTodo = null;
                (DeleteCompletedTodoCommand as Command)?.ChangeCanExecute();
            }
        }
    }
}
