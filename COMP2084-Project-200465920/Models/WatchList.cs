using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COMP2084_Project_200465920.Models
{
    public class WatchList
    {
        public int WatchListId { get; set; }
        public int MediaId { get; set; }
        public string UserId { get; set; }
        public bool Watched { get; set; }
        public Media Media { get; set; }
    }
}
