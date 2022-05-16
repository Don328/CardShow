using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.Sqlite.Schema
{
    internal static class CreateRow
    {
        internal const string Set =
            @"INSERT INTO CardSets(
                Year, Name, Sport)
            VALUES(
                @year, @name, @sport);
            SELECT last_insert_rowid();";

        internal const string Card =
            @"INSERT INTO Cards(
                SetId, Name, SetIndex)
            VALUES(
                @setId, @name, @setIndex);
            SELECT last_insert_rowid();";

        internal const string Assessment =
            @"INSERT INTO Assessments(
                CardId, Date, HighGrade,
                LowGrade, Text, Corners,
                Edges, Centering, Surface)
            VALUES(
                @cardId, @date, @highGrade,
                @lowGrade, @text, @corners,
                @edges, @centering, @surface);
            SELECT last_insert_rowid();";
    }
}
