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
    public class TypeDepenseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeDepenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeDepense
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeDepense.ToListAsync());
        }

        // GET: TypeDepense/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeDepense = await _context.TypeDepense
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeDepense == null)
            {
                return NotFound();
            }

            return View(typeDepense);
        }

        // GET: TypeDepense/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeDepense/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Code")] TypeDepense typeDepense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeDepense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeDepense);
        }

        // GET: TypeDepense/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeDepense = await _context.TypeDepense.FindAsync(id);
            if (typeDepense == null)
            {
                return NotFound();
            }
            return View(typeDepense);
        }

        // POST: TypeDepense/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nomm,Code")] TypeDepense typeDepense)
        {
            if (id != typeDepense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeDepense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeDepenseExists(typeDepense.Id))
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
            return View(typeDepense);
        }

        // GET: TypeDepense/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeDepense = await _context.TypeDepense
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeDepense == null)
            {
                return NotFound();
            }

            return View(typeDepense);
        }

        // POST: TypeDepense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeDepense = await _context.TypeDepense.FindAsync(id);
            if (typeDepense != null)
            {
                _context.TypeDepense.Remove(typeDepense);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeDepenseExists(int id)
        {
            return _context.TypeDepense.Any(e => e.Id == id);
        }
    }
}
