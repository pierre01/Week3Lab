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
        [NotifyPropertyChangedFor(nameof(IsOverdue))]
        [NotifyPropertyChangedFor(nameof(DueDateText))]
        private bool _isDone;
        partial void OnIsDoneChanged(bool oldValue, bool newValue)
        {
            if (newValue)
            {
                _todo.CompletedOn = DateTime.Now;
            }
            else
            {
                _todo.CompletedOn = null;
            }
        }

        public bool IsNotDone => !IsDone;

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
                if (IsDone) return false;
                if (DueOn == null) return false;
                return DueOn.HasValue & DueOn < DateTime.Today;
            }
        }

        public bool HasDueDate => DueOn != null;

        public string DueDateText
        {
            get
            {
                var text = string.Empty;
                if (DueOn == null) return text;
                if (IsOverdue && IsNotDone)
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
                else // Not overdue
                {
                    if (IsNotDone)
                    {
                        int daysUntilDue = (int)(DueOn - DateTime.Today).Value.TotalDays;
                        if (daysUntilDue == 1)
                        {
                            text = "Due Tomorrow";
                        }
                        else if (daysUntilDue > 1)
                        {
                            text = $"Due in {daysUntilDue} days";
                        }
                    }
                    else
                    {
                        text = $"Completed on {_todo.CompletedOn}";
                    }
                }
                return text;
            }
        }

        private Todo _todo;
        public Todo TodoModel { get { return _todo; } }

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
