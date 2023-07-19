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
    public class TypeActeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeActeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypeActe
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeActe.ToListAsync());
        }

        // GET: TypeActe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeActe = await _context.TypeActe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeActe == null)
            {
                return NotFound();
            }

            return View(typeActe);
        }

        // GET: TypeActe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeActe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Code")] TypeActe typeActe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeActe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeActe);
        }

        // GET: TypeActe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeActe = await _context.TypeActe.FindAsync(id);
            if (typeActe == null)
            {
                return NotFound();
            }
            return View(typeActe);
        }

        // POST: TypeActe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Code")] TypeActe typeActe)
        {
            if (id != typeActe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeActe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeActeExists(typeActe.Id))
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
            return View(typeActe);
        }

        // GET: TypeActe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeActe = await _context.TypeActe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeActe == null)
            {
                return NotFound();
            }

            return View(typeActe);
        }

        // POST: TypeActe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeActe = await _context.TypeActe.FindAsync(id);
            if (typeActe != null)
            {
                _context.TypeActe.Remove(typeActe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeActeExists(int id)
        {
            return _context.TypeActe.Any(e => e.Id == id);
        }
    }
}
