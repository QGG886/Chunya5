using System;
using System.ComponentModel.DataAnnotations;

namespace Chunya5.Models
{
    public class Bonds
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "债权编码")]
        public string BondsCode { get; set; } = "";

        [Required]
        [Display(Name = "债权名称")]
        public string BondsName { get; set; } = "";

        [Required, Display(Name = "市场")]
        public string Market { get; set; } = "";

        [Required, Display(Name = "面值")]
        public decimal ParValue { get; set; } = 100m;

        [Required, Display(Name = "固定利率")]
        public double Rate { get; set; }

        public bool IsDelete{ get; set; } = false;

        [Required, Display(Name = "起息日")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        public DateTime StartDate { get; set; }

        [Required, Display(Name = "到息日")]
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
        [Required, Display(Name = "期限")]
        public int Term { get; set; }

        [Required, Display(Name = "付息频率")]
        public FrequencyEnum Frequency { get; set; } = FrequencyEnum.nianfu;



    }
    public enum FrequencyEnum { yufu = 1, jifu = 2, nianfu = 3 }

}

