using CardShow.Data.Models;
using CardShow.Data.SqliteSchema;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.Contexts.Tables
{
    internal static class CardSetTable
    {
        internal static async Task<int> Create(
            SqliteConnection conn, _CardSet set)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = CreateRow.Set;
                AddParameter(cmd, "@year", set.Year);
                AddParameter(cmd, "@name", set.Name);
                AddParameter(cmd, "@sport", set.Sport);
                long setId = (long)cmd.ExecuteScalar();
                set.Id = (int)setId;
            }

            return await Task.FromResult(set.Id);
        }

        internal static async Task DeleteSet(
            SqliteConnection conn, int id)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = DeleteRow.Set;
            AddParameter(cmd, "@id", id);
            cmd.ExecuteNonQuery();

            await Task.CompletedTask;
        }

        internal static  IEnumerable<_CardSet> TryReadSets(
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

        private static void AddParameter(
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
    }
}
