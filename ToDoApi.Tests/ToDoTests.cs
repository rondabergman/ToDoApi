namespace ToDoApi.Tests
{
    public class ToDoTests
    {
        [Fact]
        public void CanCreate_NewToDo_With_Title_CreateDate_Description_DueDate()
        {
            var toDo = new ToDo
            {
                Title = "Sample Title",
                Description = "Description",
                DueDate = new DateOnly(2026, 12, 1)
            };

            Assert.NotNull(toDo);
            Assert.Equal(new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), toDo.CreateDate);
            Assert.Equal("Description", toDo.Description);
            Assert.Equal(new DateOnly(2026, 12, 1), toDo.DueDate);
        }

        [Fact]
        public void CanCreate_NewToDo_With_Only_A_Title()
        {
            var toDo = new ToDo
            {
                Title = "Sample Title"
            };

            Assert.NotNull(toDo);
            Assert.Equal(new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), toDo.CreateDate);
            Assert.Null(toDo.Description);
            Assert.Null(toDo.DueDate);
        }
    }
}
