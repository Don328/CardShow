using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CardShow.Shared.Constants.API;
using Newtonsoft.Json;
using CardShow.Shared.Models;

namespace CardShow.Web.Pages
{
    public class SetsModel : PageModel
    {
        public IEnumerable<CardSet> Sets { get; set; }
            = new List<CardSet>();

        public async Task OnGet()
        {
            await GetCardSetList();
        }

        private async Task GetCardSetList()
        {
            var url = UrlStrings.baseUrl +
                UrlStrings.sets;

            var client = new HttpClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode &&
                response.Content.Headers
                    .ContentType?.MediaType
                    == "application/json")
            {
                var stream = await response
                    .Content.ReadAsStreamAsync();

                using var streamReader = new StreamReader(stream);
                using var jsonReader = new JsonTextReader(streamReader);
                var serializer = new JsonSerializer();

                try
                {
                    Sets = serializer
                        .Deserialize<IEnumerable<CardSet>>(jsonReader);
                }
                catch
                {
                    Console.WriteLine("Invalid Json");
                }
                finally
                {
                    foreach(var set in Sets)
                    {
                        Console.WriteLine();
                        Console.WriteLine(set.Year);
                        Console.WriteLine(set.Name);
                        Console.WriteLine("Id: " + set.Id);
                        Console.WriteLine();
                    }

                    await Task.CompletedTask;
                }
            }
        }
    }
}
