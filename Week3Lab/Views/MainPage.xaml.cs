using Week3Lab.ViewModels;

namespace Week3Lab.Views;

public partial class MainPage : ContentPage
{

    private MainPageViewModel _viewModel;

    public MainPage()
    {
        _viewModel = new MainPageViewModel();
        BindingContext = _viewModel;
        InitializeComponent();
    }

}
