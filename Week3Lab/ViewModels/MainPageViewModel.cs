using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Week3Lab.Models;
using Week3Lab.Services;
using Week3Lab.Views;

namespace Week3Lab.ViewModels;

public partial class MainPageViewModel : ObservableObject, IQueryAttributable
{
    /// <summary>
    /// When the selected Todo item changes, update the IsSelected property and notify the UI
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CompleteActiveTodoCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteTodoCommand))]
    [NotifyCanExecuteChangedFor(nameof(EditTodoCommand))]
    private TodoViewModel _selectedActiveTodo;
    partial void OnSelectedActiveTodoChanged(TodoViewModel? oldValue, TodoViewModel newValue)
    {
        if (oldValue != null) { oldValue.IsSelected = false; }
        if (newValue != null) { newValue.IsSelected = true; }
    }

    /// <summary>
    /// When the selected Todo item changes, update the IsSelected property and notify the UI
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DeleteTodoCommand))]
    [NotifyCanExecuteChangedFor(nameof(ReactivateCompletedTodoCommand))]
    private TodoViewModel _selectedCompletedTodo;
    partial void OnSelectedCompletedTodoChanged(TodoViewModel? oldValue, TodoViewModel newValue)
    {
        if (oldValue != null) { oldValue.IsSelected = false; }
        if (newValue != null) { newValue.IsSelected = true; }
    }

    /// <summary>
    /// Keeps the Todo IDs unique
    /// </summary>
    private int _todoCounter;

    /// <summary>
    /// The text for the new Todo item on top of the Page
    /// </summary>
    [ObservableProperty]
    private string _newTodoText;

    /// <summary>
    /// Repository/Database for the Todo items
    /// </summary>
    private readonly ITodoRepository _todoRepository;

    /// <summary>
    /// Navigation service to navigate between pages
    /// </summary>
    private readonly INavigationService _navigationService;

    /// <summary>
    /// Collection of active Todo items
    /// </summary>
    public ObservableCollection<TodoViewModel> ActiveTodos { get; set; }

    /// <summary>
    /// Collection of completed Todo items
    /// </summary>
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

    /// <summary>
    /// Add a new Todo item to the list
    /// </summary>
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

    /// <summary>
    /// Check if the AddTodo command can be executed
    /// </summary>
    /// <returns>true if there is text for a new todo</returns>
    public bool CanExecuteAdd()
    {
        return !string.IsNullOrEmpty(NewTodoText);
    }

    /// <summary>
    /// Check if an active Todo item is selected in the CollectionView
    /// </summary>
    /// <returns>true if there is an item selected</returns>
    public bool IsActiveTodoSelected()
    {
        return SelectedActiveTodo != null;
    }

    /// <summary>
    /// Check if a completed Todo item is selected in the CollectionView
    /// </summary>
    /// <returns>true if there is an item selected</returns>
    public bool IsCompletedTodoSelected()
    {
        return SelectedCompletedTodo != null;
    }

    /// <summary>
    /// Complete the selected active Todo item
    /// </summary>
    // [RelayCommand(CanExecute = nameof(IsActiveTodoSelected))]
    [RelayCommand]
    public void CompleteActiveTodo(object param)
    {
        var todo = param as TodoViewModel;
        if (todo != null)
        {
            todo.IsDone = true;
            CompletedTodos.Insert(0, todo);
            ActiveTodos.Remove(todo);
            SelectedActiveTodo = null;
        }
    }

    /// <summary>
    /// Reactivate the selected completed Todo item to active and move it to the active list
    /// </summary>
    // [RelayCommand(CanExecute = nameof(IsCompletedTodoSelected))]
    [RelayCommand]
    public void ReactivateCompletedTodo(object param)
    {
        var todo = param as TodoViewModel;
        if (todo != null)
        {
            todo.IsDone = false;
            ActiveTodos.Insert(0, todo);
            CompletedTodos.Remove(todo);
            SelectedCompletedTodo = null;
        }
    }

    /// <summary>
    /// Delete the selected active Todo item
    /// </summary>
    //[RelayCommand(CanExecute = nameof(IsActiveTodoSelected))]
    [RelayCommand]
    public void DeleteTodo(object param)
    {
        var todo = param as TodoViewModel;
        if (todo != null)
        {
            if (todo.IsDone) { CompletedTodos.Remove(todo); }
            else { ActiveTodos.Remove(todo); }
            SelectedCompletedTodo = null;
            SelectedActiveTodo = null;
        }
    }

    /// <summary>
    /// Edit the selected active Todo item
    /// </summary>
    //[RelayCommand(CanExecute = nameof(IsActiveTodoSelected))]
    [RelayCommand]
    public void EditTodo(object param)
    {
        var todo = param as TodoViewModel;
        if (todo != null)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Todo", todo },
            };
            // Navigate to the EditTodoPage and pass the selected todo
            _navigationService.GoToAsync(nameof(EditTodoPage), true, navigationParameter);
        }
    }

    /// <summary>
    /// After editing a Todo item, update the item in the list
    /// Move it to the completed list if it is completed
    /// or move it to the active list if it is reactivated
    /// </summary>
    /// <param name="query"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var todo = query["Todo"] as TodoViewModel;
        if (todo != null)
        {
            todo.IsSelected = false;
            // check if the todo is in the active list
            if (ActiveTodos.Contains(todo))
            {
                // if the todo is completed, move it to the completed list
                if (todo.IsDone)
                {
                    ActiveTodos.Remove(todo);
                    CompletedTodos.Insert(0, todo);
                }
            }
            // check if the todo is in the completed list
            else
            {
                // if the todo is reactivated, move it to the active list
                if (!todo.IsDone)
                {
                    CompletedTodos.Remove(todo);
                    ActiveTodos.Insert(0, todo);
                }
            }
        }
    }

    //[RelayCommand(CanExecute = nameof(IsCompletedTodoSelected))]
    //public void DeleteCompletedTodo()
    //{
    //    if (SelectedCompletedTodo != null)
    //    {
    //        CompletedTodos.Remove(SelectedCompletedTodo);
    //        SelectedCompletedTodo = null;
    //    }
    //}
}
