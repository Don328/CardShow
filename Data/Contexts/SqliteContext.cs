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
            Sets = TryReadSets();
        }

        public async Task<int> CreateSet(_CardSet set)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = CreateRow.Set;
                AddParameter(cmd, "@year", set.Year);
                AddParameter(cmd, "@name", set.Name);
                long setId = (long)cmd.ExecuteScalar();
                set.Id = (int)setId;
            }

            RefreshSetsList();
            return await Task.FromResult(set.Id);
        }

        public async Task DeleteSet(int id)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = DeleteRow.Set;
            AddParameter(cmd, "@id", id);
            cmd.ExecuteNonQuery();

            RefreshSetsList();
            await Task.CompletedTask;
        }

        private IEnumerable<_CardSet> TryReadSets()
        {
            try
            {
                return ReadSets();
            }
            catch
            {
                SqliteDbPlant.CreateTable_CardSets(conn);
                SqliteDbPlant.SeedData_CardSets(conn);
                return ReadSets();
            }
        }

        private IEnumerable<_CardSet> ReadSets()
        {
            var sets = new List<_CardSet>();

            using (var command = conn.CreateCommand())
            {
                command.CommandText = ReadTable.CardSets;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sets.Add(new _CardSet()
                        {
                            Id = reader.GetInt32(0),
                            Year = reader.GetInt32(1),
                            Name = reader.GetString(2),
                        });
                    }
                };
            }

            return sets;
        }

        private void RefreshSetsList()
        {
            Sets = new List<_CardSet>();
            Sets = ReadSets();
        }

        private void AddParameter(
            DbCommand cmd,
            string name,
            object value)
        {
            var p = cmd.CreateParameter();
            if (value == null)
            {
                throw new ArgumentNullException("Parameter value cannot be null");
            }

            var type = value.GetType();
            if (type == typeof(int))
                p.DbType = DbType.Int32;
            else if (type == typeof(string))
                p.DbType = DbType.String;
            else
                throw new ArgumentException(
                    $"Unrecognized Type: {type}");

            p.Direction = ParameterDirection.Input;
            p.ParameterName = name;
            p.Value = value;
            cmd.Parameters.Add(p);
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
