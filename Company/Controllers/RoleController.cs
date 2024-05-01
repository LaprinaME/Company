using Microsoft.AspNetCore.Mvc;
using Company.DataContext;
using Company.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    public class RoleController : Controller
    {
        private readonly CompanyContext _context;

        public RoleController(CompanyContext context)
        {
            _context = context;
        }

        // GET: /Role/Index
        public async Task<IActionResult> Index()
        {
            var roles = await _context.Роли.ToListAsync();
            return View(roles);
        }
    }
}
