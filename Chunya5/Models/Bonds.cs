using System;

namespace Chunya5.Models
{
    public class Bonds
    {
        public int Id { get; set; }
        public string BondsCode { get; set; } = "";
        public string BondsName { get; set; } = "";
        public string Market { get; set; } = "";
        public decimal ParValue { get; set; }
        public double Rate { get; set; }
        public bool IsDel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime AddTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; }
        public string AddMan { get; set; } = "";
        public string ModifyMan { get; set; } = "";
        //期限
        public int Term { get; set; }
        public FrequencyEnum Frequency { get; set; } = FrequencyEnum.nianfu;
        public enum FrequencyEnum { yufu = 1, jifu = 2, nianfu = 3 }



    }
}

