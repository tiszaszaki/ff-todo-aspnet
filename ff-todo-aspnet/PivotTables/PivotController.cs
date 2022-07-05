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

        [HttpGet(TodoCommon.pivotLabel3)]
        public PivotResponse<LatestUpdateRecord> GetBoardLatestUpdate()
        {
            return pivotService.GetBoardLatestUpdate();
        }
        [HttpGet(TodoCommon.pivotLabel4)]
        public PivotResponse<LatestUpdateRecord> GetTodoLatestUpdate()
        {
            return pivotService.GetTodoLatestUpdate();
        }
    }
}
