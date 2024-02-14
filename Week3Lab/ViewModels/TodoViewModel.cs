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

        /// <summary>
        /// Set when the item is selected in the CollectionView
        /// </summary>
        [ObservableProperty]
        private bool _isSelected;

        public bool IsNotDone => !IsDone;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string? _notes;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsOverdue))]
        [NotifyPropertyChangedFor(nameof(IsNotOverdue))]
        [NotifyPropertyChangedFor(nameof(HasDueDate))]
        [NotifyPropertyChangedFor(nameof(DueDateText))]
        private DateTime? _dueOn;

        public bool IsNotOverdue => !IsOverdue && DueOn.HasValue;

        public bool IsOverdue
        {
            get
            {
                if (IsDone) return false;
                if (DueOn == null) return false;
                return DueOn.HasValue & DueOn < DateTime.Today;
            }
        }

        public bool HasDueDate => DueOn != null || IsDone;

        public string DueDateText
        {
            get
            {
                var text = string.Empty;
                if (IsNotDone)
                {
                    if (DueOn == null) return text;
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
                    else // Not overdue
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
                }
                else // IsDone
                {
                    text = $"Completed on {_todo.CompletedOn?.ToString("MM/dd/yyyy")}";
                }
                return text;
            }
        }

        private Todo _todo;
        public Todo TodoModel { get { return _todo; } }

        public TodoViewModel(Todo todo)
        {
            _todo = todo;
            Id = todo.Id;
            Title = todo.Title;

            // we don't use the property because we don't want to trigger the setter
            // that will change the completedOn date
            _isDone = todo.IsDone;

            Notes = todo.Notes;
            DueOn = todo.DueOn;
        }

    }
}
