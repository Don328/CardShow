using CardShow.Shared.Constants.API;
using CardShow.Shared.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace CardShow.Shared.APIServices
{
    public static class CardSetsAPIService
    {
        public static async Task<IEnumerable<CardSet>> GetAll()
        {
            IEnumerable<CardSet> sets;
            var url = UrlStrings.baseUrl +
                UrlStrings.sets;

            using var client = new HttpClient();
            using var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode &&
                response.Content.Headers
                    .ContentType?.MediaType
                    == "application/json")
            {
                using var stream = await response
                    .Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(stream);
                using var jsonReader = new JsonTextReader(streamReader);
                var serializer = new JsonSerializer();

                try
                {
                    sets = serializer
                        .Deserialize<IEnumerable<CardSet>>(jsonReader);

                    return await Task.FromResult(sets);
                }
                catch
                {
                    Console.WriteLine("Invalid Json");
                    return new List<CardSet>();
                }
            }
            else
            {
                return new List<CardSet>();
            }
        }

        public static async Task<HttpResponseMessage> Add(CardSet set)
        {
            var url = UrlStrings.baseUrl + UrlStrings.sets;
            using var client = new HttpClient();
            return await client
                .PostAsJsonAsync(url, set);
        }

        public static async Task<HttpResponseMessage> Delete(int id)
        {
            var url = $"{UrlStrings.baseUrl}" +
                    $"{UrlStrings.sets}/delete";

            using var client = new HttpClient();
            return await client
                .PostAsJsonAsync(url, id);
        }
    }
}
