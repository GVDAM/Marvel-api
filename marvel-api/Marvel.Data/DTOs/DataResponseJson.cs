using Marvel.Core.Domains;

namespace Marvel.Infra.DTOs
{
    public class DataResponseJson
    {
        public int Total { get; set; }
        public int Count { get; set; }
        public int Limit { get; set; }
        public int OffSet { get; set; }
        public List<Character> Results { get; set; }
    }
}
