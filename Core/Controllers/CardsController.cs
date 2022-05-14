using CardShow.Core.Data;
using CardShow.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardShow.Core.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardShowDbFixture fixture;

        public CardsController(
            ICardShowDbFixture fixture)
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
    }
}
