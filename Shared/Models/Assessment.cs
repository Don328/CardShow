using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Shared.Models
{
    public class Assessment
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public DateTime Date { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "You must enter a (high) estimate (0-10)")]
        public int HighGrade { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "You must enter a (low) estimate (0-10)")]
        public int LowGrade { get; set; }

        [Required]
        [MaxLength(250), MinLength(5, ErrorMessage = "Summarize the assessment")]
        public string Text { get; set; }
            = string.Empty;

        [Required(ErrorMessage = "Check the corners")]
        [MaxLength(100)]
        public string Corners { get; set; }
            = string.Empty;

        [Required(ErrorMessage = "Check the edges")]
        [MaxLength(100)]
        public string Edges { get; set; }
            = string.Empty;

        [Required(ErrorMessage = "Check for centering")]
        [MaxLength(100)]
        public string Centering { get; set; }
            = string.Empty;

        [Required(ErrorMessage = "Inspect the surface")]
        [MaxLength(100)]
        public string Surface { get; set; }
            = string.Empty;
    }
}
