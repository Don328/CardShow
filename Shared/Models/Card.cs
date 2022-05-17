using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Shared.Models
{
    public class Card
    {
        private const int minName = 2;
        private const int maxName = 25;
        private const int indexLength = 5;

        private const string minNameStr = "2";
        private const string maxNameStr = "25";
        private const string indexLengthStr = "5";

        public int Id { get; set; }

        [Required]
        public int SetId { get; set; }

        [Required]
        [MinLength(minName, ErrorMessage =
            $"Player name must be at least 2 characters")]
        [MaxLength(maxName, ErrorMessage =
            $"Player name must be {maxNameStr} characters or less")]
        public string Name { get; set; }
            = string.Empty;

        [StringLength(indexLength, ErrorMessage =
            $"SetIndex must be 5 characters or less")]
        public string SetIndex { get; set; }
            = string.Empty;

        public IEnumerable<Assessment> Assessments { get; set; }
            = new List<Assessment>();
    }
}
