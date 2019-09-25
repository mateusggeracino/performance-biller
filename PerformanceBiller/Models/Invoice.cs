using System.Collections.Generic;

namespace PerformanceBiller.Models
{
    public class Invoice
    {
        public string Customer { get; set; }
        public ICollection<Performance> Performances { get; set; }
    }

    public class Performance
    {
        public string PlayId { get; set; }
        public int Audience { get; set; }
    }
}