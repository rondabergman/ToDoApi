using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using ToDoApi;

public static class DataSeedExtension
{
    public static void SeedData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();

        db.Database.EnsureCreated();

        if (!db.ToDos.Any())
        {
            db.ToDos.AddRange(
                new ToDo { Title = "Go Grocery Shopping", Description = "Buy milk, butter, sugar" },
                new ToDo { Title = "Take Dog to Vet", DueDate = new DateOnly(2026, 3, 1) },
                new ToDo { Title = "buy Plane Tickets", Description = "Tickets to Paris", DueDate = new DateOnly(2026, 5, 1) }
            );
            db.SaveChanges();
        }
    }
}