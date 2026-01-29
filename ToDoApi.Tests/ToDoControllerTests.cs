using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Controllers;

namespace ToDoApi.Tests
{
    public class ToDoControllerTests
    {
        //Helper method that builds an isolated InMemory DB per test to avoid cross-test pollution.
        private static ToDoDbContext CreateContextWithSeed(params ToDo[] items)
        {
            var options = new DbContextOptionsBuilder<ToDoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var ctx = new ToDoDbContext(options);
            ctx.Database.EnsureCreated();

            if (items != null && items.Any())
            {
                ctx.ToDos.AddRange(items);
                ctx.SaveChanges();
            }

            return ctx;
        }

        [Fact]
        public async Task Get_ReturnsSeededTodos()
        {
            using var ctx = CreateContextWithSeed(
                new ToDo { Title = "A" },
                new ToDo { Title = "B" },
                new ToDo { Title = "C" }
            );

            var controller = new ToDoController(ctx);

            var action = await controller.Get();

            var ok = Assert.IsType<OkObjectResult>(action.Result);
            var todos = Assert.IsAssignableFrom<System.Collections.Generic.IEnumerable<ToDo>>(ok.Value);

            Assert.Equal(3, todos.Count());
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenMissing()
        {
            using var ctx = CreateContextWithSeed(); // empty DB
            var controller = new ToDoController(ctx);

            var action = await controller.Get(999);

            Assert.IsType<NotFoundResult>(action.Result);
        }

        [Fact]
        public async Task GetById_ReturnsTodo_WhenExists()
        {
            using var ctx = CreateContextWithSeed(new ToDo { Title = "Exists" });

            var existing = ctx.ToDos.First();
            var controller = new ToDoController(ctx);

            var action = await controller.Get(existing.Id);

            var ok = Assert.IsType<OkObjectResult>(action.Result);
            var todo = Assert.IsType<ToDo>(ok.Value);

            Assert.Equal(existing.Id, todo.Id);
            Assert.Equal("Exists", todo.Title);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAndPersists()
        {
            using var ctx = CreateContextWithSeed(); // empty DB
            var controller = new ToDoController(ctx);

            var newToDo = new ToDo { Title = "New item", Description = "desc" };

            var action = await controller.Create(newToDo);

            var created = Assert.IsType<CreatedAtRouteResult>(action.Result);
            var returned = Assert.IsType<ToDo>(created.Value);

            // database now contains the item
            var inDb = ctx.ToDos.Find(returned.Id);
            Assert.NotNull(inDb);
            Assert.Equal("New item", inDb.Title);
            Assert.Equal("desc", inDb.Description);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_And_UpdatesEntity()
        {
            using var ctx = CreateContextWithSeed(new ToDo { Title = "Old" });
            var existing = ctx.ToDos.First();
            var controller = new ToDoController(ctx);

            var updated = new ToDo
            {
                Id = existing.Id,
                Title = "Updated",
                Description = "Updated desc",
                DueDate = new DateOnly(2026, 12, 12)
            };

            var result = await controller.Update(existing.Id, updated);

            Assert.IsType<NoContentResult>(result);

            var inDb = ctx.ToDos.Find(existing.Id);
            Assert.Equal("Updated", inDb.Title);
            Assert.Equal("Updated desc", inDb.Description);
            Assert.Equal(new DateOnly(2026, 12, 12), inDb.DueDate);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenMissing()
        {
            using var ctx = CreateContextWithSeed();
            var controller = new ToDoController(ctx);

            var updated = new ToDo { Id = 999, Title = "X" };
            var result = await controller.Update(999, updated);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_RemovesEntity_AndReturnsNoContent()
        {
            using var ctx = CreateContextWithSeed(new ToDo { Title = "ToDelete" });
            var existing = ctx.ToDos.First();
            var controller = new ToDoController(ctx);

            var result = await controller.Delete(existing.Id);

            Assert.IsType<NoContentResult>(result);
            Assert.Null(ctx.ToDos.Find(existing.Id));
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenMissing()
        {
            using var ctx = CreateContextWithSeed();
            var controller = new ToDoController(ctx);

            var result = await controller.Delete(12345);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
