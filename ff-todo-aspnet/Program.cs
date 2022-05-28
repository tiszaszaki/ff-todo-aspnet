using tiszaszaki_asp_webapp_2022.Repositories;
using tiszaszaki_asp_webapp_2022.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<TodoRepository>();
builder.Services.AddScoped<TodoService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
