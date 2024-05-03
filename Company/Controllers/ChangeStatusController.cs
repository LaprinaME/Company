using Microsoft.AspNetCore.Mvc;
using Company.DataContext;
using Company.Models;
using Company.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Controllers
{
    public class ChangeStatusController : Controller
    {
        private readonly CompanyContext _context;

        public ChangeStatusController(CompanyContext context)
        {
            _context = context;
        }

        // GET: ChangeStatus
        public IActionResult Index()
        {
            var viewModel = new ChangeStatusViewModel();

            viewModel.Employees = _context.Сотрудники
                .Select(e => new EmployeeViewModel
                {
                    EmployeeId = e.Код_сотрудника,
                    FullName = e.ФИО
                }).ToList();

            viewModel.Statuses = _context.Статусы
                .Select(s => new StatusViewModel
                {
                    StatusId = s.Код_статуса,
                    StatusName = s.Название_статуса
                }).ToList();

            return View(viewModel);
        }

        // POST: ChangeStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(ChangeStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = await _context.Сотрудники.FindAsync(model.EmployeeId);
                if (employee != null)
                {
                    employee.Код_статуса = model.StatusId;
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View("Index", model);
        }
    }
}
