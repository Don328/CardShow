using CardShow.Data.SqliteSchema;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.Contexts
{
    internal static class SqliteDbPlant
    {
        internal static void CreateTable_CardSets(
            SqliteConnection conn)
        {
            (new SqliteCommand(CreateTable.CardSets, conn)).ExecuteNonQuery();
        }

        internal static void SeedData_CardSets(
            SqliteConnection conn)
        {
            (new SqliteCommand(SeedData.CardSet_1, conn)).ExecuteNonQuery();
            (new SqliteCommand(SeedData.CardSet_2, conn)).ExecuteNonQuery();
            (new SqliteCommand(SeedData.CardSet_3, conn)).ExecuteNonQuery();
        }
    }
}
