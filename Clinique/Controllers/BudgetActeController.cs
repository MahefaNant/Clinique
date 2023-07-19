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
    public class BudgetActeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BudgetActeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BudgetActe
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BudgetActe.Include(b => b.TypeActe);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BudgetActe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetActe = await _context.BudgetActe
                .Include(b => b.TypeActe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (budgetActe == null)
            {
                return NotFound();
            }

            return View(budgetActe);
        }

        // GET: BudgetActe/Create
        public IActionResult Create()
        {
            ViewData["IdTypeActe"] = new SelectList(_context.TypeActe, "Id", "Nom");
            return View();
        }

        // POST: BudgetActe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdTypeActe,Annee,Budget")] BudgetActe budgetActe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(budgetActe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTypeActe"] = new SelectList(_context.TypeActe, "Id", "Nom", budgetActe.IdTypeActe);
            return View(budgetActe);
        }

        // GET: BudgetActe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetActe = await _context.BudgetActe.FindAsync(id);
            if (budgetActe == null)
            {
                return NotFound();
            }
            ViewData["IdTypeActe"] = new SelectList(_context.TypeActe, "Id", "Nom", budgetActe.IdTypeActe);
            return View(budgetActe);
        }

        // POST: BudgetActe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdTypeActe,Annee,Budget")] BudgetActe budgetActe)
        {
            if (id != budgetActe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(budgetActe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetActeExists(budgetActe.Id))
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
            ViewData["IdTypeActe"] = new SelectList(_context.TypeActe, "Id", "Nom", budgetActe.IdTypeActe);
            return View(budgetActe);
        }

        // GET: BudgetActe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetActe = await _context.BudgetActe
                .Include(b => b.TypeActe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (budgetActe == null)
            {
                return NotFound();
            }

            return View(budgetActe);
        }

        // POST: BudgetActe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var budgetActe = await _context.BudgetActe.FindAsync(id);
            if (budgetActe != null)
            {
                _context.BudgetActe.Remove(budgetActe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BudgetActeExists(int id)
        {
            return _context.BudgetActe.Any(e => e.Id == id);
        }
    }
}
