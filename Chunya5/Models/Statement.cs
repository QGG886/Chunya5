using System;
using System.ComponentModel.DataAnnotations;

namespace Chunya5.Models
{
    public class Statement
    {
        public int Id { get; set; }

        [Display(Name = "债券编码")]
        public string BondsCode { get; set; }

        [Display(Name = "浮动盈亏")]
        public decimal FloatProfitLoss { get; set; }

        [Display(Name = "持仓盈亏")]
        public decimal PositionsProfitLoss { get; set; }

        [Display(Name = "利息收入")]
        public decimal InterestIncome { get; set; }
    }   
}

