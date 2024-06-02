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
    public class EditOrganizationController : Controller
    {
        private readonly CompanyContext _context;

        public EditOrganizationController(CompanyContext context)
        {
            _context = context;
        }

        // GET: EditOrganization
        public async Task<IActionResult> Index()
        {
            var companyContext = _context.Организации.Include(о => о.Родительская_организация).Include(о => о.Статусы);
            return View(await companyContext.ToListAsync());
        }

        // GET: EditOrganization/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var организации = await _context.Организации
                .Include(о => о.Родительская_организация)
                .Include(о => о.Статусы)
                .FirstOrDefaultAsync(m => m.Код_организации == id);
            if (организации == null)
            {
                return NotFound();
            }

            return View(организации);
        }

        // GET: EditOrganization/Create
        public IActionResult Create()
        {
            ViewData["Код_родительской_организации"] = new SelectList(_context.Организации, "Код_организации", "Код_организации");
            ViewData["Код_статуса"] = new SelectList(_context.Статусы, "Код_статуса", "Код_статуса");
            return View();
        }

        // POST: EditOrganization/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Код_организации,Код_статуса,Название_организации,ИНН,КПП,ОГРН,Код,Тип_организации,Дата_закрытия,Код_родительской_организации")] Организации организации)
        {
            if (ModelState.IsValid)
            {
                _context.Add(организации);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Код_родительской_организации"] = new SelectList(_context.Организации, "Код_организации", "Код_организации", организации.Код_родительской_организации);
            ViewData["Код_статуса"] = new SelectList(_context.Статусы, "Код_статуса", "Код_статуса", организации.Код_статуса);
            return View(организации);
        }

        // GET: EditOrganization/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var организации = await _context.Организации.FindAsync(id);
            if (организации == null)
            {
                return NotFound();
            }
            ViewData["Код_родительской_организации"] = new SelectList(_context.Организации, "Код_организации", "Код_организации", организации.Код_родительской_организации);
            ViewData["Код_статуса"] = new SelectList(_context.Статусы, "Код_статуса", "Код_статуса", организации.Код_статуса);
            return View(организации);
        }

        // POST: EditOrganization/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Код_организации,Код_статуса,Название_организации,ИНН,КПП,ОГРН,Код,Тип_организации,Дата_закрытия,Код_родительской_организации")] Организации организации)
        {
            if (id != организации.Код_организации)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(организации);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ОрганизацииExists(организации.Код_организации))
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
            ViewData["Код_родительской_организации"] = new SelectList(_context.Организации, "Код_организации", "Код_организации", организации.Код_родительской_организации);
            ViewData["Код_статуса"] = new SelectList(_context.Статусы, "Код_статуса", "Код_статуса", организации.Код_статуса);
            return View(организации);
        }

        // GET: EditOrganization/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var организации = await _context.Организации
                .Include(о => о.Родительская_организация)
                .Include(о => о.Статусы)
                .FirstOrDefaultAsync(m => m.Код_организации == id);
            if (организации == null)
            {
                return NotFound();
            }

            return View(организации);
        }

        // POST: EditOrganization/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var организации = await _context.Организации.FindAsync(id);
            if (организации != null)
            {
                _context.Организации.Remove(организации);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ОрганизацииExists(int id)
        {
            return _context.Организации.Any(e => e.Код_организации == id);
        }

    }
}
