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
    public class CredentialsController : Controller
    {
        private readonly CompanyContext _context;

        public CredentialsController(CompanyContext context)
        {
            _context = context;
        }

        // GET: Credentials
        public async Task<IActionResult> Index()
        {
            var companyContext = _context.Учетные_данные.Include(у => у.Сотрудники).Include(у => у.Статусы);
            return View(await companyContext.ToListAsync());
        }

        // GET: Credentials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var учетные_данные = await _context.Учетные_данные
                .Include(у => у.Сотрудники)
                .Include(у => у.Статусы)
                .FirstOrDefaultAsync(m => m.Код_учетных_данных == id);
            if (учетные_данные == null)
            {
                return NotFound();
            }

            return View(учетные_данные);
        }

        // GET: Credentials/Create
        public IActionResult Create()
        {
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника");
            ViewData["Код_статуса"] = new SelectList(_context.Статусы, "Код_статуса", "Код_статуса");
            return View();
        }

        // POST: Credentials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Код_учетных_данных,Код_сотрудника,Код_статуса,Срок_действия_учетки,Последний_вход")] Учетные_данные учетные_данные)
        {
            if (ModelState.IsValid)
            {
                _context.Add(учетные_данные);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника", учетные_данные.Код_сотрудника);
            ViewData["Код_статуса"] = new SelectList(_context.Статусы, "Код_статуса", "Код_статуса", учетные_данные.Код_статуса);
            return View(учетные_данные);
        }

        // GET: Credentials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var учетные_данные = await _context.Учетные_данные.FindAsync(id);
            if (учетные_данные == null)
            {
                return NotFound();
            }
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника", учетные_данные.Код_сотрудника);
            ViewData["Код_статуса"] = new SelectList(_context.Статусы, "Код_статуса", "Код_статуса", учетные_данные.Код_статуса);
            return View(учетные_данные);
        }

        // POST: Credentials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Код_учетных_данных,Код_сотрудника,Код_статуса,Срок_действия_учетки,Последний_вход")] Учетные_данные учетные_данные)
        {
            if (id != учетные_данные.Код_учетных_данных)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(учетные_данные);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Учетные_данныеExists(учетные_данные.Код_учетных_данных))
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
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника", учетные_данные.Код_сотрудника);
            ViewData["Код_статуса"] = new SelectList(_context.Статусы, "Код_статуса", "Код_статуса", учетные_данные.Код_статуса);
            return View(учетные_данные);
        }

        // GET: Credentials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var учетные_данные = await _context.Учетные_данные
                .Include(у => у.Сотрудники)
                .Include(у => у.Статусы)
                .FirstOrDefaultAsync(m => m.Код_учетных_данных == id);
            if (учетные_данные == null)
            {
                return NotFound();
            }

            return View(учетные_данные);
        }

        // POST: Credentials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var учетные_данные = await _context.Учетные_данные.FindAsync(id);
            if (учетные_данные != null)
            {
                _context.Учетные_данные.Remove(учетные_данные);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Учетные_данныеExists(int id)
        {
            return _context.Учетные_данные.Any(e => e.Код_учетных_данных == id);
        }
    }
}
