﻿using CardShow.Data.Contexts;
using CardShow.Data.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CardShow.Core.Data
{
    public class SqliteFixture : IDisposable, ICardShowDbFixture
    {
        private SqliteConnection conn;
        private readonly IConfiguration config;

        internal ICardShowDbContext Context { get; private set; }



        public SqliteFixture(IConfiguration config)
        {
            var connStr = "Data Source=:memory:";
            // var connStr = config.GetConnectionString("Sqlite_File");

            this.conn = new SqliteConnection(connStr);
            Context = new SqliteContext(conn);
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

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }
    }
}
