using Microsoft.AspNetCore.Mvc;
using OnlineShopSystem.Core.Contracts;
using OnlineShopSystem.Core.Models.Book;
using OnlineShopSystem.Core.Services;
using System.Xml.Linq;

namespace OnlineShopSystem.Web.Controllers
{
    public class BookController : Controller
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
            EditBookViewModel? book = await _bookService.GetBookByIdForEditAsync(id);

            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(book);
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

    }
}
