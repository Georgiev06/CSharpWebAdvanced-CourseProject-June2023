namespace OnlineShopSystem.Core.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<string>> GetAllCategoryNamesAsync();
    }
}
