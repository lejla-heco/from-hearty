using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

public static class Extensions
{
    public static string LOGIN_PATH = "/login";

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

    public static void AddSwaggerGenWithBearer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(config =>
        {
            config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            config.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });
        });
    }

    public static void UseMyAuthorization(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            var isLoginPath = context.Request.Path.Value?.Contains(LOGIN_PATH);
            if (isLoginPath == false)
            {
                var loginService = app.Services.GetService<LoginService>()!;
                var token = context.Request.Headers["Authorization"].FirstOrDefault();
                if (token != null)
                {
                    if (loginService.isTokenValid(new Guid(token)) == false)
                    {
                        context.Response.Clear();
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        await context.Response.WriteAsync("Unauthorized");
                        return;
                    }
                }
                else
                {
                    context.Response.Clear();
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    await context.Response.WriteAsync("Login token required");
                    return;
                }
            }
            await next.Invoke();
        });
    }
}