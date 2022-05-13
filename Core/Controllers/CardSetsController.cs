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

        [HttpDelete]
        [Route("/{id}")]
        public void Get(int id)
        {
            fixture.DeleteSet(id);
        }
    }
}
