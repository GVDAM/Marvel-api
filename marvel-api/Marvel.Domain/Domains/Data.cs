namespace Marvel.Core.Domains
{
    public class Data
    {
        public Data()
        {
            Results = new();
        }

        public int Total { get; set; }
        public int Count { get; set; }
        public int Limit { get; set; }
        public int OffSet { get; set; }
        public int CurrentPage { get; set; }
        public List<Character> Results { get; set; }
    }
}
