using ff_todo_aspnet.Constants;

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
            label.Trim();
            if (label == "") label = "default-pivot";
            logger.LogDebug($"{label}-records: ", result.records.ToString());
            logger.LogDebug($"{label}-fields: ", result.fields.ToString());
        }
        public PivotResponse<ReadinessRecord> GetBoardReadiness()
        {
            var result = pivotRepository.FetchBoardReadiness();
            logger.LogInformation("Fetched {0} Board(s) with readiness", result.records.Count());
            LogFetchingDebug(result, TodoCommon.pivotLabel1);
            return result;
        }
        public PivotResponse<ReadinessRecord> GetTodoReadiness()
        {
            var result = pivotRepository.FetchTodoReadiness();
            logger.LogInformation("Fetched {0} Todos(s) with readiness", result.records.Count());
            LogFetchingDebug(result, TodoCommon.pivotLabel2);
            return result;
        }

        public PivotResponse<LatestUpdateRecord> GetBoardLatestUpdate()
        {
            var result = pivotRepository.FetchBoardLatestUpdate();
            logger.LogInformation("Fetched {0} Board(s) with latest update", result.records.Count());
            LogFetchingDebug(result, TodoCommon.pivotLabel3);
            return result;
        }
        public PivotResponse<LatestUpdateRecord> GetTodoLatestUpdate()
        {
            var result = pivotRepository.FetchTodoLatestUpdate();
            logger.LogInformation("Fetched {0} Todos(s) with latest update", result.records.Count());
            LogFetchingDebug(result, TodoCommon.pivotLabel4);
            return result;
        }
    }
}
