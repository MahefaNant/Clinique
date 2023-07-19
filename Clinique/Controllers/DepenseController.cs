using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTemplate.C_;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCoreTemplate.Data;
using AspNetCoreTemplate.Models;

namespace AspNetCoreTemplate.Controllers
{
    public class DepenseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Depense
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Depense.Include(d => d.TypeDepense);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Depense/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depense = await _context.Depense
                .Include(d => d.TypeDepense)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (depense == null)
            {
                return NotFound();
            }

            return View(depense);
        }

        // GET: Depense/Create
        public IActionResult Create()
        {
            ViewData["IdTypeDepense"] = new SelectList(_context.TypeDepense, "Id", "Nom");
            return View();
        }

        // POST: Depense/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdTypeDepense,Montant,Date")] Depense depense)
        {
            if (ModelState.IsValid)
            {
                depense.Date = DateTimeToUTC.Make(depense.Date);
                _context.Add(depense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTypeDepense"] = new SelectList(_context.TypeDepense, "Id", "Nom", depense.IdTypeDepense);
            return View(depense);
        }
        
        [HttpPost]
        public IActionResult CreateFacile( int id_typedepense , int jour , int annee, List<int> mois, double montant)
        {
            foreach (var q in mois)
            {
                Depense depense = new Depense();
                depense.IdTypeDepense = id_typedepense;
                depense.Montant = montant;
                depense.Date = DateTimeToUTC.Make(new DateTime(annee, q, jour));
                _context.Add(depense);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index)); 
        }

        // GET: Depense/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depense = await _context.Depense.FindAsync(id);
            if (depense == null)
            {
                return NotFound();
            }
            ViewData["IdTypeDepense"] = new SelectList(_context.TypeDepense, "Id", "Nom", depense.IdTypeDepense);
            return View(depense);
        }

        // POST: Depense/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdTypeDepense,Montant,Date")] Depense depense)
        {
            if (id != depense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    depense.Date = DateTimeToUTC.Make(depense.Date);
                    _context.Update(depense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepenseExists(depense.Id))
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
            ViewData["IdTypeDepense"] = new SelectList(_context.TypeDepense, "Id", "Nom", depense.IdTypeDepense);
            return View(depense);
        }

        // GET: Depense/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depense = await _context.Depense
                .Include(d => d.TypeDepense)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (depense == null)
            {
                return NotFound();
            }

            return View(depense);
        }

        // POST: Depense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var depense = await _context.Depense.FindAsync(id);
            if (depense != null)
            {
                _context.Depense.Remove(depense);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepenseExists(int id)
        {
            return _context.Depense.Any(e => e.Id == id);
        }
    }
}
