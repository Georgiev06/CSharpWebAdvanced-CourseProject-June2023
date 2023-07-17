using Microsoft.AspNetCore.Mvc;
using OnlineShopSystem.Core.Contracts;
using OnlineShopSystem.Infrastructure.Data.Models;
using System.Security.Claims;
using OnlineShopSystem.Core.Models.Cart;

namespace OnlineShopSystem.Web.Controllers
{
    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService _cartService;

        public ShoppingCartController(IShoppingCartService cartService)
        {
            this._cartService = cartService;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userId = claim.Value;

            var cart = _cartService.GetCartByUserId(userId);

            var cartViewModel = new ShoppingCartViewModel
            {
                UserId = userId,
                Items = cart?.Books.Select(book => new ShoppingCartItemViewModel
                {
                    BookId = book.Id,
                    BookTitle = book.Title,
                    BookAuthor = book.Author,
                    BookPrice = book.Price,
                    BookImageUrl = book.ImageUrl,
                    BookDescription = book.Description,
                    BookRating = book.Rating.ToString(),
                    //Quantity = book.Count
                }).ToList() ?? new List<ShoppingCartItemViewModel>()
            };

            return View(cartViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(string userId, int bookId, int quantity)
        {
            await _cartService.AddBookToCartAsync(userId, bookId, quantity);
            return RedirectToAction(nameof(Index), new { userId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(string userId, int bookId)
        {
            await _cartService.RemoveBookFromCartAsync(userId, bookId);
            return RedirectToAction(nameof(Index), new { userId });
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart(string userId)
        {
            await _cartService.ClearCartAsync(userId);
            return RedirectToAction(nameof(Index), new { userId });
        }
    }
}
