using System.Collections.Generic;
using PerformanceBiller.Models;

namespace PerformanceBiller.Repository
{
    public interface IJsonReader
    {
        T GetData<T>(string path);
        //Invoice<T> GetInvoice();
        Play GetPlayById(string playId);
    }
}