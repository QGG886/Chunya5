using Chunya5.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chunya5.Controllers
{
    public class StatementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ExportReport(string account,DateTime startTime,DateTime endTime)
        {
            ViewBag.Account = account;
            ViewBag.StartTime = startTime.ToLongDateString();
            ViewBag.EndTime = endTime.ToLongDateString();


            return View(new List<Statement>());
        }


    }
}
