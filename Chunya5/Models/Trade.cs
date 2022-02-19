using System.ComponentModel.DataAnnotations;

namespace Chunya5.Models
{
    public class Trade
    {
        public int Id { get; set; }

        [Required,Display(Name ="交易账户")]
        public string Account { get; set; } = "";

        [Required, Display(Name = "债权代码")]
        public int BondsCode { get; set; }
        //public Bonds Bonds { get; set; }

        [Required, Display(Name = "交易时间")]
        public DateTime TradeDate { get; set; }

        //交易方向（买或卖）
        [Required, Display(Name = "交易方式")]
        public string Direction { get; set; } = "";
        //交易面额
        [Required, Display(Name = "交易面额")]
        public decimal Deno { get; set; }
        //净价
        [Required, Display(Name = "净价")]
        public decimal NetPrace { get; set; }
        //应计利息
        [Required, Display(Name = "应计利息")]
        public decimal Accrued { get; set; }
        //结算金额
        [Required, Display(Name = "结算金额")]
        public decimal Amount { get; set; }


    }
}
