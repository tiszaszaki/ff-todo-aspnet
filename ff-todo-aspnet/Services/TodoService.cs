using ff_todo_aspnet.Entities;
using ff_todo_aspnet.Repositories;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;

namespace ff_todo_aspnet.Services
{
    public class TodoService
    {
        private readonly TodoRepository todoRepository;
        public TodoService(TodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }
        public IEnumerable<TodoResponse> GetTodos()
        {
            IEnumerable<TodoResponse> result = todoRepository.FetchTodos();
            Console.WriteLine("Fetched {0} Todo(s)", result.Count());
            return result;
        }
        public IEnumerable<TodoResponse> GetAllTodosFromBoard(long boardId)
        {
            IEnumerable<TodoResponse> result = todoRepository.FetchAllTodosFromBoard(boardId);
            Console.WriteLine("Fetched {0} Todo(s) from Board with ID ({1})", result.ToList().Count, boardId);
            return result;
        }
        public TodoResponse GetTodo(long id)
        {
            TodoResponse result = todoRepository.FetchTodo(id);
            Console.WriteLine("Successfully fetched Todo with ID ({0}): {1}", id, result.ToString());
            return result;
        }
        public TodoResponse GetTodoByName(string name)
        {
            TodoResponse result = todoRepository.FetchTodoByName(name);
            Console.WriteLine("Successfully fetched Todo with name \"{0}\": {1}", name, result.ToString());
            return result;
        }
        private DateTime FetchNewDateTime()
        {
            return DateTime.UtcNow;
        }
        public Todo AddTodo(long boardId, TodoRequest todoRequest)
        {
            Todo todo = todoRequest;
            TodoResponse addedTodo;
            todo.dateCreated = FetchNewDateTime();
            todo.dateModified = FetchNewDateTime();
            todo.boardId = boardId;
            addedTodo = todoRepository.AddTodo(todo);
            Console.WriteLine("Successfully added new Todo: {0}", addedTodo.ToString());
            return todo;
        }
        public void RemoveTodo(long id)
        {
            todoRepository.RemoveTodo(id);
            Console.WriteLine("Successfully removed Todo with ID {0}", id);
        }
        public void RemoveAllTodos()
        {
            todoRepository.RemoveAllTodos();
            Console.WriteLine("Successfully removed all Todos");
        }
        public void RemoveAllTodosFromBoard(long boardId)
        {
            todoRepository.RemoveAllTodosFromBoard(boardId);
            Console.WriteLine("Successfully removed all Todos from Board with ID ({0})", boardId);
        }
        public void UpdateTodo(long id, TodoRequest patchRequest)
        {
            Todo patchedTodo = patchRequest;
            TodoResponse persistedTodo;
            patchedTodo.dateModified = FetchNewDateTime();
            persistedTodo = todoRepository.UpdateTodo(id, patchedTodo);
            Console.WriteLine("Successfully updated Todo with ID {0}: {1}", id, persistedTodo.ToString());
        }
        public Todo CloneTodo(long id, int phase, long boardId)
        {
            Todo result = todoRepository.CloneTodo(id, phase, boardId, FetchNewDateTime(), FetchNewDateTime());
            Console.WriteLine("Successfully cloned Todo with ID {0}: {1}", id, result.ToString());
            return result;
        }
    }
}
