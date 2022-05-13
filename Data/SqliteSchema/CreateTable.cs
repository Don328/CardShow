using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.SqliteSchema
{
    internal static class CreateTable
    {
        internal const string CardSets =
            @"CREATE TABLE IF NOT EXISTS
                CardSets(
                    Id      INTEGER PRIMARY KEY,
                    Year    INTEGER NOT NULL,
                    Name    VARCHAR(255) NOT NULL)";
    }
}
