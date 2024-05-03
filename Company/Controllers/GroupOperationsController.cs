using Microsoft.AspNetCore.Mvc;
using Company.DataContext;
using Company.ViewModels;
using Company.Models;
using System.Threading.Tasks;

namespace Company.Controllers
{
    public class GroupOperationsController : Controller
    {
        private readonly CompanyContext _context;

        public GroupOperationsController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new GroupOperationsViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(GroupOperationsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                
                // Перенаправление на главную страницу
                return RedirectToAction("Index", "Home");
            }

            // Если модель невалидна, возвращаем представление с моделью для исправления ошибок
            return View(viewModel);
        }
    }
}
