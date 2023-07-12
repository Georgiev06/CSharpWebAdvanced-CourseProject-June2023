using OnlineShopSystem.Core.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopSystem.Core.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync();
        Task AddBookAsync(AddBookViewModel model);
        Task<AddBookViewModel> GetAddBookModelAsync();
        Task EditBookAsync(EditBookViewModel model, int id);
        Task<EditBookViewModel?> GetBookByIdForEditAsync(int id);
        Task<BookViewModel?> GetBookByIdAsync(int id);
        Task<IEnumerable<AllBookViewModel>> GetMyBooksAsync(string userId);
        Task DeleteBookAsync(int id);
        Task AddBookToFavoritesAsync(string userId, BookViewModel book);
        Task RemoveBookFromFavoritesAsync(string userId, BookViewModel book);
    }
}
