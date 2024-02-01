using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HalaSystem.Data;
using HalaSystem.Models;

namespace HalaSystem.Controllers
{
    public class HaleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HaleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hale
        public async Task<IActionResult> Index()
        {
              return _context.Hale != null ? 
                          View(await _context.Hale.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Hale'  is null.");
        }

        // GET: Hale/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hale == null)
            {
                return NotFound();
            }

            var hala = await _context.Hale
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hala == null)
            {
                return NotFound();
            }

            return View(hala);
        }

        // GET: Hale/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hale/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Adres,IloscMiejsc")] Hala hala)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hala);
        }

        // GET: Hale/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hale == null)
            {
                return NotFound();
            }

            var hala = await _context.Hale.FindAsync(id);
            if (hala == null)
            {
                return NotFound();
            }
            return View(hala);
        }

        // POST: Hale/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Adres,IloscMiejsc")] Hala hala)
        {
            if (id != hala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HalaExists(hala.Id))
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
            return View(hala);
        }

        // GET: Hale/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hale == null)
            {
                return NotFound();
            }

            var hala = await _context.Hale
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hala == null)
            {
                return NotFound();
            }

            return View(hala);
        }

        // POST: Hale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hale == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Hale'  is null.");
            }
            var hala = await _context.Hale.FindAsync(id);
            if (hala != null)
            {
                _context.Hale.Remove(hala);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HalaExists(int id)
        {
          return (_context.Hale?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
