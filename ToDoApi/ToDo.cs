namespace ToDoApi
{
    public class ToDo
    {
        public int Id { get; set; }

        public DateOnly CreateDate { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

        public required string Title { get; set; }

        public string? Description { get; set; }

        public DateOnly? DueDate { get; set; }
    }
}