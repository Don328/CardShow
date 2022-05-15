using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.Sqlite.Schema
{
    internal static class CreateTable
    {
        internal const string CardSets =
            @"CREATE TABLE IF NOT EXISTS
                CardSets(
                    Id      INTEGER PRIMARY KEY,
                    Year    INTEGER NOT NULL,
                    Name    VARCHAR(255) NOT NULL,
                    Sport   INTEGER NOT NULL)";

        internal const string Cards =
            @"CREATE TABLE IF NOT EXISTS
                Cards(
                    Id          INTEGER PRIMARY KEY,
                    SetId       INTEGER NOT NULL,
                    Name        VARCHAR(255) NOT NULL,
                    SetIndex    VARCHAR(255),
                    FOREIGN KEY(SetId)
                        REFERENCES CardSets(Id))";
    }
}
