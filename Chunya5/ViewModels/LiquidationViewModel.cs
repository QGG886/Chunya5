using Chunya5.Helper;
using Chunya5.Models;

namespace Chunya5.ViewModels
{
    
    public class LiquidationViewModel
    {
        
        public string Account { get; set; }

        

        //资产持仓表
        public PageList<Positions> Positions { get; set; }
        //买卖盈亏
        public decimal TradeProfit { get; set; }
        //利息盈亏
        public decimal InterestProfit { get; set; }
        //浮动盈亏
        public decimal FloatProfit { get; set; }
        //现金流
        public List<MoneyFlow> MoneyFlows { get; set; }



    }
}
