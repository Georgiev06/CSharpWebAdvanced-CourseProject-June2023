namespace OnlineShopSystem.Core.Models.Book
{
    public class AllBooksFilteredAndPagedServiceModel
    {
        public AllBooksFilteredAndPagedServiceModel()
        {
            Books = new HashSet<AllBookViewModel>();
        }

        public int TotalBooksCount { get; set; }

        public IEnumerable<AllBookViewModel> Books { get; set; }
    }
}
