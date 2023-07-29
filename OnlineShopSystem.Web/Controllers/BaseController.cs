using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopSystem.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        
    }
}
