using CardShow.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardShow.Shared.Models
{
    public class CardSet
    {
        private const int minYear = 1930;
        private const int maxYear = 2023;
        private const int minName = 4;
        private const int maxName = 25;

        private const string minYrStr = "1930";
        private const string maxYrStr = "2023";
        private const string minNameStr = "4";
        private const string maxNameStr = "25";

        public int Id { get; set; }

        [Required]
        [Range(minYear, maxYear, ErrorMessage = 
            $"Year must be between " +
            $"{minYrStr} and {maxYrStr}")]
        public int Year { get; set; }

        [Required]
        [MinLength(minName, ErrorMessage = 
            $"Name must be at least " +
            $"{minNameStr} characters")]
        [MaxLength(maxName, ErrorMessage = 
            $"Name must be {maxNameStr} " +
            $"characters or less")]
        public string Name { get; set; }
            = string.Empty;

        [Required]
        [Range(1, 10, ErrorMessage = 
            "You must select a Sport")]
        public Sport Sport { get; set; }
    }
}
