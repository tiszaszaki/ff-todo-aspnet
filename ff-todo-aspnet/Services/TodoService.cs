using ff_todo_aspnet.Entities;
using ff_todo_aspnet.Repositories;
using ff_todo_aspnet.RequestObjects;
using ff_todo_aspnet.ResponseObjects;

namespace ff_todo_aspnet.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository todoRepository;
        private readonly ILogger<TodoService> logger;
        public TodoService(ITodoRepository todoRepository, ILogger<TodoService> logger)
        {
            this.todoRepository = todoRepository;
            this.logger = logger;
        }
        public IEnumerable<TodoResponse> GetTodos()
        {
            IEnumerable<TodoResponse> result = todoRepository.FetchTodos();
            logger.LogInformation("Fetched {0} Todo(s)", result.Count());
            return result;
        }
        public IEnumerable<TodoResponse> GetAllTodosFromBoard(long boardId)
        {
            IEnumerable<TodoResponse> result = todoRepository.FetchAllTodosFromBoard(boardId);
            logger.LogInformation("Fetched {0} Todo(s) from Board with ID ({1})", result.ToList().Count, boardId);
            return result;
        }
        public TodoResponse? GetTodo(long id)
        {
            TodoResponse? result = todoRepository.FetchTodo(id);
            if (result is not null)
                logger.LogInformation("Successfully fetched Todo with ID ({0}): {1}", id, result.ToString());
            else
                logger.LogError("Failed to fetch Todo with ID ({0})", id);
            return result;
        }
        public TodoResponse? GetTodoByName(string name)
        {
            TodoResponse? result = todoRepository.FetchTodoByName(name);
            if (result is not null)
                logger.LogInformation("Successfully fetched Todo with name \"{0}\": {1}", name, result.ToString());
            else
                logger.LogError("Failed to fetch Todo with name \"{0}\"", name);
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
            logger.LogInformation("Successfully added new Todo: {0}", addedTodo.ToString());
            return todo;
        }
        public Todo? RemoveTodo(long id)
        {
            Todo? todo = todoRepository.RemoveTodo(id);
            if (todo is not null)
                logger.LogInformation("Successfully removed Todo with ID ({0})", id);
            else
                logger.LogError("Failed to remove Todo with ID ({0})", id);
            return todo;
        }
        public void RemoveAllTodos()
        {
            var todoCount = todoRepository.RemoveAllTodos();
            if (todoCount > 0)
                logger.LogInformation("Successfully removed {0} Todo(s)", todoCount);
            else
                logger.LogWarning("No Todos were removed");
        }
        public void RemoveAllTodosFromBoard(long boardId)
        {
            var todoCount = todoRepository.RemoveAllTodosFromBoard(boardId);
            if (todoCount > 0)
                logger.LogInformation("Successfully removed {0} Todos from Board with ID ({1})", todoCount, boardId);
            else
                logger.LogWarning("No Todos were removed from Board with ID ({0})", boardId);
        }
        public TodoResponse? UpdateTodo(long id, TodoRequest patchRequest)
        {
            Todo patchedTodo = patchRequest;
            TodoResponse? persistedTodo;
            patchedTodo.dateModified = FetchNewDateTime();
            persistedTodo = todoRepository.UpdateTodo(id, patchedTodo);
            if (persistedTodo is not null)
                logger.LogInformation("Successfully updated Todo with ID ({0}): {1}", id, persistedTodo.ToString());
            else
                logger.LogError("Failed to update Todo with ID ({0})", id);
            return persistedTodo;
        }
        public Todo? CloneTodo(long id, int phase, long boardId)
        {
            Todo? result = todoRepository.CloneTodo(id, phase, boardId, FetchNewDateTime(), FetchNewDateTime());
            if (result is not null)
            {
                if (todoRepository.IsNameTruncated)
                    logger.LogWarning("Truncated name of Todo with ID ({0}) from \"{1}\" to \"{2}\"",
                        id, todoRepository.CloneTodoOldName, todoRepository.CloneTodoNewName);
                logger.LogInformation("Successfully cloned Todo with ID ({0}): {1}", id, result.ToString());
            }
            else
                logger.LogError("Failed to clone Todo with ID ({0})", id);
            return result;
        }
    }
}
