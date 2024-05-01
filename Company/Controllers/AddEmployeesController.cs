using Microsoft.AspNetCore.Mvc;
using Company.DataContext;
using Company.Models;
using Company.ViewModels;
using System.Threading.Tasks;

namespace Company.Controllers
{
    public class AddEmployeesController : Controller
    {
        private readonly CompanyContext _context;

        public AddEmployeesController(CompanyContext context)
        {
            _context = context;
        }

        // GET: AddEmployees/Index
        public IActionResult Index()
        {
            return View();
        }

        // POST: AddEmployees/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployees(AddEmployeesViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Создание нового сотрудника
                var employee = new Сотрудники
                {
                    ФИО = model.ФИО,
                    Пол = model.Пол,
                    Дата_рождения = model.Дата_рождения,
                    СНИЛС = model.СНИЛС,
                    Мобильный_телефон = model.Мобильный_телефон,
                    Адрес_электронной_почты = model.Адрес_электронной_почты,
                    Адрес_проживания = model.Адрес_проживания,
                    Должность = model.Должность,
                    Код_аккаунта = model.Код_аккаунта,
                    Код_статуса = model.Код_статуса,
                    Код_сотрудника = model.Код_сотрудника
                };

                // Добавление сотрудника в базу данных
                _context.Сотрудники.Add(employee);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            // Возвращение представления Index в случае невалидных данных
            return View("Index", model);
        }
    }
}
