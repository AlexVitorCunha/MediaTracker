using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using COMP2084_Project_200465920.Data;
using COMP2084_Project_200465920.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace COMP2084_Project_200465920.Controllers
{
    public class MediasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MediasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Medias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Medias.Include(m => m.Genre);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Medias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Medias
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.MediaId == id);
            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        // GET: Medias/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Name");
            return View();
        }

        // POST: Medias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MediaId,Title,MediaType,Season,Episode,ReleaseDate,GenreId")] Media media, IFormFile Poster)
        {
            if (ModelState.IsValid)
            {
                if(Poster != null)
                {
                    var fileName = UploadPoster(Poster);
                    media.Poster = fileName;
                }
                _context.Add(media);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Name", media.GenreId);
            return View(media);
        }

        private string UploadPoster (IFormFile Photo)
        {
            //get temp location of uploaded photo
            var filePath = Path.GetTempFileName();

            var fileName = Guid.NewGuid() + "-" + Photo.FileName;

            //set destination path dynamically so it works on any system
            var uploadPath = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\photos\\posters\\" + fileName;

            // actually execute the file copy now
            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                Photo.CopyTo(stream);
            }

            return fileName;


        }

        // GET: Medias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Medias.FindAsync(id);
            if (media == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Name", media.GenreId);
            return View(media);
        }

        // POST: Medias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MediaId,Title,MediaType,Season,Episode,ReleaseDate,GenreId")] Media media, IFormFile Poster)
        {
            if (id != media.MediaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(Poster != null)
                    {
                        var fileName = UploadPoster(Poster);
                        media.Poster = fileName;
                    }
                    _context.Update(media);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaExists(media.MediaId))
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
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "Name", media.GenreId);
            return View(media);
        }

        // GET: Medias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Medias
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.MediaId == id);
            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        // POST: Medias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var media = await _context.Medias.FindAsync(id);
            _context.Medias.Remove(media);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediaExists(int id)
        {
            return _context.Medias.Any(e => e.MediaId == id);
        }
    }
}
