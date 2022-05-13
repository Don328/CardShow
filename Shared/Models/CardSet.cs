using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Shared.Models
{
    public class CardSet
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
            = string.Empty;
    }
}
