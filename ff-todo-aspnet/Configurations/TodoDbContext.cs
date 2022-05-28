using Microsoft.EntityFrameworkCore;
using tiszaszaki_asp_webapp_2022.Entities;

namespace tiszaszaki_asp_webapp_2022.Configurations
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        { }
        public DbSet<Todo> Todos { get; set; }
    }
}
