using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.Models
{
    public class _Assessment
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public DateTime Date { get; set; }
        public int HighGrade { get; set; }
        public int LowGrade { get; set; }
        public string Text { get; set; }
            = string.Empty;
        public string Corners { get; set; }
            = string.Empty;
        public string Edges { get; set; }
            = string.Empty;
        public string Centering { get; set; }
            = string.Empty;
        public string Surface { get; set; }
            = string.Empty;
    }
}
