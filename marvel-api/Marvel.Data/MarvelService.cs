using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using Flurl;
using Flurl.Http;
using Marvel.Core.Domains;
using Marvel.Infra.DTOs;
using Marvel.Core.Exceptions;
using Marvel.Core.Options;

namespace Marvel.Infra
{
    public class MarvelService : IMarvelService
    {
        private readonly MarvelApiOptions _options;
        private const string routeGetCharacters = "characters";

        public MarvelService(IOptions<MarvelApiOptions> options)
        {
            _options = options.Value;
        }


        public async Task<Data> GetCharacters(int skip, int take, bool orderByName, string? nameStartsWith)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var ts = DateTime.Now.Ticks.ToString();
                    var hash = GetHash(ts);
                    var uri = new Uri(_options.BaseUrl + routeGetCharacters);

                    var httpRequest = new HttpRequestMessage()
                    {
                        Method = HttpMethod.Get,
                        RequestUri = uri
                    };

                    if (take <= 0)
                        take = 1;

                    var filters = $"?ts={ts}&apikey={_options.PublicKey}&hash={hash}&limit={take}&offset={skip}";

                    if (!string.IsNullOrEmpty(nameStartsWith))
                        filters += $"&nameStartsWith={nameStartsWith}";

                    if (orderByName)
                        filters += "&orderBy=name";
                    else
                        filters += "&orderBy=-name";

                    var response = await client.GetAsync(uri + filters);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonContent = response.Content.ReadAsStringAsync().Result;
                        var characters = JsonConvert.DeserializeObject<MarvelResponse>(jsonContent);

                        if (characters == null)
                            return new Data();

                        return characters.Data;
                    }

                    return new Data();

                }
                catch (Exception ex)
                {
                    throw new InfraException(ex.Message, ex);
                }
            }
        }

        private string GetHash(string ts)
        {
            var input = ts + _options.PrivateKey + _options.PublicKey;
            var inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes;

            using (MD5 md5 = MD5.Create())
            {
                hashBytes = md5.ComputeHash(inputBytes);

                if (hashBytes == null)
                    throw new Exception("Erro ao gerar o hash da API da MARVEL");
            }

            return BitConverter.ToString(hashBytes)
                .ToLower().Replace("-", String.Empty); ;
        }

        public async Task<Character> GetCharacter(int idCharacter)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var ts = DateTime.Now.Ticks.ToString();
                    var hash = GetHash(ts);
                    var uri = new Uri(_options.BaseUrl + routeGetCharacters);

                    var httpRequest = new HttpRequestMessage()
                    {
                        Method = HttpMethod.Get,
                        RequestUri = uri
                    };

                    var filters = $"/{idCharacter}?ts={ts}&apikey={_options.PublicKey}&hash={hash}";

                    var response = await client.GetAsync(uri + filters);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonContent = response.Content.ReadAsStringAsync().Result;
                        var marvelResponse = JsonConvert.DeserializeObject<MarvelResponse>(jsonContent);

                        if (marvelResponse == null)
                            return new Character();

                        return marvelResponse.Data.Results.First();
                    }

                    return new Character();

                }
                catch (Exception ex)
                {
                    throw new InfraException(ex.Message, ex);
                }
            }

        }
    }
}
