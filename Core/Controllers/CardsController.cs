using CardShow.Core.Data;
using CardShow.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardShow.Core.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly IDbFixture fixture;

        public CardsController(
            IDbFixture fixture)
        {
            this.fixture = fixture;
        }

        [HttpGet]
        [Route("{setId}")]
        public IEnumerable<_Card> GetBySet([FromRoute]int setId)
        {
            var cards = fixture.GetCardsBySet(setId);
            return cards;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody]_Card card)
        {
            var createdCardId = await fixture.CreateCard(card);

            return StatusCode(200, createdCardId);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody]int id)
        {
            await fixture.DeleteCard(id);
            if (fixture.CardIsDeleted(id))
                return StatusCode(200);

            return StatusCode(500);
        }
    }
}
