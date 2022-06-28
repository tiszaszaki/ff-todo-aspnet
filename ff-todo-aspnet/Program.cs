using Microsoft.EntityFrameworkCore;
using ff_todo_aspnet.Configurations;
using ff_todo_aspnet.Repositories;
using ff_todo_aspnet.Services;
using ff_todo_aspnet.PivotTables;

var builder = WebApplication.CreateBuilder(args);
var isRealDatabase = Boolean.Parse(builder.Configuration["IsRealDatabase"]);

// Add services to the container.

if (isRealDatabase)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<TodoDbContext>(x => x.UseNpgsql(connectionString));
}
else
{
    var databaseName = "ff-todo-inmemory-" + Guid.NewGuid().ToString();
    builder.Services.AddDbContext<TodoDbContext>(x => x.UseInMemoryDatabase(databaseName: databaseName));
}

builder.Services.AddScoped<IBoardRepository, BoardRepository>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IPivotRepository, PivotRepository>();

builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IPivotService, PivotService>();

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
