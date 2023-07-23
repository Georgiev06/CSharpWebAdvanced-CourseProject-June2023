﻿using Microsoft.AspNetCore.Mvc;
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
            return RedirectToAction("Index", "ShoppingCart");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int bookId)
        {
            var userId = GetUserId();

            await _cartService.RemoveBookFromCartAsync(userId, bookId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            var userId = GetUserId();

            await _cartService.ClearCartAsync(userId);
            return RedirectToAction(nameof(Index));
        }
    }
}