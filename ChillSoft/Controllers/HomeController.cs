using Microsoft.AspNetCore.Mvc;

namespace Chillisoft.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
