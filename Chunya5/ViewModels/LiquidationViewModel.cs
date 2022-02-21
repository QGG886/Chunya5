using Chunya5.Helper;
using Chunya5.Models;

namespace Chunya5.ViewModels
{
    
    public class LiquidationViewModel
    {
        
        public string Account { get; set; }

        public LiquidationViewModel(string account,
            decimal tradeProfit, decimal interestProfit,
            decimal floatProfit, List<MoneyFlow> moneyFlows, PageList<Positions> positions)
        {
            Account = account ?? throw new ArgumentNullException(nameof(account));
            TradeProfit = tradeProfit;
            InterestProfit = interestProfit;
            FloatProfit = floatProfit;
            MoneyFlows = moneyFlows ?? throw new ArgumentNullException(nameof(moneyFlows));
            Positions = positions ?? throw new ArgumentNullException(nameof(positions));
        }

        //资产持仓表
        public PageList<Positions> Positions { get; set; }
        //买卖盈亏
        public decimal TradeProfit { get; set; }
        //利息盈亏
        public decimal InterestProfit { get; set; }
        //浮动盈亏
        public decimal FloatProfit { get; set; }
        public List<MoneyFlow> MoneyFlows { get; set; }



    }
}
