using BB_01._15._2024_Template.DAL;
using BB_01._15._2024_Template.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BB_01._15._2024_Template.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<News> news= _context.SomeNews.ToList();
            return View(news);
        }
    }
}