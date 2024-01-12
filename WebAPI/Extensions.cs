using Microsoft.EntityFrameworkCore;

public static class ModelExtensions
{
    //Ref: https://stackoverflow.com/questions/55970148/apply-entity-framework-migrations-when-using-asp-net-core-in-a-docker-image
    public static void TryMigrateAndSeedData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<MyContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            Console.WriteLine("Applying migrations...");
            context.Database.Migrate();
            Console.WriteLine("Seeding data...");
            context.SetUpData();
        }
    }
}