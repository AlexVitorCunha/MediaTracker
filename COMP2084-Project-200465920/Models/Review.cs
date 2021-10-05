using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP2084_Project_200465920.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public int MediaId { get; set; }
        public Media Media { get; set; }

    }
}
