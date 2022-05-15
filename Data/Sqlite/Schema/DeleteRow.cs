using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.Sqlite.Schema
{
    internal static class DeleteRow
    {
        internal const string Set =
            @"DELETE FROM CardSets
            WHERE Id=@id";

        internal const string Card =
            @"DELETE FROM Cards
            WHERE Id=@id";
    }
}
