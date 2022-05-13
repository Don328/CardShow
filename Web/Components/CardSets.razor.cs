using CardShow.Shared.Constants.API;
using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json;

namespace CardShow.Web.Components
{
    public partial class CardSets : ComponentBase
    {
        public string testMessage = string.Empty;
        
        [Parameter]
        public IEnumerable<CardSet> Sets { get; set; }
            = new List<CardSet>();

        [Parameter]
        public CardSet SelectedSet { get; set; } = new();

        protected async override Task OnParametersSetAsync()
        {
            await GetCardSets();
        }

        private async Task GetCardSets()
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
                    foreach (var set in Sets)
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

        private async Task OnDelete()
        {
            var id = SelectedSet.Id;

            // Call to delete selected Set

            testMessage = $"Deleting " +
                $"{SelectedSet.Year} " +
                $"{SelectedSet.Name} " +
                $"[Id: {SelectedSet.Id}]";
        }

        private void ViewSet(MouseEventArgs e, int id)
        {
            SelectedSet = Sets.Where(s =>
                s.Id == id).First();
        }
    }
}
