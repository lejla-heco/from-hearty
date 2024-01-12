using static ExampleEndpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<MyService>();
builder.Services.AddDbContext<MyContext>();

var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapEndpoints();
app.MapAiExampleEndpoints();
// app.MapExampleEndpoints();
app.TryMigrateAndSeedData();

app.Run();
