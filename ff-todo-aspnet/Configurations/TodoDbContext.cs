using ff_todo_aspnet.Entities;
using Microsoft.EntityFrameworkCore;
using tiszaszaki_asp_webapp_2022.Entities;

namespace tiszaszaki_asp_webapp_2022.Configurations
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>()
                .HasMany(b => b.todos)
                .WithOne(t => t.board)
                .HasForeignKey(t => t.boardId);
        }
        public DbSet<Todo> Todos { get; set; }
    }
}
