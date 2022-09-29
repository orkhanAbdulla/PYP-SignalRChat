using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PYP_CustomIdentity.ViewModels;
using PYP_SignalRPrictice.DAL;
using PYP_SignalRPrictice.Models;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.Json;

namespace PYP_SignalRPrictice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ChatContext _chatContext;

        public HomeController(ILogger<HomeController> logger, ChatContext chatContext)
        {
            _logger = logger;
            _chatContext = chatContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View();

            if ((_chatContext.Users.Any(x => x.Username == registerVM.Username)))
            {
                ModelState.AddModelError("Username", "This Username alredy exist");
                return View();
            }
        
            User user = new User
            {
                Email = registerVM.Email,
                Username = registerVM.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password)
        };
            _chatContext.Users.Add(user);
            _chatContext.SaveChanges();
            return RedirectToAction(nameof(Login), "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginVM loginVM)
        {
            User user = _chatContext.Users.FirstOrDefault(x => x.Username == loginVM.Username);
            if (user == null && !BCrypt.Net.BCrypt.Verify(loginVM.Password, user.Password))
            {
                ModelState.AddModelError("", "UserName or Password is incorrect");
                return View();
            }

            if (HttpContext.Session.GetString("Identity") != null)
            {
                HttpContext.Session.Remove("Identity");
            }
            HttpContext.Session.SetString("Identity", user.Username);
            return RedirectToAction(nameof(Chat), "Home");

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Identity");
            return RedirectToAction("Chat");
        }
        public IActionResult Chat()
        {
            if (HttpContext.Session.GetString("Identity") != null)
            {
                var models = _chatContext.Users.ToList();
                return View(models);
            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Privacy()
        {
           
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    
    }
}