using Microsoft.AspNetCore.Mvc;

namespace Company.Controllers
{
    public class MenuAdminController : Controller
    {
        // GET: /MenuAdmin
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /MenuAdmin
        [HttpPost]
        public IActionResult Index(string page)
        {
            if (!string.IsNullOrEmpty(page))
            {
                return Redirect(page);
            }
            else
            {
                // Если не выбрана страница, просто возвращаем текущую страницу
                return RedirectToAction("Index");
            }
        }
    }
}