namespace PerformanceBiller.Models.Base
{
    public interface IAmount
    {
        void Calculate(Performance perf);
    }
}