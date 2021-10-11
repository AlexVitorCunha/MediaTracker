using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COMP2084_Project_200465920.Models
{
    
    public class Media
    {
        
        public int MediaId { get; set; }
        [Required()]
        public string Title { get; set; }
        [Required()]
        [Display(Name = "Media Type")]
        public String MediaType { get; set; }
        [Range(0, 100)]
        public int Season { get; set; }
        [Range(0, 1000)]
        public int Episode { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        [Required()]
        [Range(0, 9999)]
        public int GenreId { get; set; }
        public string Poster { get; set; }
        public Genre Genre { get; set; }

        // child ref's
        public List<Review> Reviews { get; set; }
        public List<WatchList> WatchLists { get; set; }

    }
}
