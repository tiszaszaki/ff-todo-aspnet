using ff_todo_aspnet.Entities;
using Microsoft.EntityFrameworkCore;

namespace ff_todo_aspnet.Configurations
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>()
                .HasMany(b => b.todos)
                .WithOne(t => t.board)
                .HasForeignKey(t => t.boardId);

            modelBuilder.Entity<Todo>()
                .HasMany(to => to.tasks)
                .WithOne(ta => ta.todo)
                .HasForeignKey(ta => ta.todoId);
        }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Entities.Task> Tasks { get; set; }
    }
}
