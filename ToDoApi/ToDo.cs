namespace ToDoApi
{
    public class ToDo
    {
        public int Id { get; set; }

        public DateOnly CreateDate { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

        private string _title = string.Empty;
        public required string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Title cannot be empty or whitespace.", nameof(Title));
                }

                _title = value;
            }
        }
        public string? Description { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateOnly? DueDate { get; set; }
    }
}