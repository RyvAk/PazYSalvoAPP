using Microsoft.AspNetCore.Mvc;

namespace PazYSalvoAPP.WebApp.Controllers.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
