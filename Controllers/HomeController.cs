using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Forum.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Topics");
            }
            return View(await db.Users.ToListAsync());
        }
        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Topics()
        {
            return View(Utils.TopicsToDiplayable(db.Topics.ToList(), db.Users.ToList()));
        }
        public IActionResult Posts()
        {
            var i = Utils.TopicWithPostsById(int.Parse(HttpContext.Request.Query.FirstOrDefault(i => i.Key == "topicId").Value.ToString()), db);
            return View(i);
        }
        [HttpPost]
        public async Task<IActionResult> Registration(UserInput userToValidate)
        {
            if (ModelState.IsValid)
            {
                User user = Utils.ConvertUserInputToUser(userToValidate);
                if (db.Users.Any(i => i.Nickname == user.Nickname))
                {
                    ViewBag.Error = "Such Nickname is already used.";
                    return View(userToValidate);
                }
                if (db.Users.Any(i => i.Email == user.Email))
                {
                    ViewBag.Error = "Such Email is already used.";
                    return View(userToValidate);
                }
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(userToValidate);
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserInput loginUser)
        {
            User user = db.Users.FirstOrDefault(i => i.Email == loginUser.Email && i.Password == loginUser.Password);
            if (user == null)
            {
                return View(loginUser);
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, loginUser.Email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return View("Topics", Utils.TopicsToDiplayable(db.Topics.ToList(), db.Users.ToList()));
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> NewTopic(string header)
        {
            User user = db.Users.FirstOrDefault(i => i.Email == ControllerContext.HttpContext.User.Claims.ToArray()[0].Value);
            var topic = Utils.NewTopic(header, user);
            db.Topics.Add(topic);
            db.SaveChanges();
            return View("Topics", Utils.TopicsToDiplayable(db.Topics.ToList(), db.Users.ToList()));
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> NewPost(string body, int topicId)
        {
            TopicWithPosts topic = Utils.TopicWithPostsById(topicId, db);
            User user = db.Users.FirstOrDefault(i => i.Email == ControllerContext.HttpContext.User.Claims.ToArray()[0].Value);
            var newPost = Utils.NewPost(body, user, db.Topics.FirstOrDefault(i => i.Id == topic.Id));
            db.Posts.Add(newPost);
            await db.SaveChangesAsync();
            topic = Utils.TopicWithPostsById(topicId, db);
            return View("Posts", topic);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditPost(int postId, string body, int topicId)
        {
            Post post = db.Posts.FirstOrDefault(i => i.Id == postId);
            post.Body = body;
            await db.SaveChangesAsync();
            TopicWithPosts topic = Utils.TopicWithPostsById(topicId, db);
            return View("Posts", topic);
        }
    }
}
