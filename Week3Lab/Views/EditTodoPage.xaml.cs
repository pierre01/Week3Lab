using Week3Lab.ViewModels;

namespace Week3Lab.Views;

public partial class EditTodoPage : ContentPage
{
    public EditTodoPage(EditTodoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}