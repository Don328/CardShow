using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using System.Text;
using System.Threading.Tasks;
using CardShow.Data.Models;
using CardShow.Data.SqliteSchema;
using System.Data;
using CardShow.Data.Contexts.Tables;

namespace CardShow.Data.Contexts
{
    public class SqliteContext : IDisposable, ICardShowDbContext
    {
        private readonly SqliteConnection conn;

        public IEnumerable<_CardSet> Sets { get; private set; }

        public SqliteContext(SqliteConnection conn)
        {
            this.conn = conn;
            conn.Open();
            Sets = CardSetTable.TryReadSets(conn);
        }

        public async Task<int> CreateSet(_CardSet set)
        {
            var id = await CardSetTable.Create(conn, set);

            RefreshSetsList();
            return await Task.FromResult(id);
        }

        public async Task DeleteSet(int id)
        {
            await CardSetTable.DeleteSet(conn, id);
            RefreshSetsList();
            await Task.CompletedTask;
        }

        private void RefreshSetsList()
        {
            Sets = new List<_CardSet>();
            Sets = CardSetTable.ReadSets(conn);
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
