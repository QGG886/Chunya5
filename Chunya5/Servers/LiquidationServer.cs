using Chunya5.Data;
using Chunya5.Models;

namespace Chunya5.Servers
{
    public class LiquidationServer
    {
        private readonly MyDbContext context;

        public LiquidationServer(MyDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
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

    }
}
