using Microsoft.AspNetCore.Mvc;
using OnlineShopSystem.Core.Contracts;
using OnlineShopSystem.Infrastructure.Data.Models;
using System.Security.Claims;
using OnlineShopSystem.Core.Models.Cart;
using Microsoft.EntityFrameworkCore;
using OnlineShopSystem.Web.Data;

namespace OnlineShopSystem.Web.Controllers
{
    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService _cartService;
        private readonly BookShopDbContext _context;

        public ShoppingCartController(IShoppingCartService cartService, BookShopDbContext _context)
        {
            this._cartService = cartService;
            this._context = _context;
        }

        private string GetUserId()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            return claim.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = GetUserId();

            var cart = _cartService.GetCartByUserId(userId);

            var cartViewModel = new ShoppingCartViewModel
            {
                UserId = userId,
                Items = cart?.Books.Select(book => new ShoppingCartItemViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    //Category = book.Category.Name,
                    Price = book.Price,
                    ImageUrl = book.ImageUrl,
                    Description = book.Description,
                    Rating = book.Rating.ToString(),
                }).ToList() ?? new List<ShoppingCartItemViewModel>()
            };

            return View(cartViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int bookId)
        {
            var userId = GetUserId();

            await _cartService.AddBookToCartAsync(userId, bookId);

            TempData["message"] = "Book added to cart successfully!";

            return RedirectToAction("Index", "ShoppingCart");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int bookId)
        {
            var userId = GetUserId();

            await _cartService.RemoveBookFromCartAsync(userId, bookId);

            TempData["message"] = "Book removed from cart successfully!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            var userId = GetUserId();

            await _cartService.ClearCartAsync(userId);

            TempData["message"] = "Cart cleared successfully!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ProceedToPay()
        {
            var userId = GetUserId();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            var cart = _cartService.GetCartByUserId(user.Id);

            var order = new Order
            {
                UserFirstName = user.FirstName,
                UserLastName = user.LastName,
                TotalAmount = cart.TotalPrice

            };

            if (cart.Books.Any())
            {
                foreach (var book in cart.Books)
                {
                    order.Books.Add(book);
                    order.TotalAmount += book.Price;
                }
            }

            await _context.Orders.AddAsync(order);

            user.Orders.Add(order);

            await _context.SaveChangesAsync();

            await _cartService.ClearCartAsync(userId);

            return RedirectToAction("Index", "Home");
        }

    }
}
