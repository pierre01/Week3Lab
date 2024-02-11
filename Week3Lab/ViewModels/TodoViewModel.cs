using CommunityToolkit.Mvvm.ComponentModel;
using Week3Lab.Models;

namespace Week3Lab.ViewModels
{
    public partial class TodoViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotDone))]
        private bool _isDone;

        public bool IsNotDone => !_isDone;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _notes;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsOverdue))]
        [NotifyPropertyChangedFor(nameof(HasDueDate))]
        [NotifyPropertyChangedFor(nameof(DueDateText))]
        private DateTime? _dueOn;

        public bool IsOverdue
        {
            get
            {
                if (_dueOn == null) return false;
                return _dueOn.HasValue & DueOn < DateTime.Today;
            }
        }

        public bool HasDueDate => _dueOn != null;

        public string DueDateText
        {
            get
            {
                var text = string.Empty;
                if (IsOverdue)
                {
                    int daysSinceDue = (int)(DateTime.Today - DueOn).Value.TotalDays;
                    if (daysSinceDue == 1)
                    {
                        text = "Due Yesterday";
                    }
                    else if (daysSinceDue > 1)
                    {
                        text = $"Due {daysSinceDue} days ago";
                    }
                }
                return text;
            }
        }


        private Todo _todo;

        public TodoViewModel(Todo todo)
        {
            _todo = todo;
            Title = todo.Title;
            IsDone = todo.IsDone;
            DueOn = todo.DueOn;
            Id = todo.Id;
        }

    }
}
