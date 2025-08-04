using Insecure_Bank.Models;
using Insecure_Bank.Models.DataContext;
using Insecure_Bank.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Insecure_Bank.Controllers
{
    public class HomeController : Controller
    {

        private readonly BankaDbContext _context;

        public HomeController(BankaDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        public IActionResult DashBoard()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var userId = HttpContext.Session.GetInt32("UserId");
            var transactions = _context.Transactions
                                .Where(t => t.UserId == userId)
                                .ToList();


            var model = new DashBoardViewModel
            {
                Transactions = transactions
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
