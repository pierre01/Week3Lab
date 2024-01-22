using Week3Lab.ViewModels;

namespace Week3Lab.Views;

public partial class MainPage : ContentPage
{
    int _todoCounter = 50;

    MainPageViewModel _viewModel;

    public MainPage()
    {
        _viewModel = new MainPageViewModel();
        BindingContext = _viewModel;
        InitializeComponent();
    }


    private void OnTodoAdded(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(TodoTitleEntry.Text))
        {
            _viewModel.ActiveTodos.Insert(0, new() { Id = _todoCounter++, Title = TodoTitleEntry.Text });
            TodoTitleEntry.Text = string.Empty;
        }
    }

    private void OnCheckActiveTodo(object sender, EventArgs e)
    {
        if (_viewModel.SelectedActiveTodo != null)
        {
            _viewModel.CompletedTodos.Add(_viewModel.SelectedActiveTodo);
            _viewModel.ActiveTodos.Remove(_viewModel.SelectedActiveTodo);
        }
    }

    private void OnDeleteActiveTodo(object sender, EventArgs e)
    {
        if (_viewModel.SelectedActiveTodo != null)
        {
            _viewModel.ActiveTodos.Remove(_viewModel.SelectedActiveTodo);
        }
    }

    private void OnCheckCompletedTodo(object sender, EventArgs e)
    {
        if (_viewModel.SelectedCompletedTodo != null)
        {
            _viewModel.ActiveTodos.Insert(0, _viewModel.SelectedCompletedTodo);
            _viewModel.CompletedTodos.Remove(_viewModel.SelectedCompletedTodo);
        }
    }

    private void OnDeleteCompletedTodo(object sender, EventArgs e)
    {
        if (_viewModel.SelectedCompletedTodo != null)
        {
            _viewModel.CompletedTodos.Remove(_viewModel.SelectedCompletedTodo);
        }
    }
}
