using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COMP2084_Project_200465920.Models
{
    public class Genre
    {
        [Display(Name = "Genre Id")]
        public int GenreId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "And no empty strings either!")]
        [MaxLength(100)]
        public string Name { get; set; }

        // child ref
        public List<Media> Medias { get; set; }
    }
}
