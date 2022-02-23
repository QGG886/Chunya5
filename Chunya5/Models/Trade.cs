using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Chunya5.Models
{
    public class Trade
    {
        public int Id { get; set; }

        public bool IsDelete { get; set; } = false;

        [Required(ErrorMessage ="交易账户不能为空"),Display(Name ="交易账户")]
        public string Account { get; set; } = "";

        [Required(ErrorMessage = "债券代码不能为空"), Display(Name = "债权编码")]
        
        public string BondsCode { get; set; } = "";
        //public Bonds Bonds { get; set; }

        [Required(ErrorMessage = "交易时间不能为空"), Display(Name = "交易时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]

        public DateTime TradeDate { get; set; }

        //交易方向（买或卖）
        [Required, Display(Name = "交易方式")]
        public string Direction { get; set; } = "";
        //交易面额
        [Required(ErrorMessage = "交易面额不能为空"), Display(Name = "交易面额")]
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

        [Display(Name = "添加时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        public DateTime AddTime { get; set; } = DateTime.Now;

        [Display(Name = "更新时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        public DateTime UpdateTime { get; set; }

        [Display(Name = "新增人")]
        public string AddMan { get; set; } = "";

        [Display(Name = "修改人")]
        public string ModifyMan { get; set; } = "";
    }
}
