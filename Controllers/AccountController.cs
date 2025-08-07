using Insecure_Bank.Models.DataContext;
using Insecure_Bank.Models.Model;
using Insecure_Bank.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Insecure_Bank.Controllers
{
    public class AccountController : Controller
    {
        private readonly BankaDbContext _context;
        public AccountController(BankaDbContext context)
        {
            _context = context;
        }

        //GET LOGİN
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        //POST LOGİN
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); //Formu tekrar kullanıcıya geri gösterir, girilmiş olan verileri ve hata mesajlarını sayfada tekrar gösterir.
            }
            //  SQL INJECTION ZAFİYETİ!
            // Bu kod doğrudan string concatenation kullanıyor
            //var user = _context.Users.FromSqlRaw(
               // $"SELECT * FROM Users WHERE Username='{model.Username}' AND Password = '{model.Password}'").FirstOrDefault();
            //*****ZAAFİYET DÜZELTİLDİ*******
            var user=_context.Users.FirstOrDefault(u=>u.Username==model.Username && u.Password==model.Password);
            if (user != null)
            {
                // Session'a kullanıcı bilgilerini kaydet (güvensiz)
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetInt32("UserId", user.UserId);

                return RedirectToAction("DashBoard", "Home");
            }
            ModelState.AddModelError("", "Invalid username or password.");
            return View(model);
            
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }

        //GET REGİSTER
        [HttpGet]
        public IActionResult Register()
        {
           
            return View();
        }
        //POST REGİSTER
        [HttpPost]
        public IActionResult Register( RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Kullanıcı adı daha önce alınmış mı kontrolü 
            var existUser = _context.Users.FirstOrDefault(u => u.Username == model.Username);
            if (existUser != null)
            {
                ModelState.AddModelError("Username", "Bu kullacını zaten var.");
                return View(model);
            }
            //Yeni kullanıcı ekleme
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var account = new Models.Model.Account {
                UserId=user.UserId,
                AccountNumber= (1000+user.UserId).ToString(),
                Balance=100.00m ,//İlk giriş için bonus
                 AccountType = "Vadesiz",
                

            };
            _context.Accounts.Add(account); 
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

      

        //GET PROFİL GÜNCELLE
        [HttpGet]
        public IActionResult ProfilEdit()
        {
            var username = HttpContext.Session.GetString("Username");
            if(username == null) 
                return RedirectToAction("Login");

            var user=_context.Users.FirstOrDefault(u=>u.Username == username);
            if (user == null)
                return RedirectToAction("Login");

            var model = new ProfilViewModel
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Bio = user.Bio

            };
            return View(model);
        }

        //POST PROFİL GÜNCELLE
        [HttpPost]
        public IActionResult ProfilEdit(ProfilViewModel model)
        {
            var username = HttpContext.Session.GetString("Username");
            if (username == null)
                return RedirectToAction("Login");

            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return RedirectToAction("Login");

            // Profil güncelleme - XSS için Bio alanını temizlemiyoruz!
            user.Email = model.Email;
            user.Bio = model.Bio;
            _context.SaveChanges();

            return View(model);
        }



















    }
}

