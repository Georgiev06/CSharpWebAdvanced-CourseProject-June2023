using Microsoft.AspNetCore.Mvc;
using OnlineShopSystem.Core.Contracts;

namespace OnlineShopSystem.Web.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        public async Task<IActionResult> All()
        {
            var allUsers = await _userService.GetUsersAsync();

            return View(allUsers);
        }
    }
}
