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
        private readonly ICardShowDbFixture fixture;

        public CardSetsController(
            ICardShowDbFixture fixture)
        {
            this.fixture = fixture;
        }

        [HttpGet]
        public IEnumerable<_CardSet> Get()
        {
            return fixture.GetAllCardSets();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]_CardSet set)
        {
            await fixture.CreateSet(set);
            return StatusCode(200);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody]int id)
        {
            await fixture.DeleteSet(id);
            return StatusCode(200);
        }
    }
}
