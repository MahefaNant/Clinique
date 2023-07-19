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
    public class BudgetDepenseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BudgetDepenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BudgetDepense
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BudgetDepense.Include(b => b.TypeDepense);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BudgetDepense/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetDepense = await _context.BudgetDepense
                .Include(b => b.TypeDepense)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (budgetDepense == null)
            {
                return NotFound();
            }

            return View(budgetDepense);
        }

        // GET: BudgetDepense/Create
        public IActionResult Create()
        {
            ViewData["IdTypeDepense"] = new SelectList(_context.TypeDepense, "Id", "Id");
            return View();
        }

        // POST: BudgetDepense/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdTypeDepense,Annee,Budget")] BudgetDepense budgetDepense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(budgetDepense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTypeDepense"] = new SelectList(_context.TypeDepense, "Id", "Id", budgetDepense.IdTypeDepense);
            return View(budgetDepense);
        }

        // GET: BudgetDepense/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetDepense = await _context.BudgetDepense.FindAsync(id);
            if (budgetDepense == null)
            {
                return NotFound();
            }
            ViewData["IdTypeDepense"] = new SelectList(_context.TypeDepense, "Id", "Id", budgetDepense.IdTypeDepense);
            return View(budgetDepense);
        }

        // POST: BudgetDepense/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdTypeDepense,Annee,Budget")] BudgetDepense budgetDepense)
        {
            if (id != budgetDepense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(budgetDepense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetDepenseExists(budgetDepense.Id))
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
            ViewData["IdTypeDepense"] = new SelectList(_context.TypeDepense, "Id", "Id", budgetDepense.IdTypeDepense);
            return View(budgetDepense);
        }

        // GET: BudgetDepense/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetDepense = await _context.BudgetDepense
                .Include(b => b.TypeDepense)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (budgetDepense == null)
            {
                return NotFound();
            }

            return View(budgetDepense);
        }

        // POST: BudgetDepense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var budgetDepense = await _context.BudgetDepense.FindAsync(id);
            if (budgetDepense != null)
            {
                _context.BudgetDepense.Remove(budgetDepense);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BudgetDepenseExists(int id)
        {
            return _context.BudgetDepense.Any(e => e.Id == id);
        }
    }
}
