using Microsoft.AspNetCore.Mvc;
using Company.Models;
using Company.DataContext;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    public class EditEmployeesController : Controller
    {
        private readonly CompanyContext _context;

        public EditEmployeesController(CompanyContext context)
        {
            _context = context;
        }

        // GET: EditEmployees/Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Сотрудники.ToListAsync());
        }

        // GET: EditEmployees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Сотрудники.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Код_сотрудника,Код_статуса,Код_аккаунта,ФИО,Пол,Дата_рождения,СНИЛС,Мобильный_телефон,Адрес_электронной_почты,Адрес_проживания,Должность")] Сотрудники employee)
        {
            if (id != employee.Код_сотрудника)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Код_сотрудника))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "An error occurred while saving changes. Please try again.");
                }
                return RedirectToAction("Index", "Menu");
            }
            return View(employee);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Сотрудники.Any(e => e.Код_сотрудника == id);
        }
    }
}
