using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using OnlineShopSystem.Core.Contracts;
using OnlineShopSystem.Core.Models.Book;
using OnlineShopSystem.Core.Services;
using System.Xml.Linq;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using OnlineShopSystem.Core.Models.Review;

namespace OnlineShopSystem.Web.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;

        public BookController(IBookService bookService, ICategoryService categoryService)
        {
            this._bookService = bookService;
            this._categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> All([FromQuery]AllBooksQueryModel queryModel)
        {
            var userId = GetUserId();

            var serviceModel = await _bookService.GetAllBooksAsync(queryModel, userId);

            queryModel.Books = serviceModel.Books;
            queryModel.TotalBooks = serviceModel.TotalBooksCount;
            queryModel.Categories = await this._categoryService.GetAllCategoryNamesAsync();

            return View(queryModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            AddBookViewModel model = await _bookService.GetAddBookModelAsync();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            var userId = GetUserId();

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await _bookService.AddBookAsync(userId, model);

            TempData["message"] = "Book added successfully!";

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(EditBookViewModel model, int id)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await _bookService.EditBookAsync(model, id);

            TempData["message"] = "Book edited successfully!";

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var bookModel = await _bookService.BookDetailsByIdAsync(id);

            var allReviews = await _bookService.GetReviewAsync(id);

            bookModel.Reviews = allReviews;

            return View(bookModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBookAsync(id);

            TempData["message"] = "Book deleted successfully!";
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

            TempData["message"] = "Book added to favorites successfully!";

            return RedirectToAction(nameof(Favorites));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorites(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }

            var userId = GetUserId();
            await _bookService.RemoveBookFromFavoritesAsync(userId, book);

            TempData["message"] = "Book removed from favorites successfully!";

            return RedirectToAction(nameof(Favorites));
        }

        [HttpGet]
        public IActionResult CreateReview(int bookId)
        {
            var userId = GetUserId();

            var model = new AddReviewViewModel()
            {
                UserId = userId,
                BookId = bookId,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View("CreateReview", model);
            }

            await this._bookService.AddReviewAsync(model);

            TempData["message"] = "Review added successfully!";

            return RedirectToAction(nameof(Details), new DetailsBookViewModel {Id = model.BookId});
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
