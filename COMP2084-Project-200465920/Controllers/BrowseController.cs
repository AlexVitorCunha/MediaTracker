using COMP2084_Project_200465920.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP2084_Project_200465920.Controllers
{
    public class BrowseController : Controller
    {
        //add DbContext to use the database
        private readonly ApplicationDbContext _context;

        public BrowseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Browse
        public IActionResult Index()
        {

            // use genres DbSet to fetch list of categories to display to shoppers
            var genres = _context.Genres.OrderBy(c => c.Name).ToList();
            return View(genres);
        }

        // GET: /Browse/BrowseByGenre/1
        public IActionResult BrowseByGenre(int id)
        {
            // get media in selected genre
            var medias = _context.Medias.Where(m => m.GenreId == id)
                .OrderBy(m => m.Title).ToList();
            var genre = _context.Genres.Find(id);
            ViewBag.Genre = genre.Name;
            return View(medias);
        } 
    }
}
