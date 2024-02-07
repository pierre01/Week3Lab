namespace Week3Lab.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public string Title { get; set; }

        //public override string ToString()
        //{
        //    return Title;
        //}

        //public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? DueOn { get; set; } = DateTime.Now;
        //public DateTime CompletedOn { get; set; }
    }
}
