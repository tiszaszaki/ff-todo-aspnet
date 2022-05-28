using Microsoft.EntityFrameworkCore;
using ff_todo_aspnet.Configurations;
using ff_todo_aspnet.Repositories;
using ff_todo_aspnet.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TodoDbContext>(x => x.UseNpgsql(connectionString));

builder.Services.AddScoped<BoardRepository>();
builder.Services.AddScoped<TodoRepository>();
builder.Services.AddScoped<TaskRepository>();

builder.Services.AddScoped<BoardService>();
builder.Services.AddScoped<TodoService>();
builder.Services.AddScoped<TaskService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
