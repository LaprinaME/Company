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
    public class PeriodsController : Controller
    {
        private readonly CompanyContext _context;

        public PeriodsController(CompanyContext context)
        {
            _context = context;
        }

        // GET: Periods
        public async Task<IActionResult> Index()
        {
            var companyContext = _context.Периоды_неисполнения_обязанностей.Include(п => п.Сотрудники);
            return View(await companyContext.ToListAsync());
        }

        // GET: Periods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var периоды_неисполнения_обязанностей = await _context.Периоды_неисполнения_обязанностей
                .Include(п => п.Сотрудники)
                .FirstOrDefaultAsync(m => m.Код_периода == id);
            if (периоды_неисполнения_обязанностей == null)
            {
                return NotFound();
            }

            return View(периоды_неисполнения_обязанностей);
        }

        // GET: Periods/Create
        public IActionResult Create()
        {
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника");
            return View();
        }

        // POST: Periods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Код_периода,Код_сотрудника,Дата_начала,Дата_окончания,Дата_увольнения,Дата_приема,Тип,Причина_отсутствия_на_работе,Причина_увольнения")] Периоды_неисполнения_обязанностей периоды_неисполнения_обязанностей)
        {
            if (ModelState.IsValid)
            {
                _context.Add(периоды_неисполнения_обязанностей);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника", периоды_неисполнения_обязанностей.Код_сотрудника);
            return View(периоды_неисполнения_обязанностей);
        }

        // GET: Periods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var периоды_неисполнения_обязанностей = await _context.Периоды_неисполнения_обязанностей.FindAsync(id);
            if (периоды_неисполнения_обязанностей == null)
            {
                return NotFound();
            }
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника", периоды_неисполнения_обязанностей.Код_сотрудника);
            return View(периоды_неисполнения_обязанностей);
        }

        // POST: Periods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Код_периода,Код_сотрудника,Дата_начала,Дата_окончания,Дата_увольнения,Дата_приема,Тип,Причина_отсутствия_на_работе,Причина_увольнения")] Периоды_неисполнения_обязанностей периоды_неисполнения_обязанностей)
        {
            if (id != периоды_неисполнения_обязанностей.Код_периода)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(периоды_неисполнения_обязанностей);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Периоды_неисполнения_обязанностейExists(периоды_неисполнения_обязанностей.Код_периода))
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
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника", периоды_неисполнения_обязанностей.Код_сотрудника);
            return View(периоды_неисполнения_обязанностей);
        }

        // GET: Periods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var периоды_неисполнения_обязанностей = await _context.Периоды_неисполнения_обязанностей
                .Include(п => п.Сотрудники)
                .FirstOrDefaultAsync(m => m.Код_периода == id);
            if (периоды_неисполнения_обязанностей == null)
            {
                return NotFound();
            }

            return View(периоды_неисполнения_обязанностей);
        }

        // POST: Periods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var периоды_неисполнения_обязанностей = await _context.Периоды_неисполнения_обязанностей.FindAsync(id);
            if (периоды_неисполнения_обязанностей != null)
            {
                _context.Периоды_неисполнения_обязанностей.Remove(периоды_неисполнения_обязанностей);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Периоды_неисполнения_обязанностейExists(int id)
        {
            return _context.Периоды_неисполнения_обязанностей.Any(e => e.Код_периода == id);
        }
    }
}
