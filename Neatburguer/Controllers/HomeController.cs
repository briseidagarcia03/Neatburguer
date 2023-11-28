using Microsoft.AspNetCore.Mvc;

namespace Neatburguer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
