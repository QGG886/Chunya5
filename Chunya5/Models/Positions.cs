using System.ComponentModel.DataAnnotations;

namespace Chunya5.Models
{
    //持仓表
    public class Positions
    {
        public int ID { get; set; }

        [ Display(Name = "添加时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        public DateTime AddTime { get; set; } = DateTime.Now;

        [ Display(Name = "更新时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        public DateTime UpdateTime { get; set; } = DateTime.Now;


        [Display(Name = "新增人")]
        public string AddMan { get; set; } = "";

        [ Display(Name = "修改人")]
        public string ModifyMan { get; set; } = "";

        public bool IsDelete { get; set; } = false;

        [Required, Display(Name = "账户")]
        public string Account { get; set; } = "";


        [Required, Display(Name = "债券编码")]
        public string BondsCode { get; set; } = "";

        [Required, Display(Name = "交易日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        public DateTime TradeDate { get; set; }

        [Required,Display(Name = "净价成本")]
        public decimal NetCost { get; set; }

        [Required, Display(Name = "利息成本")]
        public decimal InterestCost { get; set; }

        [Required, Display(Name = "当期应计利息")]
        public decimal AccInterest { get; set; }

        [Required, Display(Name = "已计提未实现利息收入")]

        public decimal AccUninterestImcome { get; set; }

        [Required, Display(Name = "已实现利息收入")]

        public decimal RealizedInterestIncome { get; set; }

        [Required, Display(Name = "利息收入合计")]
        public decimal TotalInterestIncome { get; set; }

        [Required, Display(Name = "买卖盈亏")]
        public decimal TradingProloss { get; set; }
        [Required, Display(Name = "浮动盈亏")]
        public decimal FloatingPl { get; set; }

        [Required, Display(Name = "持有面额")]
        public decimal DenominattonHeld { get; set; }

    }
}
