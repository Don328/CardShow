using CardShow.Data.Models;
using CardShow.Data.SqliteSchema;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.Contexts.Tables
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
                    while(reader.Read())
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
    }
}
