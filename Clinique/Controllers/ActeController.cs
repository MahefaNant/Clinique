using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCoreTemplate.Data;
using AspNetCoreTemplate.Models;

namespace AspNetCoreTemplate.Controllers
{
    public class ActeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Acte
        public async Task<IActionResult> Index(int? idFacture)
        {
            IQueryable<Acte> applicationDbContext = null;
            if(idFacture==null)
                applicationDbContext = _context.Acte.Include(a => a.FacturePatient).Include(a => a.TypeActe);
            else
            {
                applicationDbContext = _context.Acte.Include(a => a.FacturePatient).Include(a => a.TypeActe)
                    .Where(q => q.IdFacture == idFacture);
                @ViewBag.idFacture = idFacture;
            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Acte/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acte = await _context.Acte
                .Include(a => a.FacturePatient)
                .Include(a => a.TypeActe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acte == null)
            {
                return NotFound();
            }

            return View(acte);
        }

        // GET: Acte/Create
        public IActionResult Create(int? idFacture)
        {
            ViewData["IdFacture"] = new SelectList(_context.FacturePatient.Where(q=> q.Id==idFacture), "Id", "Id");
            ViewData["IdTypeActe"] = new SelectList(_context.TypeActe, "Id", "Nom");
            return View();
        }

        // POST: Acte/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdTypeActe,IdFacture,Montant")] Acte acte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(acte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { idFacture = acte.IdFacture });
            }
            ViewData["IdFacture"] = new SelectList(_context.FacturePatient, "Id", "Id", acte.IdFacture);
            ViewData["IdTypeActe"] = new SelectList(_context.TypeActe, "Id", "Nom", acte.IdTypeActe);
            return View(acte);
        }

        // GET: Acte/Edit/5
        public async Task<IActionResult> Edit(int? id, int? idFacture)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acte = await _context.Acte.FindAsync(id);
            if (acte == null)
            {
                return NotFound();
            }
            ViewData["IdFacture"] = new SelectList(_context.FacturePatient.Where(q=> q.Id==idFacture), "Id", "Id", acte.IdFacture);
            ViewData["IdTypeActe"] = new SelectList(_context.TypeActe, "Id", "Nom", acte.IdTypeActe);
            return View(acte);
        }

        // POST: Acte/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdTypeActe,IdFacture,Montant")] Acte acte)
        {
            if (id != acte.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(acte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActeExists(acte.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { idFacture = acte.IdFacture });
            }
            ViewData["IdFacture"] = new SelectList(_context.FacturePatient, "Id", "Id", acte.IdFacture);
            ViewData["IdTypeActe"] = new SelectList(_context.TypeActe, "Id", "Nom", acte.IdTypeActe);
            return View(acte);
        }

        // GET: Acte/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acte = await _context.Acte
                .Include(a => a.FacturePatient)
                .Include(a => a.TypeActe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acte == null)
            {
                return NotFound();
            }

            return View(acte);
        }

        // POST: Acte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var acte = await _context.Acte.FindAsync(id);
            if (acte != null)
            {
                _context.Acte.Remove(acte);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { idFacture = acte.IdFacture });
        }

        private bool ActeExists(int id)
        {
            return _context.Acte.Any(e => e.Id == id);
        }
    }
}
