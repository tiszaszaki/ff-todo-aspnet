﻿using ff_todo_aspnet.Configurations;

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
        private PivotResponse<ReadinessRecord> ResultReadinessPivot(IEnumerable<ReadinessRecord> records)
        {
            foreach (var r in records)
                r.doneTaskPercent = ReadinessRecord.GetPercent(r.doneTaskCount, r.taskCount);
            var res = new PivotResponse<ReadinessRecord>
            {
                fields = PivotResponseTools.ExtractFieldsFromType(typeof(ReadinessRecord)),
                fieldOrder = PivotResponseTools.ExtractFieldOrderFromType(typeof(ReadinessRecord)),
                fieldDisplay = PivotResponseTools.ExtractFieldDisplayFromType(typeof(ReadinessRecord)),
                records = records
            };
            return res;
        }
        private PivotResponse<LatestUpdateRecord> ResultLatestUpdatePivot(IEnumerable<LatestUpdateRecord> records)
        {
            var res = new PivotResponse<LatestUpdateRecord>
            {
                fields = PivotResponseTools.ExtractFieldsFromType(typeof(LatestUpdateRecord)),
                fieldOrder = PivotResponseTools.ExtractFieldOrderFromType(typeof(LatestUpdateRecord)),
                fieldDisplay = PivotResponseTools.ExtractFieldDisplayFromType(typeof(LatestUpdateRecord)),
                records = records
            };
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
            var records = new List<ReadinessRecord>(foundKeys.ToList());
            if (remainingKeys.Count() > 0)
            {
                foreach (var e in remainingKeys)
                    records.Add(new ReadinessRecord
                    {
                        id = e.id,
                        name = e.name,
                        doneTaskCount = 0,
                        taskCount = 0
                    });
            }
            /*
            var records = context.Boards.Select(board => new ReadinessRecord
            {
                id = board.id,
                name = board.name,
                doneTaskCount = board.doneTaskCount(),
                taskCount = board.taskCount()
            }).AsEnumerable();
            */
            return ResultReadinessPivot(records);
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
            var records = new List<ReadinessRecord>(foundKeys.ToList());
            if (remainingKeys.Count() > 0)
            {
                foreach (var e in remainingKeys)
                    records.Add(new ReadinessRecord
                    {
                        id = e.id,
                        name = e.name,
                        doneTaskCount = 0,
                        taskCount = 0
                    });
            }
            /*
            var records = context.Todos.Select(todo => new ReadinessRecord
            {
                id = todo.id,
                name = todo.name,
                doneTaskCount = todo.doneTaskCount(),
                taskCount = todo.taskCount()
            }).AsEnumerable();
            */
            return ResultReadinessPivot(records);
        }

        public PivotResponse<LatestUpdateRecord> FetchBoardLatestUpdate()
        {
            var records = context.Boards.Select(board => new LatestUpdateRecord
            {
                id = board.id,
                name = board.name,
                latestUpdated = board.latestUpdated,
                latestEvent = board.latestEvent.ToString(),
                affectedId = board.affectedId,
                affectedName = board.affectedName
            }).AsEnumerable();
            return ResultLatestUpdatePivot(records);
        }
        public PivotResponse<LatestUpdateRecord> FetchTodoLatestUpdate()
        {
            var records = context.Todos.Select(todo => new LatestUpdateRecord
            {
                id = todo.id,
                name = todo.name,
                latestUpdated = todo.latestUpdated,
                latestEvent = todo.latestEvent.ToString(),
                affectedId = todo.affectedId,
                affectedName = todo.affectedName
            }).AsEnumerable();
            return ResultLatestUpdatePivot(records);
        }
    }
}
