using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.Sqlite.Schema
{
    internal static class SeedData
    {
        internal const string CardSet_1 =
            @"INSERT INTO CardSets(
                Id,
                Year,
                Name,
                Sport)
            VALUES(
                1,
                1988,
                'Topps',
                1)";

        internal const string CardSet_2 =
            @"INSERT INTO CardSets(
                Id,
                Year,
                Name,
                Sport)
            VALUES(
                2,
                1991,
                'Upper Deck',
                1)";

        public const string CardSet_3 =
            @"INSERT INTO CardSets(
                Id,
                Year,
                Name,
                Sport)
            VALUES(
                3,
                2001,
                'Upper Deck',
                5)";

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
                1991,
                'Topps',
                3)";

        public const string CardSet_6 =
            @"INSERT INTO CardSets(
                Id,
                Year,
                Name,
                Sport)
            VALUES(
                6,
                1991,
                'Score',
                4)";

        public const string Card_1 =
            @"INSERT INTO Cards(
                Id,
                SetId,
                Name,
                SetIndex)
            VALUES(
                1,
                4,
                'Michael Jordan',
                '65')";

        public const string Card_2 =
            @"INSERT INTO Cards(
                Id,
                SetId,
                Name,
                SetIndex)
            VALUES(
                2,
                2,
                'Ken Griffey Jr.',
                '424')";

        public const string Card_3 =
            @"INSERT INTO Cards(
                Id,
                SetId,
                Name,
                SetIndex)
            VALUES(
                3,
                3,
                'Tiger Woods',
                'GG4')";

        public const string Card_4 =
            @"INSERT INTO Cards(
                Id,
                SetId,
                Name,
                SetIndex)
            VALUES(
                4,
                1,
                'Greg Maddux',
                '361')";

        public const string Card_5 =
            @"INSERT INTO Cards(
                Id,
                SetId,
                Name,
                SetIndex)
            VALUES(
                5,
                1,
                'George Brett',
                '700')";

        public const string Card_6 =
            @"INSERT INTO Cards(
                Id,
                SetId,
                Name,
                SetIndex)
            VALUES(
                6,
                1,
                'Mike Schmidt',
                '600')";

        public const string Card_7 =
            @"INSERT INTO Cards(
                Id,
                SetId,
                Name,
                SetIndex)
            VALUES(
                7,
                1,
                'Jose Canseco',
                '370')";

        public const string Card_8 =
            @"INSERT INTO Cards(
                Id,
                SetId,
                Name,
                SetIndex)
            VALUES(
                8,
                5,
                'Joe Montana',
                '73')";

        public const string Card_9 =
            @"INSERT INTO Cards(
                Id,
                SetId,
                Name,
                SetIndex)
            VALUES(
                9,
                5,
                'Troy Aikman',
                '371')";

        public const string Card_10 =
            @"INSERT INTO Cards(
                Id,
                SetId,
                Name,
                SetIndex)
            VALUES(
                10,
                5,
                'Cortez Kennedy',
                '287')";

        public const string Card_11 =
            @"INSERT INTO Cards(
                Id,
                SetId,
                Name,
                SetIndex)
            VALUES(
                11,
                5,
                'Dan Marino',
                '112')";

        public const string Card_12 =
            @"INSERT INTO Cards(
                Id,
                SetId,
                Name,
                SetIndex)
            VALUES(
                12,
                5,
                'Barry Sanders',
                '415')";
    }
}
