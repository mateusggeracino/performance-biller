using PerformanceBiller.Models.Base;

namespace PerformanceBiller.Models
{
    public class Tragedy : Amount
    {
        public sealed override decimal AmountValue { get; set; }
        public sealed override decimal TotalAmount { get; set; }
        protected sealed override int ComparatorValue { get; set; }
        protected sealed override int AdictionalValue { get; set; }
        protected override int VolumeCredits { get; set; }

        public Tragedy()
        {
            AmountValue = 40000;
            ComparatorValue = 30;
            AdictionalValue = 1000;
            TotalAmount = 0;
        }

        public override void Calculate(Performance perf)
        {
            if (perf.Audience > ComparatorValue)
            {
                AmountValue += AdictionalValue * (perf.Audience - ComparatorValue);
            }
        }
    }
}