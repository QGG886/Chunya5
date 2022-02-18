using Chunya5.Data;
using Chunya5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chunya5.Controllers
{
    public class BoodsController : Controller
    {
        private readonly MyDbContext myDbContext;

        public BoodsController(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext ?? throw new ArgumentNullException(nameof(myDbContext));
        }
        // GET: BoodsController
        public ActionResult Index()
        {
            myDbContext.Bonds.Add(new Bonds()
            {
                BondsCode = "hhh",
                BondsName="123",
                CNBD="china",
                ParValue= 100m,
                Rate = 0.01,
                IsDel= false

            });
            myDbContext.SaveChanges();
            return View();
        }

        // GET: BoodsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BoodsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BoodsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BoodsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BoodsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BoodsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BoodsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
