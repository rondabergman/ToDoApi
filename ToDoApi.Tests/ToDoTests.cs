namespace ToDoApi.Tests;

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

    [Fact]
    public void CreateDate_RemainsSame_AfterModifyingProperties()
    {
        var toDo = new ToDo
        {
            Title = "Immutable createdate check"
        };

        var originalCreateDate = toDo.CreateDate;

        // modify other properties
        toDo.Description = "Changed";
        toDo.DueDate = new DateOnly(2026, 11, 11);
        toDo.Id = 7;
        toDo.Title = "Changed Title";

        Assert.Equal(originalCreateDate, toDo.CreateDate);
    }

    [Fact]
    public void DueDate_CanBe_Set_And_Cleared()
    {
        var toDo = new ToDo
        {
            Title = "DueDate test"
        };

        Assert.Null(toDo.DueDate);

        var due = new DateOnly(2026, 8, 15);
        toDo.DueDate = due;
        Assert.Equal(due, toDo.DueDate);

        toDo.DueDate = null;
        Assert.Null(toDo.DueDate);
    }

    [Fact]
    public void Title_DoesNot_AllowEmptyString_Or_Whitespace()
    {
        // empty string
        Assert.Throws<ArgumentException>(() => new ToDo { Title = string.Empty });

        // whitespace-only should also be rejected
        Assert.Throws<ArgumentException>(() => new ToDo { Title = "   " });
    }
}
