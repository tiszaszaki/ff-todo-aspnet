using Microsoft.AspNetCore.Mvc;
using ff_todo_aspnet.Constants;

namespace ff_todo_aspnet.PivotTables
{
    [ApiController]
    [Route(TodoCommon.pivotPath)]
    public class PivotController
    {
        private readonly IPivotService pivotService;

        public PivotController(IPivotService pivotService)
        {
            this.pivotService = pivotService;
        }

        [HttpGet(TodoCommon.pivotLabel1)]
        public PivotResponse<ReadinessRecord> GetBoardReadiness()
        {
            return pivotService.GetBoardReadiness();
        }
        [HttpGet(TodoCommon.pivotLabel2)]
        public PivotResponse<ReadinessRecord> GetTodoReadiness()
        {
            return pivotService.GetTodoReadiness();
        }
    }
}
