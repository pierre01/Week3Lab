using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Week3Lab.Models;

namespace Week3Lab.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CompleteActiveTodoCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteActiveTodoCommand))]
    private Todo _selectedActiveTodo;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DeleteCompletedTodoCommand))]
    [NotifyCanExecuteChangedFor(nameof(ReactivateCompletedTodoCommand))]
    private Todo _selectedCompletedTodo;

    private int _todoCounter;

    [ObservableProperty]
    private string _newTodoText;

    public ObservableCollection<Todo> ActiveTodos { get; set; }
    public ObservableCollection<Todo> CompletedTodos { get; set; }

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

    [RelayCommand(CanExecute = nameof(CanExecuteAdd))]
    private void AddTodo()
    {
        if (!string.IsNullOrEmpty(NewTodoText))
        {
            ActiveTodos.Insert(0, new() { Id = _todoCounter++, Title = NewTodoText });
            NewTodoText = string.Empty;
        }
    }

    public bool CanExecuteAdd()
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

    [RelayCommand(CanExecute = nameof(IsActiveTodoSelected))]
    public void CompleteActiveTodo()
    {
        if (SelectedActiveTodo != null)
        {
            SelectedActiveTodo.IsDone = true;
            CompletedTodos.Insert(0, SelectedActiveTodo);
            ActiveTodos.Remove(SelectedActiveTodo);
            SelectedActiveTodo = null;
        }
    }

    [RelayCommand(CanExecute = nameof(IsCompletedTodoSelected))]
    public void ReactivateCompletedTodo()
    {
        if (SelectedCompletedTodo != null)
        {
            SelectedCompletedTodo.IsDone = false;
            ActiveTodos.Insert(0, SelectedCompletedTodo);
            CompletedTodos.Remove(SelectedCompletedTodo);
            SelectedCompletedTodo = null;
        }
    }

    [RelayCommand(CanExecute = nameof(IsActiveTodoSelected))]
    public void DeleteActiveTodo()
    {
        if (SelectedActiveTodo != null)
        {
            ActiveTodos.Remove(SelectedActiveTodo);
            SelectedActiveTodo = null;
        }

    }

    [RelayCommand(CanExecute = nameof(IsCompletedTodoSelected))]
    public void DeleteCompletedTodo()
    {
        if (SelectedCompletedTodo != null)
        {
            CompletedTodos.Remove(SelectedCompletedTodo);
            SelectedCompletedTodo = null;
        }
    }
}
