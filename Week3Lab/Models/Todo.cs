namespace Week3Lab.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public string Title { get; set; }
        public string? Notes { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? DueOn { get; set; } = null;
        public DateTime? CompletedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }

    }
}
