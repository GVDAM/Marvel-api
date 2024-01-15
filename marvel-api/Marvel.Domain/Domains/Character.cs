namespace Marvel.Core.Domains
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public Thumbnail? Thumbnail { get; set; }
        public bool IsFav { get; set; }
    }
}
