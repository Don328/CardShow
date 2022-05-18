using CardShow.Core.Data;
using CardShow.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardShow.Core.Controllers
{
    [Route("api/assessments")]
    [ApiController]
    public class AssessmentsController : ControllerBase
    {
        private readonly ILogger<AssessmentsController> logger;
        private readonly IDbFixture fixture;

        public AssessmentsController(
            ILogger<AssessmentsController> logger,
            IDbFixture fixture)
        {
            this.logger = logger;
            this.fixture = fixture;
        }

        [HttpGet]
        [Route("{cardId}")]
        public async Task<IEnumerable<_Assessment>> GetByCard([FromRoute] int cardId)
        {
            logger.LogInformation(
                $"Recieved request to get Assessment " +
                $"list for card (id:{cardId})");

            return await fixture.GetCardAssessments(cardId);
       }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] _Assessment assessment)
        {
            logger.LogInformation(
                $"Recieved a request to " +
                $"create a new Assessment for card (Id:{assessment.CardId})");
            var assessmentId = await fixture.CreateAssessment(assessment);
            logger.LogInformation($"Assessment Created (id:{assessmentId}) Returning 200 OK");

            return StatusCode(200, assessmentId);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            logger.LogInformation($"Recieved a request to delete assessment (id:{id})");
            await fixture.DeleteAssessment(id);

            logger.LogInformation($"Checking that Assessment was deleted");
            var isDeleted = await fixture.AssessmentIsDeleted(id);
            if (isDeleted)
            {
                logger.LogInformation("Delete successful. Returning 200 OK");
                return StatusCode(200);
            }

            logger.LogInformation("Assessment was not deleleted. Returning 500");
            return StatusCode(500);
        }
    }
}
