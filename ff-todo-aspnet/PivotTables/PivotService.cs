using ff_todo_aspnet.Constants;
using System.Collections.ObjectModel;

namespace ff_todo_aspnet.PivotTables
{
    public class PivotService : IPivotService
    {
        private readonly IPivotRepository pivotRepository;
        private readonly ILogger<PivotService> logger;

        public PivotService(IPivotRepository pivotRepository, ILogger<PivotService> logger)
        {
            this.pivotRepository = pivotRepository;
            this.logger = logger;
        }
        private void LogFetchingDebug<T>(PivotResponse<T> result, string label)
        {
            if ((result.records is not null) && (result.fields is not null))
            {
                label.Trim();
                if (label == "") label = "default-pivot";
                logger.LogDebug($"{label}-records: ", result.records.ToString());
                logger.LogDebug($"{label}-fields: ", result.fields.ToString());
            }
        }
        public PivotResponse<ReadinessRecord> GetBoardReadiness()
        {
            var result = pivotRepository.FetchBoardReadiness();
            var count = (result.records ?? new Collection<ReadinessRecord>()).Count();
            logger.LogInformation("Fetched {0} Board(s) with readiness", count);
            LogFetchingDebug(result, TodoCommon.pivotLabel1);
            return result;
        }
        public PivotResponse<ReadinessRecord> GetTodoReadiness()
        {
            var result = pivotRepository.FetchTodoReadiness();
            var count = (result.records ?? new Collection<ReadinessRecord>()).Count();
            logger.LogInformation("Fetched {0} Todos(s) with readiness", count);
            LogFetchingDebug(result, TodoCommon.pivotLabel2);
            return result;
        }

        public PivotResponse<LatestUpdateRecord> GetBoardLatestUpdate()
        {
            var result = pivotRepository.FetchBoardLatestUpdate();
            var count = (result.records ?? new Collection<LatestUpdateRecord>()).Count();
            logger.LogInformation("Fetched {0} Board(s) with latest update", count);
            LogFetchingDebug(result, TodoCommon.pivotLabel3);
            return result;
        }
        public PivotResponse<LatestUpdateRecord> GetTodoLatestUpdate()
        {
            var result = pivotRepository.FetchTodoLatestUpdate();
            var count = (result.records ?? new Collection<LatestUpdateRecord>()).Count();
            logger.LogInformation("Fetched {0} Todos(s) with latest update", count);
            LogFetchingDebug(result, TodoCommon.pivotLabel4);
            return result;
        }
    }
}
