using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.SqliteSchema
{
    internal static class ReadRow
    {
        const string CardSet =
            @"SELECT 
            Id,
            Year
            Name
            FROM CardSets
            WHERE Id=@id";
    }
}
