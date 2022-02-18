using Chunya5.Data;
using Chunya5.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Chunya5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext myDbContext;

        public HomeController(ILogger<HomeController> logger,
            MyDbContext myDbContext)
        {
            _logger = logger;
            this.myDbContext = myDbContext ?? throw new ArgumentNullException(nameof(myDbContext));
        }

        public IActionResult Index()
        {
           
            return View();
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