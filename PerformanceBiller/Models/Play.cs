namespace PerformanceBiller.Models
{
    public class Play
    {
        public string Name { get; set; }
        public PlayType Type { get; set; }
    }

    public enum PlayType
    {
        Tragedy,
        Comedy
    }
}