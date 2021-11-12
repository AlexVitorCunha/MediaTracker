using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using COMP2084_Project_200465920.Data;
using COMP2084_Project_200465920.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace COMP2084_Project_200465920.Controllers
{
    [Authorize]
    public class WatchListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WatchListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WatchLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.WatchLists.ToListAsync());
        }

        // GET: WatchLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchList = await _context.WatchLists
                .FirstOrDefaultAsync(m => m.WatchListId == id);
            if (watchList == null)
            {
                return NotFound();
            }

            return View(watchList);
        }

        // GET: WatchLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WatchLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WatchListId,UserId,Watched")] WatchList watchList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(watchList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(watchList);
        }

        // GET: WatchLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchList = await _context.WatchLists.FindAsync(id);
            if (watchList == null)
            {
                return NotFound();
            }
            return View(watchList);
        }

        // POST: WatchLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WatchListId,UserId,Watched")] WatchList watchList)
        {
            if (id != watchList.WatchListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(watchList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WatchListExists(watchList.WatchListId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(watchList);
        }

        // GET: WatchLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchList = await _context.WatchLists
                .FirstOrDefaultAsync(m => m.WatchListId == id);
            if (watchList == null)
            {
                return NotFound();
            }

            return View(watchList);
        }

        // POST: WatchLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var watchList = await _context.WatchLists.FindAsync(id);
            _context.WatchLists.Remove(watchList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WatchListExists(int id)
        {
            return _context.WatchLists.Any(e => e.WatchListId == id);
        }

        // POST: WatchLists/AddMedia

        [HttpPost]
        public IActionResult AddMedia(int MediaId, bool Watched)
        {
            //set UserId to WatchList item
            var userId = GetUserId();
            if(userId == "")
            {
                return View("/Identity/Account/Login");
            }
            else
            {
                var media = _context.WatchLists
                    .SingleOrDefault(m => m.MediaId == MediaId && m.UserId == userId);
                if(media == null)
                {
                    media = new WatchList
                    {
                        MediaId = MediaId,
                        UserId = userId,
                        Watched = Watched
                    };
                    //save to WatchLists table in db
                    _context.WatchLists.Add(media);
                }
                _context.SaveChanges();
                //Load personal WatchList
                return RedirectToAction("MyWatchList");
            }
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

        // GET: /WatchLists/MyWatchList/
        public IActionResult MyWatchList()
        {
            // identity the user from the session var
            var userId = GetUserId();
            //load media for this user from the db for display
            var medias = _context.WatchLists
                .Include(w => w.Media)
                .Where(m => m.UserId == userId).ToList();
            return View(medias);
        }

        //GET: /WatchLists/RemoveFromWatchList/
        public IActionResult RemoveFromWatchList(int id)
        {
            var media = _context.WatchLists.Find(id);
            _context.WatchLists.Remove(media);
            _context.SaveChanges();
            return RedirectToAction("MyWatchList");
        }

    }
}
