using System.ComponentModel.DataAnnotations;

namespace Chunya5.Models
{
    //持仓表
    public class Position
    {
        public int ID { get; set; }

        [ Display(Name = "添加时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        public DateTime AddTime { get; set; } = DateTime.Now;

        [ Display(Name = "更新时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        public DateTime UpdateTime { get; set; }

        [ Display(Name = "新增人")]
        public string AddMan { get; set; } = "";

        [ Display(Name = "修改人")]
        public string ModifyMan { get; set; } = "";

        public bool IsDeleteete { get; set; } = false;

        [Required, Display(Name = "账户")]
        public string Accout { get; set; } = "";


        [Required, Display(Name = "债券编码")]
        public string BondsCode { get; set; } = "";

        [Required, Display(Name = "交易日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        public DateTime TradeDate { get; set; }

        [Required,Display(Name = "净价成本")]
        public Decimal NetCost { get; set; }

        [Required, Display(Name = "利息成本")]
        public Decimal InterestCost { get; set; }

        [Required, Display(Name = "当期应计利息")]
        public Decimal AccInterest { get; set; }

        [Required, Display(Name = "已计提未实现利息收入")]

        public Decimal AccUninterestImcome { get; set; }

        [Required, Display(Name = "已实现利息收入")]

        public Decimal RealizedInterestIncome { get; set; }

        [Required, Display(Name = "利息收入合计")]
        public Decimal TotalInterestIncome { get; set; }

        [Required, Display(Name = "买卖盈亏")]
        public Decimal TradingProloss { get; set; }
        [Required, Display(Name = "浮动盈亏")]
        public Decimal FloatingPl { get; set; }

        [Required, Display(Name = "持有面额")]
        public Decimal DenominattonHeld { get; set; }

    }
}
