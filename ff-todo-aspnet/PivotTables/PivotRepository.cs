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

        private class PivotPrimaryKey
        {
            public long id { get; set; }
            public string name { get; set; }
        }
        private PivotResponse<ReadinessRecord> ResultReadinessPivot(IEnumerable<ReadinessRecord> foundKeys, IEnumerable<PivotPrimaryKey> remainingKeys)
        {
            var records = new Collection<ReadinessRecord>(foundKeys.ToList());
            foreach (var e in remainingKeys)
                records.Add(new ReadinessRecord
                {
                    id = e.id,
                    name = e.name,
                    doneTaskCount = 0,
                    taskCount = 0
                });
            foreach (var r in records)
            {
                if (r.taskCount != 0)
                    r.doneTaskPercent = (double) r.doneTaskCount / r.taskCount;
                else
                    r.doneTaskPercent = -1;
            }
            var res = new PivotResponse<ReadinessRecord>
            {
                fields = PivotResponse<ReadinessRecord>.ExtractFieldsFromType(typeof(ReadinessRecord)),
                fieldOrder = ReadinessRecord.fieldOrder,
                records = records
            };
            foreach (var f in res.fieldOrder)
            {
                var role = ReadinessRecord.fieldRoles[f].Trim();
                if (role != "")
                {
                    var temp = res.fields.Single(e => e.Key == f);
                    var key = temp.Key;
                    var value = temp.Value;
                    res.fields.Remove(temp);
                    value += $",{role}";
                    res.fields.Add(new KeyValuePair<string, string>(key, value));
                }
            }
            return res;
        }

        public PivotResponse<ReadinessRecord> FetchBoardReadiness()
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
                .Select(groupedBoardTodoTask => new ReadinessRecord
                    {
                        id = groupedBoardTodoTask.Key.id,
                        name = groupedBoardTodoTask.Key.name,
                        doneTaskCount = groupedBoardTodoTask.Count(boardTodoTask => boardTodoTask.done),
                        taskCount = groupedBoardTodoTask.Count()
                    }
                );
            var remainingKeys = context.Boards
                .Select(board => new PivotPrimaryKey { id = board.id, name = board.name })
                .Except(foundKeys.Select(e => new PivotPrimaryKey { id = e.id, name = e.name }));
            return ResultReadinessPivot(foundKeys, remainingKeys);
        }
        public PivotResponse<ReadinessRecord> FetchTodoReadiness()
        {
            var foundKeys = context.Todos
                .Join(
                    context.Tasks,
                    todo => todo.id,
                    task => task.todoId,
                    (todo, task) => new
                    {
                        todoId = todo.id,
                        name = todo.name,
                        taskId = task.id,
                        done = task.done
                    }
                )
                .GroupBy(todoTask => new PivotPrimaryKey { id = todoTask.todoId, name = todoTask.name })
                .Select(groupedTodoTask => new ReadinessRecord
                {
                    id = groupedTodoTask.Key.id,
                    name = groupedTodoTask.Key.name,
                    doneTaskCount = groupedTodoTask.Count(boardTodoTask => boardTodoTask.done),
                    taskCount = groupedTodoTask.Count()
                }
            );
            var remainingKeys = context.Todos
                .Select(todo => new PivotPrimaryKey { id = todo.id, name = todo.name })
                .Except(foundKeys.Select(e => new PivotPrimaryKey { id = e.id, name = e.name }));
            return ResultReadinessPivot(foundKeys, remainingKeys);
        }
    }
}
