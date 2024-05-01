using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Company.DataContext;
using Company.Models;
using System.Threading.Tasks;

namespace Company.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly CompanyContext _context;

        public EmployeesController(CompanyContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Сотрудники
                .Include(e => e.Статусы)
                .Include(e => e.Аккаунты)
                .ToListAsync();
            return View(employees);
        }
    }
}
