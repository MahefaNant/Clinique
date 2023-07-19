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
    public class FacturePatientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacturePatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FacturePatient
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FacturePatient.Include(f => f.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FacturePatient/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturePatient = await _context.FacturePatient
                .Include(f => f.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);

            var factureDetails = await _context.VFactureActe.Include(a => a.TypeActe)
                .Include(a => a.FacturePatient)
                .Include(a => a.Patient)
                .Where(a => a.IdFacture == id)
                .ToListAsync();

            var sommeFacture = await _context.VSommeFacture.Where(a => a.IdFacture == id).FirstOrDefaultAsync();

            ViewBag.factureDetails = factureDetails;
            ViewBag.sommeFacture = sommeFacture.TotalMontant;
            if (facturePatient == null)
            {
                return NotFound();
            }

            return View(facturePatient);
        }

        // GET: FacturePatient/Create
        public IActionResult Create()
        {
            ViewData["IdPatient"] = new SelectList(_context.Patient, "Id", "Nom");
            return View();
        }

        // POST: FacturePatient/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPatient,Date")] FacturePatient facturePatient)
        {
            if (ModelState.IsValid)
            {
                facturePatient.Date = DateTimeToUTC.Make(facturePatient.Date);
                _context.Add(facturePatient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPatient"] = new SelectList(_context.Patient, "Id", "Nom", facturePatient.IdPatient);
            return View(facturePatient);
        }

        // GET: FacturePatient/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturePatient = await _context.FacturePatient.FindAsync(id);
            if (facturePatient == null)
            {
                return NotFound();
            }
            ViewData["IdPatient"] = new SelectList(_context.Patient, "Id", "Nom", facturePatient.IdPatient);
            return View(facturePatient);
        }

        // POST: FacturePatient/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPatient,Date")] FacturePatient facturePatient)
        {
            if (id != facturePatient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    facturePatient.Date = DateTimeToUTC.Make(facturePatient.Date);
                    _context.Update(facturePatient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturePatientExists(facturePatient.Id))
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
            ViewData["IdPatient"] = new SelectList(_context.Patient, "Id", "Nom", facturePatient.IdPatient);
            return View(facturePatient);
        }

        // GET: FacturePatient/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturePatient = await _context.FacturePatient
                .Include(f => f.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facturePatient == null)
            {
                return NotFound();
            }

            return View(facturePatient);
        }

        // POST: FacturePatient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facturePatient = await _context.FacturePatient.FindAsync(id);
            if (facturePatient != null)
            {
                _context.FacturePatient.Remove(facturePatient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturePatientExists(int id)
        {
            return _context.FacturePatient.Any(e => e.Id == id);
        }
    }
}
