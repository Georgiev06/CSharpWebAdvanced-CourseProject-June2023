using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopSystem.Core.Contracts;
using OnlineShopSystem.Web.Data;

namespace OnlineShopSystem.Web.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly BookShopDbContext _data;
        private readonly IUserService _userService;

        public UserController(BookShopDbContext data, IUserService userService)
        {
            this._data = data;
            this._userService = userService;
        }
        public async Task<IActionResult> All()
        {
            var allUsers = await _userService.GetUsersAsync();

            return View(allUsers);
        }

        [HttpPost]
        public async Task<IActionResult> MakeAdmin(string id)
        {
            var user = await _data.Users.FirstOrDefaultAsync(u => u.Id == id);

            var isAdmin = await _data.UserRoles.AnyAsync(ur => ur.UserId == id);

            // If the user is not already an admin, add the "Admin" role to the user
            if (user != null && !isAdmin)
            {
                await _data.UserRoles.AddAsync(new IdentityUserRole<string>() { UserId = id});
                await _data.SaveChangesAsync();
            }

            return RedirectToAction("All", "User");
        }

        [HttpPost]
        public IActionResult DeleteUser(string id)
        {
            var user = _data.Users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                _data.Users.Remove(user);
                _data.SaveChanges();
            }

            return RedirectToAction("All", "User");
        }
    }
}
