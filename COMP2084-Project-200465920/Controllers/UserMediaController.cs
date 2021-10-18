using COMP2084_Project_200465920.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP2084_Project_200465920.Controllers
{
    public class UserMediaController : Controller
    {
        //add DbContext to use the database
        private readonly ApplicationDbContext _context;

        public UserMediaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            // use genres DbSet to fetch list of categories to display to shoppers
            var genres = _context.Genres.OrderBy(c => c.Name).ToList();
            return View(genres);
        }
    }
}
