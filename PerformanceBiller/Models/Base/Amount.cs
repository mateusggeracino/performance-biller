using System;
using System.Globalization;

namespace PerformanceBiller.Models.Base
{
    public abstract class Amount : IAmount
    {
        private CultureInfo CultureInfo { get; set; }
        public abstract decimal AmountValue { get; set; }
        public abstract decimal TotalAmount { get; set; }
        protected abstract int ComparatorValue { get; set; }
        protected abstract int AdictionalValue { get; set; }
        public int VolumeCredits { get; set; }

        protected Amount()
        {
            CultureInfo = new CultureInfo("en-US");
        }
        public abstract void Calculate(Performance perf);

        public void AddVolumeCredits(Performance perf)
        {
            VolumeCredits += Math.Max(perf.Audience - 30, 0);
        }

        public void SumAmount(decimal amount) => TotalAmount += amount;
        public string DividePorCem() => (AmountValue / 100).ToString("C", CultureInfo);
    }
}