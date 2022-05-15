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
    internal class CardTable
    {
        internal static IEnumerable<_Card> GetCardsBySetId(SqliteConnection conn, int setId)
        {
            var cards = new List<_Card>();

            using (var command = conn.CreateCommand())
            {
                command.CommandText = ReadTable.CardsFromSet;
                ParamBuilder.Build(command, "@setId", setId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cards.Add(new _Card()
                        {
                            Id = reader.GetInt32(0),
                            SetId = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            SetIndex = reader.GetString(3)
                        });
                    }
                }
            }

            return cards;
        }

        internal static async Task<int> Create(
            SqliteConnection conn,
            _Card card)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = CreateRow.Card;
                ParamBuilder.Build(cmd, "@setId", card.SetId);
                ParamBuilder.Build(cmd, "@name", card.Name);
                ParamBuilder.Build(cmd, "@setIndex", card.SetIndex);

                long cardId = (long)cmd.ExecuteScalar();
                card.Id = (int)cardId;

            }

            return await Task.FromResult(card.Id);
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

        internal static bool Any(
            SqliteConnection conn,
            int setId)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = ReadTable.CardsFromSet;
            ParamBuilder.Build(cmd, "@setId", setId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return true;
            }

            return false;
        }

        internal static bool Exists(
            SqliteConnection conn,
            int id)
        {
            using var cmd = conn.CreateCommand();
            ParamBuilder.Build(cmd, "@id", id);
            cmd.CommandText = ReadRow.Card;
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return true;
            }

            return false;
        }
    }
}
