﻿using CardShow.Data.Models;
using CardShow.Data.Sqlite.Tables;
using Microsoft.Data.Sqlite;

namespace CardShow.Data
{
    public class CardShowDbContext : IDisposable, ICardShowDbContext
    {
        private readonly SqliteConnection conn;

        public IEnumerable<_CardSet> Sets { get; private set; }

        public CardShowDbContext(SqliteConnection conn)
        {
            this.conn = conn;
            conn.Open();
            Sets = CardSetTable.TryReadSets(conn).Result;
        }

        public async Task<int> CreateSet(_CardSet set)
        {
            var id = await CardSetTable.Create(conn, set);
            await RefreshSetsList();
            return await Task.FromResult(id);
        }

        public async Task DeleteSet(int id)
        {
            await CardSetTable.DeleteSet(conn, id);
            await RefreshSetsList();
        }

        private async Task RefreshSetsList()
        {
            Sets = new List<_CardSet>();
            Sets = await CardSetTable.ReadSets(conn);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<_Card>> GetCardsBySetId(int setId)
        {
            return await CardTable.GetCardsBySetId(conn, setId);
        }

        public async Task<int> CreateCard(_Card card)
        {
            return await CardTable.Create(conn, card);
        }

        public async Task DeleteCard(int id)
        {
            var assessments = AssessmentTable.GetCardAssesments(conn, id);
            if (assessments.Any())
            {
                foreach (var assessment in assessments)
                {
                    await AssessmentTable.Delete(conn, assessment.Id);
                }
            }

            await CardTable.Delete(conn, id);
        }

        public async Task<bool> CardExists(int id)
        {
            return await CardTable.Exists(conn, id);
        }

        public async Task<IEnumerable<_Assessment>> GetCardAssessments(int cardId)
        {
            return await Task.FromResult(
                AssessmentTable.GetCardAssesments(conn, cardId));
        }

        public async Task<int> CreateAssessment(_Assessment assessment)
        {
            return await AssessmentTable.Create(conn, assessment);
        }

        public async Task DeleteAssessment(int id)
        {
            await AssessmentTable.Delete(conn, id);
        }

        public async Task<bool> AssessmentExists(int id)
        {
            return await AssessmentTable.Exists(conn, id);
        }

        public void Dispose()
        {
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
