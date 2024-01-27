using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Net;
using WebAPI.ApiControllers;
using static WebAPI.ApiControllers.ExampleEndpoints;

namespace WebAPI
{
    public static class ServiceDependencyInjection
    {
        public static string LOGIN_PATH = "/login";
        public static string LOGOUT_PATH = "/logout";

        public static IServiceCollection RegisterWebApi(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGenWithBearer();
           
            services.AddScoped<MyService>();
           
            services.AddCors(p =>
                p.AddPolicy("ANY_ORIGIN", builder =>
                {
                    builder
                        .WithOrigins("*")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                })
            );

            return services;
        }

        public static WebApplication SetupApplication(this WebApplication application)
        {
            if (application.Environment.IsDevelopment() || application.Environment.IsProduction())
            {
                application.UseSwagger();
                application.UseSwaggerUI();
            }

            application.UseSerilogRequestLogging(configure =>
            {
                configure.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000}ms";
            });

            application.UseCors("ANY_ORIGIN");
            application.UseHttpsRedirection();

            application.UseMyAuthorization();

            application.MapEndpoints();
            application.MapAiExampleEndpoints();
            application.MapExampleEndpoints();
            application.TryMigrateAndSeedData();

            return application;
        }

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
                var isLoginOrLogoutPath = (context.Request.Path.Value?.Contains(LOGIN_PATH) ?? false) || 
                    (context.Request.Path.Value?.Contains(LOGOUT_PATH) ?? false);
                if (isLoginOrLogoutPath == false)
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
}
