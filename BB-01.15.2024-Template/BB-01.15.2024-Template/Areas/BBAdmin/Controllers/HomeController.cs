using Microsoft.AspNetCore.Mvc;

namespace BB_01._15._2024_Template.Areas.BBAdmin.Controllers
{
    [Area("BBAdmin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
