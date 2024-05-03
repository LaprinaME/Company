using Microsoft.AspNetCore.Mvc;
using Company.DataContext;
using System.Linq;

namespace Company.Controllers
{
    public class OrganizationsController : Controller
    {
        private readonly CompanyContext _context;

        public OrganizationsController(CompanyContext context)
        {
            _context = context;
        }

        // GET: Organizations
        public IActionResult Index()
        {
            var organizations = _context.Организации.ToList();
            return View(organizations);
        }
    }
}
