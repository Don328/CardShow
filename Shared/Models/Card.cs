using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Shared.Models
{
    public class Card
    {
        public int Id { get; set; }
        public int SetId { get; set; }
        public string Name { get; set; }
            = string.Empty;
        public string SetIndex { get; set; }
            = string.Empty;
    }
}
