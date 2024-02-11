using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Week3Lab.Models;
using Week3Lab.Services;
using Week3Lab.Views;

namespace Week3Lab.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CompleteActiveTodoCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteActiveTodoCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditActiveTodoCommand))]
    private TodoViewModel _selectedActiveTodo;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DeleteCompletedTodoCommand))]
    [NotifyCanExecuteChangedFor(nameof(ReactivateCompletedTodoCommand))]
    private TodoViewModel _selectedCompletedTodo;

    private int _todoCounter;

    [ObservableProperty]
    private string _newTodoText;
    private readonly ITodoRepository _todoRepository;
    private readonly INavigationService _navigationService;

    public ObservableCollection<TodoViewModel> ActiveTodos { get; set; }
    public ObservableCollection<TodoViewModel> CompletedTodos { get; set; }

    public MainPageViewModel(ITodoRepository todoRepository, INavigationService navigationService)
    {
        _todoRepository = todoRepository;
        _navigationService = navigationService;
        ActiveTodos = new ObservableCollection<TodoViewModel>();
        CompletedTodos = new ObservableCollection<TodoViewModel>();
        foreach (Todo todo in _todoRepository.GetAllTodos())
        {
            if (!todo.IsDone)
            {
                ActiveTodos.Add(new TodoViewModel(todo));
            }
            else
            {
                CompletedTodos.Add(new TodoViewModel(todo));
            }
        }
    }

    [RelayCommand(CanExecute = nameof(CanExecuteAdd))]
    private void AddTodo()
    {
        if (!string.IsNullOrEmpty(NewTodoText))
        {
            var todo = _todoRepository.Add(new() { Title = NewTodoText });
            ActiveTodos.Insert(0, new TodoViewModel(todo));
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

    [RelayCommand(CanExecute = nameof(IsActiveTodoSelected))]
    public void EditActiveTodo()
    {
        if (SelectedActiveTodo != null)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Todo", SelectedActiveTodo },
            };
            // Navigate to the EditTodoPage and pass the selected todo
            _navigationService.GoToAsync(nameof(EditTodoPage), true, navigationParameter);
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
