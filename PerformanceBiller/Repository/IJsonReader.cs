using PerformanceBiller.Models;

namespace PerformanceBiller.Repository
{
    public interface IJsonReader
    {
        Invoice GetInvoice();
        Play GetPlayById(string playId);
    }
}