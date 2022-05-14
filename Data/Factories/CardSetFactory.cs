using CardShow.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.Factories
{
    public class CardSetFactory
    {
        public _CardSet CreateSet(
            int year, string name, int sport)
        {
            return new _CardSet()
            {
                Year = year,
                Name = name,
                Sport = sport
            };
        }

    }
}
