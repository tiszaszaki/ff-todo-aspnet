using ff_todo_aspnet.Configurations;
using System.Collections.ObjectModel;

namespace ff_todo_aspnet.PivotTables
{
    public class PivotRepository : IPivotRepository
    {
        private readonly TodoDbContext context;

        public PivotRepository(TodoDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<BoardReadinessResponse> FetchBoardReadiness()
        {
            var foundKeys = context.Boards
                .Join(
                    context.Todos,
                    board => board.id,
                    todo => todo.boardId,
                    (board, todo) => new
                    {
                        boardId = board.id,
                        name = board.name,
                        todoId = todo.id
                    }
                )
                .Join(
                    context.Tasks,
                    boardTodo => boardTodo.todoId,
                    task => task.todoId,
                    (boardTodo, task) => new
                    {
                        boardId = boardTodo.boardId,
                        name = boardTodo.name,
                        todoId = boardTodo.todoId,
                        taskId = task.id,
                        done = task.done
                    }
                )
                .GroupBy(boardTodoTask => new { id = boardTodoTask.boardId, name = boardTodoTask.name })
                .Select(groupedBoardTodoTask => new BoardReadinessResponse
                    {
                        id = groupedBoardTodoTask.Key.id,
                        name = groupedBoardTodoTask.Key.name,
                        doneTaskCount = groupedBoardTodoTask.Count(boardTodoTask => boardTodoTask.done),
                        TaskCount = groupedBoardTodoTask.Count()
                    }
                );
            var remainingKeys = context.Boards
                .Select(board => new { id = board.id, name = board.name })
                .Except(foundKeys.Select(e => new { id = e.id, name = e.name }));
            var res = new Collection<BoardReadinessResponse>(foundKeys.ToList());
            foreach (var e in remainingKeys)
                res.Add(new BoardReadinessResponse
                {
                    id = e.id,
                    name = e.name,
                    doneTaskCount = 0,
                    TaskCount = 0
                });
            return res;
        }
    }
}
