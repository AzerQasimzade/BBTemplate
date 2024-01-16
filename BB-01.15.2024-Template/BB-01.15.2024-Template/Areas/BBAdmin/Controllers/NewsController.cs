using BB_01._15._2024_Template.Areas.BBAdmin.ViewModels.News;
using BB_01._15._2024_Template.DAL;
using BB_01._15._2024_Template.Models;
using BB_01._15._2024_Template.Utilities.Enums;
using BB_01._15._2024_Template.Utilities.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BB_01._15._2024_Template.Areas.BBAdmin.Controllers
{
    [Area("BBAdmin")]
    public class NewsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public NewsController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<News> news = await _context.SomeNews.ToListAsync();            
            return View(news);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateNewsVM createVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!createVM.Photo.ValidateFileType(FileHelper.Image))
            {
                ModelState.AddModelError("Photo", "File type is not correct");
                return View();
            }
            if (!createVM.Photo.ValidateSizeType(SizeHelper.gb))
            {
                ModelState.AddModelError("Photo", "File size is not correct");
                return View();
            }
            string filename=Guid.NewGuid().ToString()+createVM.Photo.FileName;
            string path = Path.Combine(_env.WebRootPath, "admin", "images",filename);
            FileStream file = new FileStream(path, FileMode.Create);
            await createVM.Photo.CopyToAsync(file);
            News newNews = new News
            {
                Image=filename,
                Title = createVM.Title,
                Description = createVM.Description,
                ByName = createVM.ByName,
            };
            await _context.SomeNews.AddAsync(newNews);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            News existed = await _context.SomeNews.FirstOrDefaultAsync(x => x.Id == id);
            if (existed is null)
            {
                return NotFound();
            }
            UpdateNewsVM newsVM = new UpdateNewsVM
            {
                ByName = existed.ByName,
                Description = existed.Description,
                Title = existed.Title,
                Photo=existed.Photo
            };
               
            return View(newsVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateNewsVM newsVM)
        {
            News existed = await _context.SomeNews.FirstOrDefaultAsync(x => x.Id == id);
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (newsVM.Photo is not null)
            {
                if (!newsVM.Photo.ValidateFileType(FileHelper.Image))
                {
                    ModelState.AddModelError("Photo", "File type is not correct");
                    return View();
                }
                if (!newsVM.Photo.ValidateSizeType(SizeHelper.gb))
                {
                    ModelState.AddModelError("Photo", "File size is not correct");
                    return View();
                }
                string filename = Guid.NewGuid().ToString() + newsVM.Photo.FileName;
                string path = Path.Combine(_env.WebRootPath, "admin", "images", filename);
                FileStream file = new FileStream(path, FileMode.Create);
                await newsVM.Photo.CopyToAsync(file);
                existed.Image.DeleteFile(_env.WebRootPath, "admin", "images");
                existed.Image=filename;
            }
            existed.ByName = newsVM.ByName;
            existed.Description= newsVM.Description;
            existed.Title= newsVM.Title;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            News news = await _context.SomeNews.FirstOrDefaultAsync(x => x.Id == id);

            if (news is null)
            {
                return NotFound();
            }
            _context.SomeNews.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
