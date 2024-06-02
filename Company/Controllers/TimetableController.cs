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
    public class TimetableController : Controller
    {
        private readonly CompanyContext _context;

        public TimetableController(CompanyContext context)
        {
            _context = context;
        }

        // GET: Timetable
        public async Task<IActionResult> Index()
        {
            var companyContext = _context.Расписание.Include(р => р.Сотрудники);
            return View(await companyContext.ToListAsync());
        }

        // GET: Timetable/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var расписание = await _context.Расписание
                .Include(р => р.Сотрудники)
                .FirstOrDefaultAsync(m => m.Код_расписания == id);
            if (расписание == null)
            {
                return NotFound();
            }

            return View(расписание);
        }

        // GET: Timetable/Create
        public IActionResult Create()
        {
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника");
            return View();
        }

        // POST: Timetable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Код_расписания,Код_сотрудника,День_недели,Время_начала,Время_окончания")] Расписание расписание)
        {
            if (ModelState.IsValid)
            {
                _context.Add(расписание);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника", расписание.Код_сотрудника);
            return View(расписание);
        }

        // GET: Timetable/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var расписание = await _context.Расписание.FindAsync(id);
            if (расписание == null)
            {
                return NotFound();
            }
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника", расписание.Код_сотрудника);
            return View(расписание);
        }

        // POST: Timetable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Код_расписания,Код_сотрудника,День_недели,Время_начала,Время_окончания")] Расписание расписание)
        {
            if (id != расписание.Код_расписания)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(расписание);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!РасписаниеExists(расписание.Код_расписания))
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
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника", расписание.Код_сотрудника);
            return View(расписание);
        }

        // GET: Timetable/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var расписание = await _context.Расписание
                .Include(р => р.Сотрудники)
                .FirstOrDefaultAsync(m => m.Код_расписания == id);
            if (расписание == null)
            {
                return NotFound();
            }

            return View(расписание);
        }

        // POST: Timetable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var расписание = await _context.Расписание.FindAsync(id);
            if (расписание != null)
            {
                _context.Расписание.Remove(расписание);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool РасписаниеExists(int id)
        {
            return _context.Расписание.Any(e => e.Код_расписания == id);
        }
    }
}
