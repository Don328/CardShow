using CardShow.Core.Data;
using CardShow.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CardShow.Test.CS_Core
{
    public class DataFixtureTests
    {
        private readonly DbFixture fixture;

        public DataFixtureTests()
        {
            var logFactory = new LoggerFactory();
            var logger = new Logger<DbFixture>(logFactory);

            var connString = new Dictionary<string, string>()
            { ["ConnectionString"] = "Data Source=:memory:" };
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(connString);
            var config = builder.Build();

            fixture = new DbFixture(logger, config);
        }

        [Fact]
        public void Test_DbEntryExists()
        {
            Assert.False(fixture.SetIsDeleted(1));
        }

        [Fact]
        public void Test_GetAllCardSets()
        {
            var sets = fixture.GetAllCardSets();
            Assert.Equal(6, sets.Count());
        }

        [Fact]
        public async void Test_CreateAndDelteSet()
        {
            var set = new _CardSet()
            {
                Name = "Fleer",
                Year = 1989,
                Sport = 3
            };

            Assert.True(fixture.SetIsDeleted(7));
            var id = await fixture.CreateSet(set);
            Assert.False(fixture.SetIsDeleted(7));
            await fixture.DeleteSet(id);
            Assert.True(fixture.SetIsDeleted(7));
        }

        [Fact]
        public void Test_GetCardsBySet()
        {
            Assert.NotEmpty(fixture.GetCardsBySet(1));
            Assert.Empty(fixture.GetCardsBySet(6));
        }

        [Fact]
        public async void Test_CreateAndDeleteCard()
        {
            var card = new _Card()
            {
                SetId = 1,
                Name = "Test Card",
                SetIndex = "1"
            };

            Assert.True(fixture.CardIsDeleted(13));
            await fixture.CreateCard(card);
            Assert.False(fixture.CardIsDeleted(13));
            await fixture.DeleteCard(13);
            Assert.True(fixture.CardIsDeleted(13));
        }

        [Fact]
        public async void Test_DeleteSetOrphanConstraint()
        {
            Assert.False(fixture.SetIsDeleted(5));
            var cards = fixture.GetCardsBySet(5);
            
            // Set should not delete if cards are orphaned
            await fixture.DeleteSet(5);
            Assert.False(fixture.SetIsDeleted(5));
            
            foreach(var card in cards)
            {
                await fixture.DeleteCard(card.Id);
            }

            // Set should delete when no cards are attached
            await fixture.DeleteSet(5);
            Assert.True(fixture.SetIsDeleted(5));
        }
    }
}
