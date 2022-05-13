using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.SqliteSchema
{
    internal static class UpdateRow
    {
        const string Card =
            @"UPDATE Cards
            SET Status=@status
            WHERE Id=@id";
    }
}
