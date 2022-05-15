using CardShow.Data;
using CardShow.Data.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CardShow.Core.Data
{
    public class DbFixture : IDisposable, IDbFixture
    {
        private SqliteConnection conn;
        private readonly IConfiguration config;

        internal ICardShowDbContext Context { get; private set; }



        public DbFixture(IConfiguration config)
        {
            var connStr = "Data Source=:memory:";
            // var connStr = config.GetConnectionString("Sqlite_File");

            this.conn = new SqliteConnection(connStr);
            Context = new CardShowDbContext(conn);
        }

        public IEnumerable<_CardSet> GetAllCardSets()
        {
            return Context.Sets;
        }

        public async Task<int> CreateSet(_CardSet set)
        {
            return await Context.CreateSet(set);
        }

        public async Task DeleteSet(int id)
        {
            var set = (from s in Context.Sets
                    where s.Id == id
                    select s).FirstOrDefault();

            if (set != null)
            {
                await Context.DeleteSet(id);
            }
        }

        public bool SetIsDeleted(int id)
        {
            return !Context.Sets.Where(s =>
                s.Id == id).Any();
        }

        public IEnumerable<_Card> GetCardsBySet(int setId)
        {
            return Context.GetCardsBySetId(setId);
        }

        public async Task<int> CreateCard(_Card card)
        {
            return await Context.CreateCard(card);
        }

        public async Task DeleteCard(int id)
        {
            await Context.DeleteCard(id);
        }

        public bool CardIsDeleted(int id)
        {
            return !Context.CardExists(id);
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }
    }
}
