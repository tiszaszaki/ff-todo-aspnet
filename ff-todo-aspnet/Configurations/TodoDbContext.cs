﻿using ff_todo_aspnet.Entities;
using Microsoft.EntityFrameworkCore;

namespace ff_todo_aspnet.Configurations
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>()
                .HasKey(b => b.id);

            modelBuilder.Entity<Todo>()
                .HasKey(t => t.id);

            modelBuilder.Entity<Entities.Task>()
                .HasKey(t => t.id);

            modelBuilder.Entity<Board>()
                .HasMany(b => b.todos)
                .WithOne(t => t.board)
                .HasForeignKey(t => t.boardId);

            modelBuilder.Entity<Todo>()
                .HasOne(t => t.board)
                .WithMany(b => b.todos)
                .IsRequired();

            modelBuilder.Entity<Todo>()
                .HasMany(to => to.tasks)
                .WithOne(ta => ta.todo)
                .HasForeignKey(ta => ta.todoId);

            modelBuilder.Entity<Entities.Task>()
                .HasOne(ta => ta.todo)
                .WithMany(to => to.tasks)
                .IsRequired();
        }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Entities.Task> Tasks { get; set; }
    }
}
