using Microsoft.AspNetCore.Mvc;
using OnlineShopSystem.Web.Models;
using System.Diagnostics;

namespace OnlineShopSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("Error404");
            }

            return View();
        }
    }
}