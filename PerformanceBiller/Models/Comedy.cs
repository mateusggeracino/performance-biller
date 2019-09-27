using PerformanceBiller.Models.Base;

namespace PerformanceBiller.Models
{
    public class Comedy : Amount
    {
        public sealed override decimal AmountValue { get; set; }
        public sealed override decimal TotalAmount { get; set; }
        protected sealed override int ComparatorValue { get; set; }
        protected sealed override int AdictionalValue { get; set; }
        protected override int VolumeCredits { get; set; }
        private int AnotherValue { get; }

        public Comedy()
        {
            AmountValue = 30000;
            ComparatorValue = 20;
            AdictionalValue = 500;
            AnotherValue = 300;
            TotalAmount = 0;
        }

        public override void Calculate(Performance perf)
        {
            if (perf.Audience > ComparatorValue)
            {
                AmountValue += AdictionalValue * (perf.Audience - ComparatorValue);
            }

            AmountValue += AnotherValue * perf.Audience;
        }

        public void AddExtraVolumeCredits(Performance perf) => VolumeCredits += perf.Audience / 5;
    }
}