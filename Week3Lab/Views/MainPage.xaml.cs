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
            _viewModel.ActiveTodos.Add(new() { Id = _todoCounter++, Title = TodoTitleEntry.Text });
            TodoTitleEntry.Text = string.Empty;
        }
    }
}
