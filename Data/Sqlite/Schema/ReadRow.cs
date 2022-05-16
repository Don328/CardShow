using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.Sqlite.Schema
{
    internal static class ReadRow
    {
        internal const string CardSet =
            @"SELECT 
            Id,
            Year,
            Name,
            Sport 
            FROM CardSets
            WHERE Id=@id";

        internal const string Card =
            @"SELECT
                Id,
                SetId,
                Name,
                SetIndex
            FROM Cards
            WHERE Id=@id";

        internal const string Assessment =
            @"SELECT
                Id,
                CardId,
                Date,
                HighGrade,
                LowGrade,
                Text,
                Corners,
                Edges,
                Centering,
                Surface
            FROM Assessments
            WHERE Id=@id";
    }
}
