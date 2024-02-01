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
    public class RezerwacjeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RezerwacjeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rezerwacje
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rezerwacje.Include(r => r.Hala);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rezerwacje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rezerwacje == null)
            {
                return NotFound();
            }

            var rezerwacja = await _context.Rezerwacje
                .Include(r => r.Hala)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezerwacja == null)
            {
                return NotFound();
            }

            return View(rezerwacja);
        }

        // GET: Rezerwacje/Create
        public IActionResult Create()
        {
            ViewData["HalaId"] = new SelectList(_context.Hale, "Id", "Nazwa");
            return View();
        }

        // POST: Rezerwacje/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HalaId,UserId,Od,Do")] Rezerwacja rezerwacja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezerwacja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HalaId"] = new SelectList(_context.Hale, "Id", "Nazwa", rezerwacja.HalaId);
            return View(rezerwacja);
        }

        // GET: Rezerwacje/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rezerwacje == null)
            {
                return NotFound();
            }

            var rezerwacja = await _context.Rezerwacje.FindAsync(id);
            if (rezerwacja == null)
            {
                return NotFound();
            }
            ViewData["HalaId"] = new SelectList(_context.Hale, "Id", "Nazwa", rezerwacja.HalaId);
            return View(rezerwacja);
        }

        // POST: Rezerwacje/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HalaId,UserId,Od,Do")] Rezerwacja rezerwacja)
        {
            if (id != rezerwacja.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezerwacja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezerwacjaExists(rezerwacja.Id))
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
            ViewData["HalaId"] = new SelectList(_context.Hale, "Id", "Nazwa", rezerwacja.HalaId);
            return View(rezerwacja);
        }

        // GET: Rezerwacje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rezerwacje == null)
            {
                return NotFound();
            }

            var rezerwacja = await _context.Rezerwacje
                .Include(r => r.Hala)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezerwacja == null)
            {
                return NotFound();
            }

            return View(rezerwacja);
        }

        // POST: Rezerwacje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rezerwacje == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rezerwacje'  is null.");
            }
            var rezerwacja = await _context.Rezerwacje.FindAsync(id);
            if (rezerwacja != null)
            {
                _context.Rezerwacje.Remove(rezerwacja);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezerwacjaExists(int id)
        {
          return (_context.Rezerwacje?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
