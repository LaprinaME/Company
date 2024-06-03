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
    public class EmployeesAppController : Controller
    {
        private readonly CompanyContext _context;

        public EmployeesAppController(CompanyContext context)
        {
            _context = context;
        }

        // GET: EmployeesApp
        public async Task<IActionResult> Index()
        {
            var companyContext = _context.Сотрудники_приложения.Include(с => с.Приложения).Include(с => с.Сотрудники);
            return View(await companyContext.ToListAsync());
        }

        // GET: EmployeesApp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var сотрудники_приложения = await _context.Сотрудники_приложения
                .Include(с => с.Приложения)
                .Include(с => с.Сотрудники)
                .FirstOrDefaultAsync(m => m.Код_сотрудников_приложения == id);
            if (сотрудники_приложения == null)
            {
                return NotFound();
            }

            return View(сотрудники_приложения);
        }

        // GET: EmployeesApp/Create
        public IActionResult Create()
        {
            ViewData["Код_приложения"] = new SelectList(_context.Приложения, "Код_приложения", "Код_приложения");
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника");
            return View();
        }

        // POST: EmployeesApp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Код_сотрудников_приложения,Код_приложения,Код_сотрудника")] Сотрудники_приложения сотрудники_приложения)
        {
            if (ModelState.IsValid)
            {
                _context.Add(сотрудники_приложения);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Код_приложения"] = new SelectList(_context.Приложения, "Код_приложения", "Код_приложения", сотрудники_приложения.Код_приложения);
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника", сотрудники_приложения.Код_сотрудника);
            return View(сотрудники_приложения);
        }

        // GET: EmployeesApp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var сотрудники_приложения = await _context.Сотрудники_приложения.FindAsync(id);
            if (сотрудники_приложения == null)
            {
                return NotFound();
            }
            ViewData["Код_приложения"] = new SelectList(_context.Приложения, "Код_приложения", "Код_приложения", сотрудники_приложения.Код_приложения);
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника", сотрудники_приложения.Код_сотрудника);
            return View(сотрудники_приложения);
        }

        // POST: EmployeesApp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Код_сотрудников_приложения,Код_приложения,Код_сотрудника")] Сотрудники_приложения сотрудники_приложения)
        {
            if (id != сотрудники_приложения.Код_сотрудников_приложения)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(сотрудники_приложения);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Сотрудники_приложенияExists(сотрудники_приложения.Код_сотрудников_приложения))
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
            ViewData["Код_приложения"] = new SelectList(_context.Приложения, "Код_приложения", "Код_приложения", сотрудники_приложения.Код_приложения);
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника", сотрудники_приложения.Код_сотрудника);
            return View(сотрудники_приложения);
        }

        // GET: EmployeesApp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var сотрудники_приложения = await _context.Сотрудники_приложения
                .Include(с => с.Приложения)
                .Include(с => с.Сотрудники)
                .FirstOrDefaultAsync(m => m.Код_сотрудников_приложения == id);
            if (сотрудники_приложения == null)
            {
                return NotFound();
            }

            return View(сотрудники_приложения);
        }

        // POST: EmployeesApp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var сотрудники_приложения = await _context.Сотрудники_приложения.FindAsync(id);
            if (сотрудники_приложения != null)
            {
                _context.Сотрудники_приложения.Remove(сотрудники_приложения);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Сотрудники_приложенияExists(int id)
        {
            return _context.Сотрудники_приложения.Any(e => e.Код_сотрудников_приложения == id);
        }
    }
}
