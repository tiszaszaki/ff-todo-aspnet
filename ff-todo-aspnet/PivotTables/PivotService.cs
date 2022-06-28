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
        public IEnumerable<BoardReadinessResponse> GetBoardReadiness()
        {
            IEnumerable<BoardReadinessResponse> result = pivotRepository.FetchBoardReadiness();
            logger.LogInformation("Fetched {0} Board(s) with readiness", result.Count());
            return result;
        }
    }
}
