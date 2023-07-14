using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using OnlineShopSystem.Core.Contracts;
using OnlineShopSystem.Core.Models.Book;
using OnlineShopSystem.Core.Services;
using System.Xml.Linq;

namespace OnlineShopSystem.Web.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            this._bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await _bookService.GetAllBooksAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddBookViewModel model = await _bookService.GetAddBookModelAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await _bookService.AddBookAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            EditBookViewModel? model = await _bookService.GetBookByIdForEditAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBookViewModel model, int id)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await _bookService.EditBookAsync(model, id);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var bookModel = await _bookService.BookDetailsByIdAsync(id);

            return View(bookModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return RedirectToAction(nameof(All));
        }


        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var user = GetUserId();

            var model = await _bookService.GetMyBooksAsync(user);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorites(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }

            var userId = GetUserId();

            await _bookService.AddBookToFavoritesAsync(userId, book);

            return RedirectToAction(nameof(Favorites));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromFavorites(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }

            var userId = GetUserId();
            await _bookService.RemoveBookFromFavoritesAsync(userId, book);

            return RedirectToAction(nameof(Favorites));
        }

        public async Task<IActionResult> Order()
        {
            return View();
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
