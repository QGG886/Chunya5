using System.ComponentModel.DataAnnotations;

namespace Chunya5.Models
{
    public class MoneyFlow
    {
        public int Id { get; set; }

        [Display(Name = "债券编码")]
        public string BondsCode { get; set; }

        [Display(Name = "交易日")]
        public DateTime PayInterestsDate { get; set; }

        [Display(Name = "现金流")]
        public decimal Prace { get; set; }

    }
}
