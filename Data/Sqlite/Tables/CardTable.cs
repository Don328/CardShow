using CardShow.Data.Models;
using CardShow.Data.Sqlite.Schema;
using CardShow.Data.SqliteSchema;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.Sqlite.Tables
{
    internal static class CardTable
    {
        internal static async Task<IEnumerable<_Card>> GetCardsBySetId(SqliteConnection conn, int setId)
        {
            var cards = new List<_Card>();

            using (var command = conn.CreateCommand())
            {
                command.CommandText = ReadTable.CardsFromSet;
                ParamBuilder.Build(command, "@setId", setId);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cards.Add(new _Card(
                        id:         reader.GetInt32(0),
                        setId:      reader.GetInt32(1),
                        name:       reader.GetString(2),
                        setIndex:   reader.GetString(3)));
                }
            }
            
            return await Task.FromResult(cards);
        }

        internal static async Task<int> Create(
            SqliteConnection conn,
            _Card card)
        {
            int createdId;

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = CreateRow.Card;
                ParamBuilder.Build(cmd, "@setId", card.SetId);
                ParamBuilder.Build(cmd, "@name", card.Name);
                ParamBuilder.Build(cmd, "@setIndex", card.SetIndex);

                long rowid = (long)cmd.ExecuteScalar();
                createdId = (int)rowid;
            }

            return await Task.FromResult(createdId);
        }

        internal static async Task Delete(
            SqliteConnection conn, int id)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = DeleteRow.Card;
            ParamBuilder.Build(cmd, "@id", id);
            cmd.ExecuteNonQuery();

            await Task.CompletedTask;
        }

        internal static async Task<bool> Any(
            SqliteConnection conn,
            int setId)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = ReadTable.CardsFromSet;
            ParamBuilder.Build(cmd, "@setId", setId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }

        internal static async Task<bool> Exists(
            SqliteConnection conn,
            int id)
        {
            using var cmd = conn.CreateCommand();
            ParamBuilder.Build(cmd, "@id", id);
            cmd.CommandText = ReadRow.Card;
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
    }
}
