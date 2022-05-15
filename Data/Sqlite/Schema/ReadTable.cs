using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.SqliteSchema
{
    internal static class ReadTable
    {
        internal const string CardSets =
            @"SELECT
                Id,
                Year,
                Name,
                Sport 
            From CardSets";

        internal const string CardsFromSet =
            @"SELECT
                Id,
                SetId,
                Name,
                SetIndex
            FROM Cards
            WHERE SetId=@setId";

        internal const string CardsByPlayer =
            @"SELECT
                Id,
                SetId,
                Name,
                SetIndex
            FROM Cards
            WHERE Name=@name";
    }
}
