using Chunya5.Data;
using Chunya5.Helper;
using Chunya5.Models;
using Chunya5.ViewModels;
using Chunya5.Servers;
using Microsoft.AspNetCore.Mvc;

namespace Chunya5.Controllers
{
    public class LiquidationController : Controller
    {
        private readonly MyDbContext _context;
        private readonly LiquidationServer liquidationServer;

        public LiquidationController(MyDbContext myDbContext, LiquidationServer liquidationServer)
        {
            this._context = myDbContext ?? throw new ArgumentNullException(nameof(myDbContext));
            this.liquidationServer = liquidationServer ?? throw new ArgumentNullException(nameof(liquidationServer));
        }
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Calculate(string account, DateTime liDate, int page)
        {
            //int pageSize = 5;
            //positions
            Boolean dateAvailable;

            DateTime recentlyOpenDate = liquidationServer.SearchRecentlyOpenDate(account, liDate,out dateAvailable);
            
            ViewBag.liDate = liDate.ToLongDateString();
            Boolean isCal = true;
            if (dateAvailable == false)
            {
                //未查询到持仓记录
                isCal= liquidationServer.FirstCalculatePositions(account, liDate,out recentlyOpenDate);
                ViewBag.firstCalDate = recentlyOpenDate.ToLongDateString();
            }
            else
            {
                ViewBag.firstCalDate = recentlyOpenDate.ToLongDateString();
            }
            if (isCal)
            {
                while (liquidationServer.CalculateDays(recentlyOpenDate, liDate) < 0)
                {
                    recentlyOpenDate = recentlyOpenDate.AddDays(1);
                    liquidationServer.CalulatePosition(account, recentlyOpenDate);

                }
            }
            
            List<Positions> positions = _context.Positions.Where(x => x.IsDelete == false &&x.Account == account &&x.TradeDate ==liDate).ToList();



            var moneyFlows = _context.MoneyFlows.ToList();
            ViewBag.IsCal = isCal;
            var model = new LiquidationViewModel()
            {
                Account = account,
                Positions= positions,
                
                //Positions = await PageList<Positions>.CreatPageListAsync(positions, page, pageSize),
                TradeProfit = 0,
                InterestProfit = 0,
                FloatProfit = 0,
                MoneyFlows = moneyFlows
            };
            return View(model);
        }

        /// <summary>
        /// 查询该账户在某一天的所有交易记录，并根据债券编码进行合并操作
        /// </summary>
        /// <param name="trades">某账户在某一天的所有交易记录</param>
        /// <returns>合并后的交易记录</returns>
        private List<Trade> MergeTrade(List<Trade> trades)
        {
            List<string> allCode = new List<string>();
            foreach (var trade in trades)
            {
                if (!allCode.Contains(trade.BondsCode))
                    allCode.Add(trade.BondsCode);
            }

            List<Trade> mergeTrades = new List<Trade>();
            #region 合并当前日期下债卷编码相同的债卷
            foreach (var code in allCode)
            {
                var trade = new Trade();
                trade.BondsCode = code;
                var sameCodeTrades = trades.Where(x => x.BondsCode == code).ToList();

                string direction;//交易方式
                decimal deno = 0;    //交易面额
                decimal netPrace = 0;//净价
                decimal accrued = 0;//应计利息
                decimal amount = 0; //结算金额

                foreach (var item in sameCodeTrades)
                {
                    if (item.Direction == "买入")
                    {
                        deno += item.Deno;
                        netPrace += item.NetPrace * item.Deno * 100;
                        accrued += item.Accrued;
                        amount += item.Amount;

                    }
                    else
                    {
                        deno -= item.Deno;
                        netPrace -= item.NetPrace * item.Deno * 100;
                        accrued -= item.Accrued;
                        amount -= item.Amount;
                    }

                }
                if (deno >= 0)
                {
                    direction = "买入";
                }
                else
                    direction = "卖出";

                netPrace = netPrace / deno > 0 ? netPrace / deno : netPrace / deno * -1;

                trade.Direction = direction;
                trade.Deno = deno;
                trade.NetPrace = netPrace;
                trade.Accrued = accrued;
                trade.Amount = amount;
                trade.TradeDate = trades[0].TradeDate;
                mergeTrades.Add(trade);

            }

            #endregion
            return mergeTrades;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trades"></param>
        /// <returns>某账户在某天所有债卷的持仓记录</returns>
        private List<Positions> CalculatePositions(List<Trade> trades)
        {

            List<Positions> positionsList = new List<Positions>();
            List<Trade> mergeTrade =  MergeTrade(trades);

            foreach (var trade in mergeTrade)
            {
                Positions positions = new Positions();

                positions.Account = trade.Account;//账户
                positions.BondsCode = trade.BondsCode;//债券编码
                positions.TradeDate = trade.TradeDate;//交易日期
                positions.NetCost = trade.NetPrace;//净价成本
                positions.InterestCost = trade.Accrued;//利息成本

                //（计算日期-上一个付息日+1）*交易面额*年利率/365天
                //注意年利率可能存在百分号与小数的转换
                //positions.AccInterest;//当期应计利息


                //已计提未实现利息收入
                //当期应计利息 - 利息成本

                //已实现利息收入
                positions.RealizedInterestIncome = 0;

                //利息收入合计
                //已计提未实现利息收入 + 已实现利息收入
                //positions.TotalInterestIncome = 

                //买卖盈亏
                positions.TradingProloss = 0;

                //浮动盈亏


                //持有面额
                positions.DenominattonHeld = trade.Deno;




            }
            return positionsList;

        }
        



        



    }


}

