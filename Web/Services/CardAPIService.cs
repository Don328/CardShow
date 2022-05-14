using CardShow.Shared.Constants.API;
using CardShow.Shared.Models;
using Newtonsoft.Json;

namespace CardShow.Web.Services
{
    internal static class CardAPIService
    {
        internal static async Task<IEnumerable<Card>> GetBySet(int setId)
        {
            IEnumerable<Card> cards;

            var url = UrlStrings.baseUrl + UrlStrings.cards + $"/{setId}";
            var client = new HttpClient();
            using var response =  await client.GetAsync(url);

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
                    cards = serializer
                        .Deserialize<IEnumerable<Card>>(jsonReader);

                    return await Task.FromResult(cards);
                }
                catch
                {
                    Console.WriteLine("Invalid Json");
                    return new List<Card>();
                }
            }
            else
            {
                return new List<Card>();
            }
        }
    }
}
