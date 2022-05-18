using CardShow.Data;
using CardShow.Data.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CardShow.Core.Data
{
    public class DbFixture : IDisposable, IDbFixture
    {
        private SqliteConnection conn;

        internal ICardShowDbContext Context { get; private set; }
        private ILogger<DbFixture> logger;


        public DbFixture(
            ILogger<DbFixture> logger,
            IConfiguration config)
        {
            this.logger = logger;
            var connStr = "Data Source=:memory:";
            // var connStr = config.GetConnectionString("Sqlite_File");

            this.conn = new SqliteConnection(connStr);
            logger.LogInformation("Creating db context");
            Context = new CardShowDbContext(conn);
        }

        public IEnumerable<_CardSet> GetAllCardSets()
        {
            logger.LogInformation("Getting Card Sets");
            return Context.Sets;
        }

        public async Task<int> CreateSet(_CardSet set)
        {
            logger.LogInformation($"Creating new Card Set [{set.Year} {set.Name}]");
            return await Context.CreateSet(set);
        }

        public async Task DeleteSet(int id)
        {
            var set = (from s in Context.Sets
                    where s.Id == id
                    select s).FirstOrDefault();

            if (set != null)
            {
                logger.LogInformation($"Attempting to delete set (id:{id})");
                await Context.DeleteSet(id);
            }
        }

        public async Task<bool> SetIsDeleted(int id)
        {
            logger.LogInformation($"Checking if Set (id:{id}) is deleted");
            var exists = Context.Sets.Where(s =>
                s.Id == id).Any();
            logger.LogInformation($"set (id:{id}) isDeleted:{!exists}");

            return await Task.FromResult(!exists);
        }

        public async Task<IEnumerable<_Card>> GetCardsBySet(int setId)
        {
            logger.LogInformation($"Requesting Cards from set (id:{setId})");
            return await Context.GetCardsBySetId(setId);
        }

        public async Task<int> CreateCard(_Card card)
        {
            logger.LogInformation($"Creating new Card for: {card.Name}");
            return await Context.CreateCard(card);
        }

        public async Task DeleteCard(int id)
        {
            logger.LogInformation($"Deleting Card (id:{id})");
            await Context.DeleteCard(id);
        }

        public async Task<bool> CardIsDeleted(int id)
        {
            logger.LogInformation($"Checking if Card (id:{id}) is deleted");
            var exists = await Context.CardExists(id);


            logger.LogInformation($"Card (id:{id}) isDeleted:{!exists}");
            return !exists;
        }

        public async Task<IEnumerable<_Assessment>> GetCardAssessments(int cardId)
        {
            logger.LogInformation($"Requesting assessments for card (id:{cardId})");
            return await Context.GetCardAssessments(cardId);
        }

        public async Task<int> CreateAssessment(_Assessment assessment)
        {
            logger.LogInformation($"Creating new assessment for card (id:{assessment.CardId})");
            return await Context.CreateAssessment(assessment);
        }

        public async Task DeleteAssessment(int id)
        {
            logger.LogInformation($"Deleting Assessment (id:{id})");
            await Context.DeleteAssessment(id);
        }

        public async Task<bool> AssessmentIsDeleted(int id)
        {
            logger.LogInformation($"Checking if Assessment (id:{id}) is deleted");
            var exists = await Context.AssessmentExists(id);

            logger.LogInformation($"Assessment (id:{id}) isDeleted:{!exists}");
            return !exists;
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
