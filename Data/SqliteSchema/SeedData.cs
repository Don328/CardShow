using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.SqliteSchema
{
    internal static class SeedData
    {
        public const string CardSet_1 =
            @"INSERT INTO CardSets(
                Id,
                Year,
                Name)
            VALUES(
                1,
                1990,
                'Topps')";

        public const string CardSet_2 =
            @"INSERT INTO CardSets(
                Id,
                Year,
                Name)
            VALUES(
                2,
                1991,
                'Donruss')";

        public const string CardSet_3 =
            @"INSERT INTO CardSets(
                Id,
                Year,
                Name)
            VALUES(
                3,
                1990,
                'Fleer')";
    }
}
