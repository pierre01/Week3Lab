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
        public int _id;

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
                _todo.Notes = Notes;
                _repositoryService.Update(_todo.TodoModel);
            }

            var navigationParameter = new Dictionary<string, object>
            {
                { "Todo", _todo },
            };
            await _navigationService.GoToAsync("//MainPage", true, navigationParameter);
            // return to the main page
            //await _navigationService.GoBackAsync(true);
        }

        [RelayCommand]
        private async void Cancel()
        {
            await _navigationService.GoBackAsync(true);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _todo = query["Todo"] as TodoViewModel;
            DueOn = _todo.DueOn;
            Title = _todo.Title;
            IsDone = _todo.IsDone;
            Id = _todo.Id;
        }
    }
}
