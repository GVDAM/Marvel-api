namespace Marvel.Core.Options
{
    public class MarvelApiOptions
    {
        public const string MarvelApi = "MarvelApi";

        public string PublicKey { get; set; } = string.Empty;
        public string PrivateKey { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
    }
}
