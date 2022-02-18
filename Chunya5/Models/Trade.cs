namespace Chunya5.Models
{
    public class Trade
    {
        public int Id { get; set; }
        public string Account { get; set; } = "";
        public int BondsCode { get; set; }
        //public Bonds Bonds { get; set; }
        public DateTime TradeDate { get; set; }
        //交易方向（买或卖）
        public string Direction { get; set; } = "";
        //交易面额
        public decimal Deno { get; set; }
        //净价
        public decimal NetPrace { get; set; }
        //应计利息
        public decimal Accrued { get; set; }
        //结算金额
        public decimal Amount { get; set; }


    }
}
