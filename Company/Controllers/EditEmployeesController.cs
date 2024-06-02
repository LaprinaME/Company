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
    public class EditEmployeesController : Controller
    {
        private readonly CompanyContext _context;

        public EditEmployeesController(CompanyContext context)
        {
            _context = context;
        }

        // GET: EditEmployees
        public async Task<IActionResult> Index()
        {
            var companyContext = _context.Сотрудники.Include(с => с.Аккаунты).Include(с => с.Статусы);
            return View(await companyContext.ToListAsync());
        }

        // GET: EditEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var сотрудники = await _context.Сотрудники
                .Include(с => с.Аккаунты)
                .Include(с => с.Статусы)
                .FirstOrDefaultAsync(m => m.Код_сотрудника == id);
            if (сотрудники == null)
            {
                return NotFound();
            }

            return View(сотрудники);
        }

        // GET: EditEmployees/Create
        public IActionResult Create()
        {
            ViewData["Код_аккаунта"] = new SelectList(_context.Аккаунты, "Код_аккаунта", "Логин");
            ViewData["Код_статуса"] = new SelectList(_context.Статусы, "Код_статуса", "Код_статуса");
            return View();
        }

        // POST: EditEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Код_сотрудника,Код_статуса,Код_аккаунта,ФИО,Пол,Дата_рождения,СНИЛС,Мобильный_телефон,Адрес_электронной_почты,Адрес_проживания,Должность")] Сотрудники сотрудники)
        {
            if (ModelState.IsValid)
            {
                _context.Add(сотрудники);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Код_аккаунта"] = new SelectList(_context.Аккаунты, "Код_аккаунта", "Логин", сотрудники.Код_аккаунта);
            ViewData["Код_статуса"] = new SelectList(_context.Статусы, "Код_статуса", "Код_статуса", сотрудники.Код_статуса);
            return View(сотрудники);
        }

        // GET: EditEmployees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var сотрудники = await _context.Сотрудники.FindAsync(id);
            if (сотрудники == null)
            {
                return NotFound();
            }
            ViewData["Код_аккаунта"] = new SelectList(_context.Аккаунты, "Код_аккаунта", "Логин", сотрудники.Код_аккаунта);
            ViewData["Код_статуса"] = new SelectList(_context.Статусы, "Код_статуса", "Код_статуса", сотрудники.Код_статуса);
            return View(сотрудники);
        }

        // POST: EditEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Код_сотрудника,Код_статуса,Код_аккаунта,ФИО,Пол,Дата_рождения,СНИЛС,Мобильный_телефон,Адрес_электронной_почты,Адрес_проживания,Должность")] Сотрудники сотрудники)
        {
            if (id != сотрудники.Код_сотрудника)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(сотрудники);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!СотрудникиExists(сотрудники.Код_сотрудника))
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
            ViewData["Код_аккаунта"] = new SelectList(_context.Аккаунты, "Код_аккаунта", "Логин", сотрудники.Код_аккаунта);
            ViewData["Код_статуса"] = new SelectList(_context.Статусы, "Код_статуса", "Код_статуса", сотрудники.Код_статуса);
            return View(сотрудники);
        }

        // GET: EditEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var сотрудники = await _context.Сотрудники
                .Include(с => с.Аккаунты)
                .Include(с => с.Статусы)
                .FirstOrDefaultAsync(m => m.Код_сотрудника == id);
            if (сотрудники == null)
            {
                return NotFound();
            }

            return View(сотрудники);
        }

        // POST: EditEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var сотрудники = await _context.Сотрудники.FindAsync(id);
            if (сотрудники != null)
            {
                _context.Сотрудники.Remove(сотрудники);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool СотрудникиExists(int id)
        {
            return _context.Сотрудники.Any(e => e.Код_сотрудника == id);
        }
    }
}
