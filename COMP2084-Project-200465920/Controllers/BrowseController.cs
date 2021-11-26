using COMP2084_Project_200465920.Data;
using COMP2084_Project_200465920.Models;
using Microsoft.AspNetCore.Http;
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
            var userId = GetUserId();
            var watchList = _context.WatchLists.Where(u => u.UserId == userId);
            var medias = _context.Medias.Where(m => m.GenreId == id)
                .OrderBy(m => m.Title).ToList();
            var genre = _context.Genres.Find(id);
            var tuple = new Tuple<IEnumerable<Media>, IEnumerable<WatchList>>(medias, watchList);
            ViewBag.Genre = genre.Name;
            return View(medias);
        }

        private string GetUserId()
        {
            // check session for an existing UserId for the user's cart
            if (HttpContext.Session.GetString("UserId") == null)
            {
                // this is user's 1st cart item
                var userId = "";
                if (User.Identity.IsAuthenticated)
                {
                    //user has logged in; use email
                    userId = User.Identity.Name;
                }

                // store userId in a session var
                HttpContext.Session.SetString("UserId", userId);
            }

            return HttpContext.Session.GetString("UserId");
        }
    }
}
