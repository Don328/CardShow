using CardShow.Data.Sqlite.Schema;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.Sqlite
{
    internal static class DbPlant
    {
        internal static void CreateTable_CardSets(
            SqliteConnection conn)
        {
            new SqliteCommand(CreateTable.CardSets, conn).ExecuteNonQuery();
            new SqliteCommand(CreateTable.Cards, conn).ExecuteNonQuery();
        }

        internal static void SeedData_CardSets(
            SqliteConnection conn)
        {
            new SqliteCommand(SeedData.CardSet_1, conn).ExecuteNonQuery();
            new SqliteCommand(SeedData.CardSet_2, conn).ExecuteNonQuery();
            new SqliteCommand(SeedData.CardSet_3, conn).ExecuteNonQuery();
            new SqliteCommand(SeedData.CardSet_4, conn).ExecuteNonQuery();
            new SqliteCommand(SeedData.CardSet_5, conn).ExecuteNonQuery();

            new SqliteCommand(SeedData.Card_1, conn).ExecuteNonQuery();
            new SqliteCommand(SeedData.Card_2, conn).ExecuteNonQuery();
            new SqliteCommand(SeedData.Card_3, conn).ExecuteNonQuery();
            new SqliteCommand(SeedData.Card_4, conn).ExecuteNonQuery();
            new SqliteCommand(SeedData.Card_5, conn).ExecuteNonQuery();
            new SqliteCommand(SeedData.Card_6, conn).ExecuteNonQuery();
            new SqliteCommand(SeedData.Card_7, conn).ExecuteNonQuery();
            new SqliteCommand(SeedData.Card_8, conn).ExecuteNonQuery();
            new SqliteCommand(SeedData.Card_9, conn).ExecuteNonQuery();
            new SqliteCommand(SeedData.Card_10, conn).ExecuteNonQuery();
            new SqliteCommand(SeedData.Card_11, conn).ExecuteNonQuery();
            new SqliteCommand(SeedData.Card_12, conn).ExecuteNonQuery();
        }
    }
}
