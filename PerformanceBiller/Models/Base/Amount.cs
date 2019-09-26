namespace PerformanceBiller.Models.Base
{
    public abstract class Amount : IAmount
    {
        public abstract decimal AmountValue { get; set; }
        public abstract decimal TotalAmount { get; set; }
        protected abstract int ComparatorValue { get; set; }
        protected abstract int AdictionalValue { get; set; }

        public abstract void Calculate(Performance perf);
    }
}