using Microsoft.AspNetCore.Mvc;

namespace GameManagement.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}