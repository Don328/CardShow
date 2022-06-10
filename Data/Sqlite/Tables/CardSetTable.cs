using CardShow.Data.Models;
using CardShow.Data.Sqlite.Schema;
using CardShow.Data.SqliteSchema;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.Sqlite.Tables
{
    internal static class CardSetTable
    {
        internal static async Task<int> Create(
            SqliteConnection conn, _CardSet set)
        {
            int createdId;

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = CreateRow.Set;
                ParamBuilder.Build(cmd, "@year", set.Year);
                ParamBuilder.Build(cmd, "@name", set.Name);
                ParamBuilder.Build(cmd, "@sport", set.Sport);
                long setId = (long)cmd.ExecuteScalar();
                createdId = (int)setId;
            }

            return await Task.FromResult(createdId);
        }

        internal static async Task DeleteSet(
            SqliteConnection conn, int id)
        {
            var hasCards = await CardTable.Any(conn, id);
            if (!hasCards)
            {
                using var cmd = conn.CreateCommand();
                cmd.CommandText = DeleteRow.Set;
                ParamBuilder.Build(cmd, "@id", id);
                cmd.ExecuteNonQuery();
            }

            await Task.CompletedTask;
        }

        internal static async Task<IEnumerable<_CardSet>> TryReadSets(
            SqliteConnection conn)
        {
            try
            {
                return await ReadSets(conn);
            }
            catch
            {
                DbPlant.CreateTable_CardSets(conn);
                DbPlant.SeedData_CardSets(conn);
                return await ReadSets(conn);
            }
        }

        internal static async Task<IEnumerable<_CardSet>> ReadSets(
            SqliteConnection conn)
        {
            var sets = new List<_CardSet>();

            using (var command = conn.CreateCommand())
            {
                command.CommandText = ReadTable.CardSets;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sets.Add(new _CardSet(
                            id:     reader.GetInt32(0),
                            year:   reader.GetInt32(1),
                            name:   reader.GetString(2),
                            sport:  reader.GetInt32(3)));
                    }
                };
            }

            return await Task.FromResult(sets);
        }
    }
}
