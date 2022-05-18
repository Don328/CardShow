using CardShow.Core.Data;
using CardShow.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardShow.Core.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ILogger<CardsController> logger;
        private readonly IDbFixture fixture;

        public CardsController(
            ILogger<CardsController> logger,
            IDbFixture fixture)
        {
            this.logger = logger;
            this.fixture = fixture;
        }

        [HttpGet]
        [Route("{setId}")]
        public async Task<IEnumerable<_Card>> GetBySet([FromRoute]int setId)
        {
            logger.LogInformation(
                $"Recieved request to get Card " +
                $"list for set (id:{setId})");
            return await fixture.GetCardsBySet(setId);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody]_Card card)
        {
            logger.LogInformation(
                $"Recieved a request to " +
                $"create a new Card for {card.Name}");
            var createdCardId = await fixture.CreateCard(card);
            logger.LogInformation($"Card Created (id:{createdCardId}) Returning 200 OK");

            return StatusCode(200, createdCardId);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody]int id)
        {
            logger.LogInformation($"Recieved a request to delete Card (id:{id})");
            await fixture.DeleteCard(id);

            logger.LogInformation($"Checking that Card was deleted");

            var isDeleted = await fixture.CardIsDeleted(id);
            if (isDeleted)
            {
                logger.LogInformation("Delete successful. Returning 200 OK");
                return StatusCode(200);
            }

            logger.LogInformation("Card was not deleleted. Returning 500");
            return StatusCode(500);
        }
    }
}
