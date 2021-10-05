﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using COMP2084_Project_200465920.Data;
using COMP2084_Project_200465920.Models;

namespace COMP2084_Project_200465920.Controllers
{
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
    }
}
