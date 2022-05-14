using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.SqliteSchema
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
    }
}
