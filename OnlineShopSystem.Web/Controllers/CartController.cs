using Microsoft.AspNetCore.Mvc;
using OnlineShopSystem.Infrastructure.Data.Models;

namespace OnlineShopSystem.Web.Controllers
{
    public class CartController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }

        public  IActionResult AddToCart()
        {
            return View();
        }

        public IActionResult RemoveFromCart()
        {
            return View();
        }

        public IActionResult ClearCart()
        {
            return View();
        }
    }
}
