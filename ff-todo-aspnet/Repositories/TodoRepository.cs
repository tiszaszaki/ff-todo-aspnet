using ff_todo_aspnet.Configurations;
using ff_todo_aspnet.Constants;
using ff_todo_aspnet.Entities;
using ff_todo_aspnet.ResponseObjects;
using System.Text.RegularExpressions;

namespace ff_todo_aspnet.Repositories
{
    public class TodoRepository
    {
        private readonly TodoDbContext context;
        public TodoRepository(TodoDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<TodoResponse> FetchTodos()
        {
            return context.Todos.Select<Todo, TodoResponse>(todo => todo);
        }
        public TodoResponse FetchTodo(long id)
        {
            return context.Todos.Single(todo => todo.id == id);
        }
        public Todo AddTodo(Todo todo)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
            return todo;
        }
        public void RemoveTodo(long id)
        {
            var todo = context.Todos.Single(todo => todo.id == id);
            context.Todos.Remove(todo);
            context.SaveChanges();
        }
        public void RemoveAllTodos()
        {
            context.Todos.RemoveRange(context.Todos);
        }
        public void RemoveAllTodosFromBoard(long boardId)
        {
            foreach (var todo in context.Todos.Where(todo => todo.boardId == boardId))
                context.Todos.Remove(todo);
        }
        public void UpdateTodo(long id, Todo patchedTodo)
        {
            var todo = context.Todos.Single(todo => todo.id == id);
            todo.name = patchedTodo.name;
            todo.description = patchedTodo.description;
            todo.phase = patchedTodo.phase;
            todo.deadline = patchedTodo.deadline;
            context.SaveChanges();
        }
        private string replaceNameToUnused(long id)
        {
            string res = context.Todos.Single(todo => todo.id == id).name;
            while (context.Todos.Where(todo => todo.name == res).ToArray().Length > 0)
            {
                string strNew; var i = 0;
                string reNumPat = @"\d+";
                var matchCount = new Regex(reNumPat).Matches(res).Count;
                strNew = Regex.Replace(res, reNumPat, m => {
                    string res = m.Value;
                    if (i == matchCount - 1)
                        res = (long.Parse(res) + 1).ToString();
                    i++;
                    return res;
                });
                if (res == strNew)
                    res = strNew + " " + 2.ToString() + TodoCommon.TODO_CLONE_SUFFIX;
                else
                    res = strNew;
            }
            return res;
        }
        public Todo CloneTodo(long id, int phase, long boardId, DateTime dateCreatedNew, DateTime dateModifiedNew)
        {
            Todo todo = context.Todos.Single(todo => todo.id == id);
            todo.id = context.Todos.Max(todo => todo.id) + 1;
            todo.name = replaceNameToUnused(id);
            todo.phase = phase;
            todo.dateCreated = dateCreatedNew;
            todo.dateModified = dateModifiedNew;
            todo.boardId = boardId;
            context.Todos.Add(todo);
            context.SaveChanges();
            return context.Todos.Single(clonedTodo => clonedTodo.name == todo.name);
        }
    }
}
