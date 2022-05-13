using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.SqliteSchema
{
    internal static class CreateRow
    {
        const string Set =
        @"INSERT INTO CardSets(
            Year, Name)
        VALUES(
            @year, @name);
        SELECT last_insert_rowid();";
    }
}
