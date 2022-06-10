using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Data.Models
{
    public class _Assessment
    {
        public _Assessment(
            int cardId,
            DateTime date,
            int highGrade,
            int lowGrade,
            string text,
            string corners,
            string edges,
            string centering,
            string surface,
            int id = 0)
        {
            Id = id;
            CardId = cardId;
            Date = date;
            HighGrade = highGrade;
            LowGrade = lowGrade;
            Text = text;
            Corners = corners;
            Edges = edges;
            Centering = centering;
            Surface = surface;
        }

        public int Id { get; }
        public int CardId { get; }
        public DateTime Date { get; }
        public int HighGrade { get; }
        public int LowGrade { get; }
        public string Text { get; }
            = string.Empty;
        public string Corners { get; }
            = string.Empty;
        public string Edges { get; }
            = string.Empty;
        public string Centering { get; }
            = string.Empty;
        public string Surface { get; }
            = string.Empty;
    }
}
