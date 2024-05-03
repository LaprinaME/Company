using Microsoft.AspNetCore.Mvc;
using Company.Models;
using Company.DataContext;

namespace Company.Controllers
{
    public class AddOrganizationController : Controller
    {
        private readonly CompanyContext _context;

        public AddOrganizationController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Название_организации,ИНН,КПП,ОГРН,Дата_закрытия")] Организации организации)
        {
            if (ModelState.IsValid)
            {
                _context.Add(организации);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Предполагается, что у вас есть метод Index для отображения списка организаций
            }
            return View(организации);
        }
    }
}
