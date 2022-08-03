using Microsoft.EntityFrameworkCore;
using ff_todo_aspnet.Configurations;

namespace ff_todo_aspnet.PivotTables
{
    public class PivotRepository : IPivotRepository
    {
        private readonly TodoDbContext context;

        public PivotRepository(TodoDbContext context)
        {
            this.context = context;
        }

        private PivotResponse<ReadinessRecord> ResultReadinessPivot(IEnumerable<ReadinessRecord> records)
        {               
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
            var records_with_todos = context.Boards.Include(board => board.todos);
            var records_with_tasks = records_with_todos.ThenInclude(todo => todo.tasks);
            var records = records_with_tasks.Select(board => new ReadinessRecord
            {
                id = board.id,
                name = board.name,
                doneTaskCount = board.doneTaskCount,
                taskCount = board.taskCount,
                doneTaskPercent = board.doneTaskPercent
        }).AsEnumerable();
            return ResultReadinessPivot(records);
        }
        public PivotResponse<ReadinessRecord> FetchTodoReadiness()
        {
            var records_with_tasks = context.Todos.Include(todo => todo.tasks);
            var records = records_with_tasks.Select(todo => new ReadinessRecord
            {
                id = todo.id,
                name = todo.name,
                doneTaskCount = todo.doneTaskCount,
                taskCount = todo.taskCount,
                doneTaskPercent = todo.doneTaskPercent
            }).AsEnumerable();
            return ResultReadinessPivot(records);
        }

        public PivotResponse<LatestUpdateRecord> FetchBoardLatestUpdate()
        {
            var records_with_todos = context.Boards.Include(board => board.todos);
            var records_with_tasks = records_with_todos.ThenInclude(todo => todo.tasks);
            var records = records_with_tasks.Select(board => new LatestUpdateRecord
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
            var records_with_tasks = context.Todos.Include(todo => todo.tasks);
            var records = records_with_tasks.Select(todo => new LatestUpdateRecord
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
