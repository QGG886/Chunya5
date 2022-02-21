using System;
using System.ComponentModel.DataAnnotations;

namespace Chunya5.Models
{
    public class Bonds
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "债券编码不能为空")]
        [Display(Name = "债权编码")]
        public string BondsCode { get; set; } = "";

        [Required(ErrorMessage = "债权名称不能为空")]
        [Display(Name = "债权名称")]
        public string BondsName { get; set; } = "";

        [Required(ErrorMessage = "市场不能为空"), Display(Name = "市场")]
        public string Market { get; set; } = "";

        [Required(ErrorMessage = "面值不能为空"), Display(Name = "面值")]
        public decimal ParValue { get; set; } = 100m;

        [Required(ErrorMessage = "固定利率不能为空")]
        [Range(-100, 100, ErrorMessage = "固定利率范围不正确")]
        [Display(Name = "固定利率")]
        public double Rate { get; set; }

        public bool IsDelete { get; set; } = false;
        

        [Required(ErrorMessage = "起息日不能为空"), Display(Name = "起息日")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "到息日不能为空"), Display(Name = "到息日")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        public DateTime EndDate { get; set; }

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

        //期限
        [Required(ErrorMessage = "期限不能为空"), Display(Name = "期限")]
        public int Term { get; set; }

        [Required(ErrorMessage = "付息频率不能为空"), Display(Name = "付息频率")]
        public FrequencyEnum Frequency { get; set; } = FrequencyEnum.年付;



    }
    public enum FrequencyEnum { 月付 = 1, 季付 = 2, 年付 = 3 }

}

