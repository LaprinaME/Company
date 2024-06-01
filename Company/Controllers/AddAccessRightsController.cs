using Microsoft.AspNetCore.Mvc;
using Company.DataContext;
using Company.Models;
using Company.ViewModels;
using System.Threading.Tasks;

namespace Company.Controllers
{
    public class AddAccessRightsController : Controller
    {
        private readonly CompanyContext _context;

        public AddAccessRightsController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddAccessRightsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var accessRight = new Права_доступа
                {
                    Код_права = model.Код_права,
                    Код_роли = model.Код_роли,
                    Код_приложения = model.Код_приложения,
                    Название_права = model.Название_права,
                    Начало_действия_доступа = model.Начало_действия_доступа,
                    Окончание_действия_доступа = model.Окончание_действия_доступа,
                    Тип_доступа = model.Тип_доступа
                };

                _context.Права_доступа.Add(accessRight);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }
       
    }
}
