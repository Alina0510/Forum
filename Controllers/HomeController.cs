using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Forum.Models;

namespace Forum.Controllers
{
    public class HomeController : Controller
    {
        Models.AppContext db;
        public HomeController(Models.AppContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}