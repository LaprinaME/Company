using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Company.DataContext;
using Company.Models;

namespace Company.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly CompanyContext _context;

        public ApplicationsController(CompanyContext context)
        {
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            return View(await _context.Приложения.ToListAsync());
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var приложения = await _context.Приложения
                .FirstOrDefaultAsync(m => m.Код_приложения == id);
            if (приложения == null)
            {
                return NotFound();
            }

            return View(приложения);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Код_приложения,Название_приложения,Тип_приложения")] Приложения приложения)
        {
            if (ModelState.IsValid)
            {
                _context.Add(приложения);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(приложения);
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var приложения = await _context.Приложения.FindAsync(id);
            if (приложения == null)
            {
                return NotFound();
            }
            return View(приложения);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Код_приложения,Название_приложения,Тип_приложения")] Приложения приложения)
        {
            if (id != приложения.Код_приложения)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(приложения);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ПриложенияExists(приложения.Код_приложения))
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
            return View(приложения);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var приложения = await _context.Приложения
                .FirstOrDefaultAsync(m => m.Код_приложения == id);
            if (приложения == null)
            {
                return NotFound();
            }

            return View(приложения);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var приложения = await _context.Приложения.FindAsync(id);
            if (приложения != null)
            {
                _context.Приложения.Remove(приложения);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ПриложенияExists(int id)
        {
            return _context.Приложения.Any(e => e.Код_приложения == id);
        }
    }
}
