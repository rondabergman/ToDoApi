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
                new ToDo { Title = "Buy Plane Tickets", Description = "Tickets to Paris", DueDate = new DateOnly(2026, 5, 1) },
                new ToDo { Title = "Finish Project Report", Description = "Write executive summary and prepare slides", DueDate = new DateOnly(2026, 2, 15) },
                new ToDo { Title = "Call Mom", Description = "Weekly check-in" },
                new ToDo { Title = "Pay Electricity Bill", Description = "Use online banking", DueDate = new DateOnly(2026, 2, 5) },
                new ToDo { Title = "Gym Session", Description = "Leg day workout", DueDate = new DateOnly(2026, 1, 30), IsCompleted = true },
                new ToDo { Title = "Schedule Oil Change", Description = "Call mechanic for appointment", DueDate = new DateOnly(2026, 4, 10) },
                new ToDo { Title = "Plan Birthday Party", Description = "Guest list, venue and cake", DueDate = new DateOnly(2026, 6, 12) }
            );
            db.SaveChanges();
        }
    }
}