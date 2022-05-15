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
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = CreateRow.Set;
                SqliteParameter.Build(cmd, "@year", set.Year);
                SqliteParameter.Build(cmd, "@name", set.Name);
                SqliteParameter.Build(cmd, "@sport", set.Sport);
                long setId = (long)cmd.ExecuteScalar();
                set.Id = (int)setId;
            }

            return await Task.FromResult(set.Id);
        }

        internal static async Task DeleteSet(
            SqliteConnection conn, int id)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = DeleteRow.Set;
            SqliteParameter.Build(cmd, "@id", id);
            cmd.ExecuteNonQuery();

            await Task.CompletedTask;
        }

        internal static IEnumerable<_CardSet> TryReadSets(
            SqliteConnection conn)
        {
            try
            {
                return ReadSets(conn);
            }
            catch
            {
                SqliteDbPlant.CreateTable_CardSets(conn);
                SqliteDbPlant.SeedData_CardSets(conn);
                return ReadSets(conn);
            }
        }

        internal static IEnumerable<_CardSet> ReadSets(
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
                        sets.Add(new _CardSet()
                        {
                            Id = reader.GetInt32(0),
                            Year = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Sport = reader.GetInt32(3)
                        });
                    }
                };
            }

            return sets;
        }
    }
}
