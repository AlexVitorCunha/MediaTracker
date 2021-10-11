using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COMP2084_Project_200465920.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        [Required()]
        [Range(0, 10)]
        public int Score { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        [Required()]
        [Range(0, 9999)]
        public int MediaId { get; set; }
        public Media Media { get; set; }

    }
}
