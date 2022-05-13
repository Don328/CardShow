using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.SqliteSchema
{
    internal static class ReadTable
    {
        public static readonly string CardSets =
            @"SELECT
            Id,
            Year,
            Name
            From CardSets";
    }
}
