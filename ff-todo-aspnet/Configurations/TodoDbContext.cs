using ff_todo_aspnet.Constants;
using ff_todo_aspnet.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ff_todo_aspnet.Configurations
{
    public class TodoDbContext : DbContext
    {
        public enum TodoDbEntityType
        {
            FFTODO_BOARD,
            FFTODO_TODO,
            FFTODO_TASK
        }
        public bool IsNameTruncated { get; set; }
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) {
            IsNameTruncated = false;
        }
        private bool doesNameExist(TodoDbEntityType entityType, string name)
        {
            bool res = true;
            switch (entityType)
            {
                case TodoDbEntityType.FFTODO_BOARD:
                    res &= Boards.Where(b => b.name == name).ToList().Count > 0;
                break;
                case TodoDbEntityType.FFTODO_TODO:
                    res &= Todos.Where(t => t.name == name).ToList().Count > 0;
                    break;
                case TodoDbEntityType.FFTODO_TASK:
                    res &= Tasks.Where(t => t.name == name).ToList().Count > 0;
                    break;
                default: break;
            }
            return res;
        }
        public string ReplaceNameToUnused(TodoDbEntityType entityType, string name, bool doCloning)
        {
            string res = name;
            while (doesNameExist(entityType, res))
            {
                string reNumPat = @"\d+", strNew;
                int matchCount, i = 0;
                matchCount = new Regex(reNumPat).Matches(res).Count;
                strNew = Regex.Replace(res, reNumPat, m => {
                    string res = m.Value;
                    if (i == matchCount - 1)
                        res = (long.Parse(res) + 1).ToString();
                    i++;
                    return res;
                });
                if (res == strNew)
                    res = strNew + " " + 2.ToString();
                else
                    res = strNew;
                if (doCloning)
                {
                    matchCount = new Regex(TodoCommon.TODO_CLONE_SUFFIX_REGEX).Matches(res).Count;
                    if (matchCount == 0)
                        res += TodoCommon.TODO_CLONE_SUFFIX;
                }
                IsNameTruncated = res.Length > TodoCommon.MAX_TODO_NAME_LENGTH;
                if (IsNameTruncated)
                {
                    var strTruncateIdx = TodoCommon.MAX_TODO_NAME_LENGTH / 2; 
                    var lengthOverrun = res.Length - TodoCommon.MAX_TODO_NAME_LENGTH; var lengthOverrunHalf = 0;
                    var truncatedRes = "";
                    lengthOverrun += TodoCommon.FIELD_TRUNCATE_STR.Length; lengthOverrunHalf = lengthOverrun / 2;
                    truncatedRes += res.Substring(0, strTruncateIdx - lengthOverrunHalf);
                    truncatedRes += TodoCommon.FIELD_TRUNCATE_STR;
                    truncatedRes += res.Substring(strTruncateIdx + lengthOverrun - lengthOverrunHalf);
                    res = truncatedRes;
                }
            }
            return res;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.LogTo(Console.WriteLine);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>()
                .HasMany(b => b.todos)
                .WithOne(t => t.board)
                .HasForeignKey(t => t.boardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Todo>()
                .HasOne(t => t.board)
                .WithMany(b => b.todos)
                .IsRequired();

            modelBuilder.Entity<Todo>()
                .HasMany(to => to.tasks)
                .WithOne(ta => ta.todo)
                .HasForeignKey(ta => ta.todoId)
                .OnDelete(DeleteBehavior.Cascade);

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
