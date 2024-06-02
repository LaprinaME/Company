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
    public class EditStatusesController : Controller
    {
        private readonly CompanyContext _context;

        public EditStatusesController(CompanyContext context)
        {
            _context = context;
        }

        // GET: EditStatuses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Статусы.ToListAsync());
        }

        // GET: EditStatuses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var статусы = await _context.Статусы
                .FirstOrDefaultAsync(m => m.Код_статуса == id);
            if (статусы == null)
            {
                return NotFound();
            }

            return View(статусы);
        }

        // GET: EditStatuses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EditStatuses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Код_статуса,Название_статуса")] Статусы статусы)
        {
            if (ModelState.IsValid)
            {
                _context.Add(статусы);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(статусы);
        }

        // GET: EditStatuses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var статусы = await _context.Статусы.FindAsync(id);
            if (статусы == null)
            {
                return NotFound();
            }
            return View(статусы);
        }

        // POST: EditStatuses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Код_статуса,Название_статуса")] Статусы статусы)
        {
            if (id != статусы.Код_статуса)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(статусы);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!СтатусыExists(статусы.Код_статуса))
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
            return View(статусы);
        }

        // GET: EditStatuses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var статусы = await _context.Статусы
                .FirstOrDefaultAsync(m => m.Код_статуса == id);
            if (статусы == null)
            {
                return NotFound();
            }

            return View(статусы);
        }

        // POST: EditStatuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var статусы = await _context.Статусы.FindAsync(id);
            if (статусы != null)
            {
                _context.Статусы.Remove(статусы);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool СтатусыExists(int id)
        {
            return _context.Статусы.Any(e => e.Код_статуса == id);
        }
    }
}
