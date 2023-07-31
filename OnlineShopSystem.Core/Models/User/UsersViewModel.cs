namespace OnlineShopSystem.Web.Areas.Admin.Models.User
{
    public class UsersViewModel
    {
        public string Id { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public bool IsAdmin { get; set; }
    }
}
