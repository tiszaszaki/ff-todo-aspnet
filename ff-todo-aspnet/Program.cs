using Microsoft.EntityFrameworkCore;
using ff_todo_aspnet.Configurations;
using ff_todo_aspnet.Repositories;
using ff_todo_aspnet.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddDbContext<TodoDbContext>(x => x.UseNpgsql(connectionString));

builder.Services.AddScoped<BoardRepository>();
builder.Services.AddScoped<TodoRepository>();
builder.Services.AddScoped<TaskRepository>();

builder.Services.AddScoped<BoardService>();
builder.Services.AddScoped<TodoService>();
builder.Services.AddScoped<TaskService>();

builder.Services.AddControllers();

// logging
using ILoggerFactory loggerFactory =
    LoggerFactory.Create(builder =>
        builder.AddSimpleConsole(options =>
        {
            options.IncludeScopes = true;
            options.SingleLine = true;
            options.UseUtcTimestamp = true;
            //options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
        }));

builder.Services.AddLogging();

// setup CORS
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder => {
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.

//app cors
app.UseCors("corsapp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
