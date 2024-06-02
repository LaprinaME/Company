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
    public class StaffController : Controller
    {
        private readonly CompanyContext _context;

        public StaffController(CompanyContext context)
        {
            _context = context;
        }

        // GET: Staff
        public async Task<IActionResult> Index()
        {
            var companyContext = _context.Кадровые_назначения.Include(к => к.Организации).Include(к => к.Отделы).Include(к => к.Периоды_неисполнения_обязанностей).Include(к => к.Сотрудники);
            return View(await companyContext.ToListAsync());
        }

        // GET: Staff/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var кадровые_назначения = await _context.Кадровые_назначения
                .Include(к => к.Организации)
                .Include(к => к.Отделы)
                .Include(к => к.Периоды_неисполнения_обязанностей)
                .Include(к => к.Сотрудники)
                .FirstOrDefaultAsync(m => m.Код_назначения == id);
            if (кадровые_назначения == null)
            {
                return NotFound();
            }

            return View(кадровые_назначения);
        }

        // GET: Staff/Create
        public IActionResult Create()
        {
            ViewData["Код_организации"] = new SelectList(_context.Организации, "Код_организации", "Код_организации");
            ViewData["Код_отдела"] = new SelectList(_context.Отделы, "Код_отдела", "Код_отдела");
            ViewData["Код_периода"] = new SelectList(_context.Периоды_неисполнения_обязанностей, "Код_периода", "Код_периода");
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника");
            return View();
        }

        // POST: Staff/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Код_назначения,Код_сотрудника,Код_организации,Код_периода,Код_отдела,Должность,Вид_занятости,ФИО_руководителя,Табельный_номер,Номер_кабинета,Рабочий_телефон,Рабочий_адрес_электронной_почты")] Кадровые_назначения кадровые_назначения)
        {
            if (ModelState.IsValid)
            {
                _context.Add(кадровые_назначения);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Код_организации"] = new SelectList(_context.Организации, "Код_организации", "Код_организации", кадровые_назначения.Код_организации);
            ViewData["Код_отдела"] = new SelectList(_context.Отделы, "Код_отдела", "Код_отдела", кадровые_назначения.Код_отдела);
            ViewData["Код_периода"] = new SelectList(_context.Периоды_неисполнения_обязанностей, "Код_периода", "Код_периода", кадровые_назначения.Код_периода);
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника", кадровые_назначения.Код_сотрудника);
            return View(кадровые_назначения);
        }

        // GET: Staff/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var кадровые_назначения = await _context.Кадровые_назначения.FindAsync(id);
            if (кадровые_назначения == null)
            {
                return NotFound();
            }
            ViewData["Код_организации"] = new SelectList(_context.Организации, "Код_организации", "Код_организации", кадровые_назначения.Код_организации);
            ViewData["Код_отдела"] = new SelectList(_context.Отделы, "Код_отдела", "Код_отдела", кадровые_назначения.Код_отдела);
            ViewData["Код_периода"] = new SelectList(_context.Периоды_неисполнения_обязанностей, "Код_периода", "Код_периода", кадровые_назначения.Код_периода);
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника", кадровые_назначения.Код_сотрудника);
            return View(кадровые_назначения);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Код_назначения,Код_сотрудника,Код_организации,Код_периода,Код_отдела,Должность,Вид_занятости,ФИО_руководителя,Табельный_номер,Номер_кабинета,Рабочий_телефон,Рабочий_адрес_электронной_почты")] Кадровые_назначения кадровые_назначения)
        {
            if (id != кадровые_назначения.Код_назначения)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(кадровые_назначения);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Кадровые_назначенияExists(кадровые_назначения.Код_назначения))
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
            ViewData["Код_организации"] = new SelectList(_context.Организации, "Код_организации", "Код_организации", кадровые_назначения.Код_организации);
            ViewData["Код_отдела"] = new SelectList(_context.Отделы, "Код_отдела", "Код_отдела", кадровые_назначения.Код_отдела);
            ViewData["Код_периода"] = new SelectList(_context.Периоды_неисполнения_обязанностей, "Код_периода", "Код_периода", кадровые_назначения.Код_периода);
            ViewData["Код_сотрудника"] = new SelectList(_context.Сотрудники, "Код_сотрудника", "Код_сотрудника", кадровые_назначения.Код_сотрудника);
            return View(кадровые_назначения);
        }

        // GET: Staff/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var кадровые_назначения = await _context.Кадровые_назначения
                .Include(к => к.Организации)
                .Include(к => к.Отделы)
                .Include(к => к.Периоды_неисполнения_обязанностей)
                .Include(к => к.Сотрудники)
                .FirstOrDefaultAsync(m => m.Код_назначения == id);
            if (кадровые_назначения == null)
            {
                return NotFound();
            }

            return View(кадровые_назначения);
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var кадровые_назначения = await _context.Кадровые_назначения.FindAsync(id);
            if (кадровые_назначения != null)
            {
                _context.Кадровые_назначения.Remove(кадровые_назначения);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Кадровые_назначенияExists(int id)
        {
            return _context.Кадровые_назначения.Any(e => e.Код_назначения == id);
        }
    }
}
