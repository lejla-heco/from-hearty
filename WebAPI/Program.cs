using static ExampleEndpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<MyService>();
builder.Services.AddDbContext<BloggingContext>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapEndpoints();
app.MapAiEndpoints();
app.MapExampleEndpoints();
app.Run();
