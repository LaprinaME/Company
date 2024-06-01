using Microsoft.AspNetCore.Mvc;
using Company.DataContext;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            var organizations = from o in _context.Организации
                                select o;

            if (!String.IsNullOrEmpty(searchString))
            {
                organizations = organizations.Where(o => o.Название_организации.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    organizations = organizations.OrderByDescending(o => o.Название_организации);
                    break;
                case "Date":
                    organizations = organizations.OrderBy(o => o.Дата_закрытия);
                    break;
                case "date_desc":
                    organizations = organizations.OrderByDescending(o => o.Дата_закрытия);
                    break;
                default:
                    organizations = organizations.OrderBy(o => o.Название_организации);
                    break;
            }

            return View(await organizations.AsNoTracking().ToListAsync());
        }
    }
}
