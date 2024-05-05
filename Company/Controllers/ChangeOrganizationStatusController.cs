using Microsoft.AspNetCore.Mvc;
using Company.Models;
using System.Threading.Tasks;
using Company.DataContext;
using Company.ViewModels;

namespace Company.Controllers
{
    public class ChangeOrganizationStatusController : Controller
    {
        private readonly CompanyContext _context;

        public ChangeOrganizationStatusController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new ChangeOrganizationStatusViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeOrganizationStatus(ChangeOrganizationStatusViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var organization = new Организации
                {
                    Код_организации = viewModel.Код_организации,
                    Название_организации = viewModel.Название_организации,
                    Код_статуса = viewModel.Код_статуса,
                    Код_родительской_организации = viewModel.Код_родительской_организации
                };

                _context.Update(organization);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }
    }
}
