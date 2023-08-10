using OnlineShopSystem.Core.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopSystem.Core.Models.Review;

namespace OnlineShopSystem.Core.Contracts
{
    public interface IBookService
    {
        Task<AllBooksFilteredAndPagedServiceModel> GetAllBooksAsync(AllBooksQueryModel queryModel, string userId);
        Task AddBookAsync( AddBookViewModel model);
        Task<AddBookViewModel> GetAddBookModelAsync();
        Task EditBookAsync(EditBookViewModel model, int id);
        Task<EditBookViewModel?> GetBookByIdForEditAsync(int id);
        Task<BookViewModel?> GetBookByIdAsync(int id);
        Task<IEnumerable<AllBookViewModel>> GetMyBooksAsync(string userId);
        Task DeleteBookAsync(int id);
        Task<DetailsBookViewModel> BookDetailsByIdAsync(int id);
        Task AddBookToFavoritesAsync(string userId, BookViewModel book);
        Task RemoveBookFromFavoritesAsync(string userId, BookViewModel book);
        Task AddReviewAsync(AddReviewViewModel review);
        Task<IEnumerable<ReviewViewModel>> GetReviewAsync(int bookId);

    }
}
