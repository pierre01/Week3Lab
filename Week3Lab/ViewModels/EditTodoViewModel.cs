using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using Week3Lab.Services;

namespace Week3Lab.ViewModels
{
    public partial class EditTodoViewModel : ObservableValidator, IQueryAttributable
    {
        private TodoViewModel? _todo;

        [ObservableProperty]
        [Required]
        private string _title;

        [ObservableProperty]
        private string? _notes;

        [ObservableProperty]
        private DateTime? _dueOn;

        private DateTime? _lastModifiedOn;

        [ObservableProperty]
        private bool _isDone;

        private INavigationService _navigationService;

        private ITodoRepository _repositoryService;

        public EditTodoViewModel(ITodoRepository repositoryService, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _repositoryService = repositoryService;
        }

        [RelayCommand]
        private async void Save()
        {
            ValidateAllProperties();
            if (HasErrors)
            {
                return;
            }
            if (_todo != null)
            {
                _todo.DueOn = DueOn;
                _todo.Title = Title;
                _todo.IsDone = IsDone;
                _repositoryService.Update(_todo.TodoModel);
            }
            // return to the main page
            Shell.Current.Navigation.PopAsync(true);
            //_navigationService.GoToAsync(nameof(MainPage), true);
        }

        [RelayCommand]
        private void Cancel()
        {
            Shell.Current.Navigation.PopAsync(true);
            //_navigationService.GoToAsync(nameof(MainPage), true);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _todo = query["Todo"] as TodoViewModel;
            DueOn = _todo.DueOn;
            Title = _todo.Title;
            IsDone = _todo.IsDone;
        }
    }
}
