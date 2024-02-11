using Week3Lab.ViewModels;

namespace Week3Lab.Views;

public partial class MainPage : ContentPage
{

    public MainPage(MainPageViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }

}
