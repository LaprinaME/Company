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
    public class AccessController : Controller
    {
        private readonly CompanyContext _context;

        public AccessController(CompanyContext context)
        {
            _context = context;
        }

        // GET: Access
        public async Task<IActionResult> Index()
        {
            var companyContext = _context.Ограничения_доступа.Include(о => о.Расписание).Include(о => о.Сотрудники);
            return View(await companyContext.ToListAsync());
        }

        // GET: Access/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ограничения_доступа = await _context.Ограничения_доступа
                .Include(о => о.Расписание)
                .Include(о => о.Сотрудники)
                .FirstOrDefaultAsync(m => m.Код_ограничения == id);
            if (ограничения_доступа == null)
            {
                return NotFound();
            }

            return View(ограничения_доступа);
        }

        // GET: Access/Create
        public IActionResult Create()
        {
            ViewData["Код_расписания"] = new SelectList(_context.Расписание, "Код_расписания", "Код_расписания");
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника");
            return View();
        }

        // POST: Access/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Код_ограничения,Код_сотрудника,Код_расписания,Период_доступа_с,Период_доступа_по,Доступ_по_IP")] Ограничения_доступа ограничения_доступа)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ограничения_доступа);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Код_расписания"] = new SelectList(_context.Расписание, "Код_расписания", "Код_расписания", ограничения_доступа.Код_расписания);
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника", ограничения_доступа.Код_сотрудника);
            return View(ограничения_доступа);
        }

        // GET: Access/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ограничения_доступа = await _context.Ограничения_доступа.FindAsync(id);
            if (ограничения_доступа == null)
            {
                return NotFound();
            }
            ViewData["Код_расписания"] = new SelectList(_context.Расписание, "Код_расписания", "Код_расписания", ограничения_доступа.Код_расписания);
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника", ограничения_доступа.Код_сотрудника);
            return View(ограничения_доступа);
        }

        // POST: Access/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Код_ограничения,Код_сотрудника,Код_расписания,Период_доступа_с,Период_доступа_по,Доступ_по_IP")] Ограничения_доступа ограничения_доступа)
        {
            if (id != ограничения_доступа.Код_ограничения)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ограничения_доступа);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Ограничения_доступаExists(ограничения_доступа.Код_ограничения))
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
            ViewData["Код_расписания"] = new SelectList(_context.Расписание, "Код_расписания", "Код_расписания", ограничения_доступа.Код_расписания);
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника", ограничения_доступа.Код_сотрудника);
            return View(ограничения_доступа);
        }

        // GET: Access/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ограничения_доступа = await _context.Ограничения_доступа
                .Include(о => о.Расписание)
                .Include(о => о.Сотрудники)
                .FirstOrDefaultAsync(m => m.Код_ограничения == id);
            if (ограничения_доступа == null)
            {
                return NotFound();
            }

            return View(ограничения_доступа);
        }

        // POST: Access/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ограничения_доступа = await _context.Ограничения_доступа.FindAsync(id);
            if (ограничения_доступа != null)
            {
                _context.Ограничения_доступа.Remove(ограничения_доступа);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Ограничения_доступаExists(int id)
        {
            return _context.Ограничения_доступа.Any(e => e.Код_ограничения == id);
        }
    }
}
