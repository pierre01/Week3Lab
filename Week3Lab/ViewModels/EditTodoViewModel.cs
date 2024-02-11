using CommunityToolkit.Mvvm.ComponentModel;

namespace Week3Lab.ViewModels
{
    class EditTodoViewModel : ObservableObject, IQueryAttributable
    {
        private TodoViewModel? _todo;

        public EditTodoViewModel()
        {

        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _todo = query["Todo"] as TodoViewModel;
        }
    }
}
