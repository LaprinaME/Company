using Company.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Company.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;

namespace Company.Controllers
{
    public class HomeController : Controller
    {
        // Действие для отображения главной страницы
        public IActionResult Index()
        {
            return View();
        }
    }
}
