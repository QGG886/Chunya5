using Chunya5.Data;
using Chunya5.Models;

namespace Chunya5.Servers
{
    public class LiquidationServer
    {
        private readonly MyDbContext _context;

        public LiquidationServer(MyDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        /// <summary>
        /// search最早交易日期
        /// </summary>
        /// <param name="账户名称"></param>
        /// <param name="日期"></param>
        /// <returns></returns>
        public DateTime SearchEarliestDate(string account, DateTime date, out bool isHas)
        {
            //		select from 交易 where 账户名称 = 账户名称 , 日期 <= 日期 order by 日期 asc limit 1;
            //		if(交易.isEmpty) return null;
            //		Date date = 交易.日期;
            isHas = true;
            var trade = _context.Trade.Where(x => x.Account == account && x.TradeDate <= date)
                .OrderBy(x => x.TradeDate).First();
            if (trade == null)
            {
                isHas = false;
                return new DateTime();
            }
            return trade.TradeDate;
        }

        /// <summary>
        /// 通过账户和日搜索该账户的持仓
        /// </summary>
        /// <param name="account"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<Positions> SearchPositions(string account, DateTime date)
        {
            var positions = _context.Positions.Where(x => x.Account == account && x.TradeDate == date).ToList();
            return positions;

        }

        /// <summary>
        /// 搜索最近持仓日期
        /// </summary>
        /// <param name="account"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime SearchRecentlyOpenDate(string account, DateTime date)
        {
            //		select from 持仓 where 账户名称 = 账户名称 , 日期 <= date order by 日期 desc limit 1;
            //		if(持仓.isEmpty) return null;
            //		Date date = 持仓.日期;

            var time = _context.Positions.Where(x => x.Account == account && x.TradeDate <= date)
                .OrderByDescending(x => x.TradeDate).First().TradeDate;

            return time;
        }

        /// <summary>
        /// 计算两个日期之间的天数
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int CalculateDays(DateTime startTime, DateTime endTime)
        {

            int days = endTime.Subtract(startTime).Days;

            return days;
        }


        /// <summary>
        /// 搜索交易
        /// </summary>
        /// <param name="account"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<Trade> SearchTrade(string account, DateTime data)
        {
            var trades = _context.Trade.Where(x => x.Account == account && x.TradeDate <= data).ToList();
            return trades;
        }

        /// <summary>
        /// 获取债券信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Bonds GetBonds(string code)
        {
            var bonds = _context.Bonds.FirstOrDefault(x => x.BondsCode == code);
            if (bonds == null)
            {
                return null;

            }
            return bonds;
        }

        /// <summary>
        /// 获取交易表所有的账户
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllAccount()
        {
            List<string> accounts = new List<string>();

            foreach (var trade in _context.Trade)
            {
                if (!accounts.Contains(trade.Account))
                {
                    accounts.Add(trade.Account);
                }
            }
            return accounts;
        }

        /// <summary>
        /// 计算上一个付息日
        /// </summary>
        /// <param name="startDate">起息日</param>
        /// <param name="endDate">到期日</param>
        /// <param name="calculateDate">交易日</param>
        /// <returns>上一个付息日</returns>
        public DateTime CalculateLastInterestDate(DateTime startDate, DateTime endDate, DateTime calculateDate)
        {
            DateTime LastInterestDate = new();
            if (calculateDate < startDate || calculateDate >= endDate)
            {
                calculateDate.AddDays(1);
                return calculateDate;
            }
            else if (calculateDate >= startDate && calculateDate < new DateTime(startDate.Year + 1, 1, 1))
            {
                LastInterestDate = startDate;
            }
            else
            {
                LastInterestDate = new DateTime(calculateDate.Year, 1, 1);
            }
            return LastInterestDate;
        }

        /// <summary>
        /// 查询该账户在某一天的所有交易记录，并根据债券编码进行合并操作
        /// </summary>
        /// <param name="trades">某账户在某一天的所有交易记录</param>
        /// <returns>合并后的交易记录</returns>
        public List<Trade> MergeTrade(List<Trade> trades)
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
        /// 计算持仓
        /// </summary>
        /// <param name="account">账户名称</param>
        /// <param name="date">日期</param>
        public void CalulatePosition(String account, DateTime date)
        {
            //List<交易> 全部交易 = search交易(账户名称, 计算日);
            List<Trade> allTrades = _context.Trade.Where(x => x.Account == account && x.TradeDate == date && x.IsDelete == false && x.IsDelete == false).ToList();

            //List<持仓> search持仓 = search持仓(账户名称, new Date(计算日.getTime() - 1000 * 60 * 60 * 24));
            List<Positions> positions = _context.Positions.Where(x => x.Account == account && x.IsDelete == false && x.TradeDate == date.AddDays(-1)).ToList();

            List<Trade> mergeTrade = MergeTrade(allTrades);

            //获取债券数据字典
            List<string> bondsCode = new List<string>();
            foreach (var positionItem in positions)
            {
                if (!bondsCode.Contains(positionItem.BondsCode))
                {
                    bondsCode.Add(positionItem.BondsCode);
                }

            }
            foreach (Trade tradeItem in mergeTrade)
            {

                if (!bondsCode.Contains(tradeItem.BondsCode))
                {
                    bondsCode.Add(tradeItem.BondsCode);
                }

            }
            List<Bonds> allBonds = _context.Bonds.Where(x => x.IsDelete == false && bondsCode.Contains(x.BondsCode)).ToList();

            Dictionary<string, Bonds> bondsDictionary = new Dictionary<string, Bonds>();
            foreach (Bonds bondsItem in allBonds)
            {

                bondsDictionary.Add(bondsItem.BondsCode, bondsItem);
            }


            // TODO
            // 付息操作


            //		持仓表中有持仓数据，而合并后交易条目无

            //		持仓表中有持仓数据，而合并后交易条目有买方向数据

            //		持仓表中有持仓数据，而合并后交易条目有卖方向数据

        }
        /// <summary>
        /// 付息操作
        /// </summary>
        /// <param name="position">持仓</param>
        /// <param name="bondsDictionary">债券字典</param>
        /// <returns></returns>
        public List<Positions> PayInterest(List<Positions> position, Dictionary<string, Bonds> bondsDictionary)
        {
            foreach (Positions positionItem in position)
            {
                Bonds targetBonds = bondsDictionary[positionItem.BondsCode];
                if (!IsInterestDate(targetBonds.StartDate, targetBonds.EndDate, positionItem.TradeDate.AddDays(1)))
                {
                    continue;
                }
                positionItem.RealizedInterestIncome = positionItem.RealizedInterestIncome + positionItem.AccInterest - positionItem.InterestCost;
                positionItem.InterestCost = 0;
                positionItem.AccInterest = 0;
                positionItem.AccUninterestImcome = 0;
                positionItem.TotalInterestIncome = positionItem.AccUninterestImcome + positionItem.RealizedInterestIncome;
            }
            return position;
        }

        /// <summary>
        /// 判断是否是付息日
        /// </summary>
        /// <param name="startDate">起息日</param>
        /// <param name="endDate">到息日</param>
        /// <param name="calculateDate">计算日</param>
        /// <returns></returns>
        public Boolean IsInterestDate(DateTime startDate, DateTime endDate, DateTime calculateDate)
        {
            if (calculateDate < startDate || calculateDate > endDate)
            {
                return false;
            }
            else if (calculateDate >= startDate && calculateDate == new DateTime(startDate.Year + 1, 1, 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 计算第一条持仓日期
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="caldate">计算日期</param>
        /// <param name="firstpositiondate">第一条持仓日期</param>
        /// <returns></returns>
        //public Boolean FirstCalculatePositions(string account, DateTime caldate, out DateTime firstpositiondate)
        //{

        //    Boolean isNull;
        //    DateTime firstdate = SearchEarliestDate(account, caldate, out isNull);
        //    firstpositiondate = firstdate;

        //    if (isNull)
        //    {
        //        return false;
        //    }







        //}
    }
}
