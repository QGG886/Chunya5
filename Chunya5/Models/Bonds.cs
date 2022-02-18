namespace Chunya5.Models
{
    public class Bonds
    {
        public int Id { get; set; }
        public string BondsCode { get; set; }
        public string BondsName { get; set; }
        public string CNBD { get; set; }
        public decimal ParValue { get; set; }
        public double Rate { get; set; }
        public bool IsDel { get; set; }

    }
}
