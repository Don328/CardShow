using CardShow.Core.Data;
using CardShow.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardShow.Core.Controllers
{
    [Route("api/sets")]
    [ApiController]
    public class CardSetsController : ControllerBase
    {
        private readonly ILogger<CardSetsController> logger;
        private readonly IDbFixture fixture;

        public CardSetsController(
            ILogger<CardSetsController> logger,
            IDbFixture fixture)
        {
            this.logger = logger;
            this.fixture = fixture;
        }

        [HttpGet]
        public IEnumerable<_CardSet> Get()
        {
            logger.LogInformation("Recieved request for Card sets (Get all)");
            return fixture.GetAllCardSets();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody]_CardSet set)
        {
            logger.LogInformation(
                $"Recieved a request to create a " +
                $"new Card Set: {set.Year} {set.Name}");
            try
            {
                var id = await fixture.CreateSet(set);
                logger.LogInformation($"Card Set created (id:{id}) returning 200 OK");
                return StatusCode(200, id);
            }
            catch
            {
                logger.LogInformation($"Something went wrong creating CardSet entry");
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody]int id)
        {
            logger.LogInformation($"Recieved a request to delete CardSet (id:{id})");
            await fixture.DeleteSet(id);

            logger.LogInformation("Checking that delete was successful");
            if(fixture.SetIsDeleted(id))
            {
                logger.LogInformation("Returning 200 OK");
                return StatusCode(200);
            }

            logger.LogInformation("The CardSet was not deleted. Returning 500");
            return StatusCode(500);
        }
    }
}
