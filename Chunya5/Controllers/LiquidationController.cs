using Chunya5.Data;
using Chunya5.Helper;
using Chunya5.Models;
using Chunya5.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Chunya5.Controllers
{
    public class LiquidationController : Controller
    {
        private readonly MyDbContext _context;

        public LiquidationController(MyDbContext myDbContext)
        {
            this._context = myDbContext ?? throw new ArgumentNullException(nameof(myDbContext));
        }
        public async Task<IActionResult> Index(string account, DateTime liDate, int page)
        {
            var pageSize = 5;
            var positions = _context.Positions.Where(x => x.IsDelete == false);
            if (page == 0) page = 1;
            positions.OrderBy(x => x.TradeDate);

            var moneyFlows = new List<MoneyFlow>();
            
            var model = new LiquidationViewModel()
            {
                Account = account,
                Positions = await PageList<Positions>.CreatPageListAsync(positions, page, pageSize),
                TradeProfit = 0,
                InterestProfit = 0,
                FloatProfit = 0,
                MoneyFlows = moneyFlows
            };


            return View(model);
        }


    }
}
