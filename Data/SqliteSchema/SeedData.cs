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
                Name,
                Sport)
            VALUES(
                1,
                1990,
                'Topps',
                1)";

        public const string CardSet_2 =
            @"INSERT INTO CardSets(
                Id,
                Year,
                Name,
                Sport)
            VALUES(
                2,
                1991,
                'Donruss',
                1)";

        public const string CardSet_3 =
            @"INSERT INTO CardSets(
                Id,
                Year,
                Name,
                Sport)
            VALUES(
                3,
                1990,
                'Fleer',
                1)";
        
        public const string CardSet_4 =
            @"INSERT INTO CardSets(
                Id,
                Year,
                Name,
                Sport)
            VALUES(
                4,
                1990,
                'NBA Hoops',
                2)";

        public const string CardSet_5 =
            @"INSERT INTO CardSets(
                Id,
                Year,
                Name,
                Sport)
            VALUES(
                5,
                1990,
                'Pro Set',
                3)";
    }
}
