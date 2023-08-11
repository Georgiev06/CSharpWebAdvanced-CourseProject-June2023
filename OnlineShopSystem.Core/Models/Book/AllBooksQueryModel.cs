using System.ComponentModel.DataAnnotations;
using OnlineShopSystem.Core.Models.Book.Enum;

namespace OnlineShopSystem.Core.Models.Book
{
    public class AllBooksQueryModel
    {
        public AllBooksQueryModel()
        {
            this.CurrentPage = 1;
            this.BooksPerPage = 4;

            this.Categories = new HashSet<string>();
            this.Books = new HashSet<AllBookViewModel>();
        }
        
        public int CurrentPage { get; set; }
        public int BooksPerPage { get; set; }
        public int TotalBooks { get; set; }

        [Display(Name = "Search by text")]
        public string? SearchTerm { get; set; }
        public string? Category { get; set; } 

        [Display(Name = "Sort Books by")]
        public BookSorting Sorting { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<AllBookViewModel> Books { get; set; }
    }
}
