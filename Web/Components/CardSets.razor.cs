using CardShow.Shared.Constants.API;
using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json;

namespace CardShow.Web.Components
{
    public partial class CardSets : ComponentBase
    {
        private bool showAddSet = false;

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

            using var client = new HttpClient();
            using var response = await client.GetAsync(url);
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

        private void ViewSet(int id)
        {
            showAddSet = false;
            SelectedSet = Sets.Where(s =>
                s.Id == id).First();
        }

        private void ShowAddSetForm()
        {
            SelectedSet = new();
            showAddSet = true;
        }

        private async void AddSet(CardSet set)
        {

            var url = UrlStrings.baseUrl + UrlStrings.sets;
            using var client = new HttpClient();
            using var response = await client.PostAsJsonAsync<CardSet>(url, set);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var id = Int32.Parse(content);
                await GetCardSets();
                showAddSet = false;
                SelectedSet = Sets.Where(s =>
                    s.Id == id).First();
                StateHasChanged();
            }
        }

        private async Task DeleteSet()
        {
            var id = SelectedSet.Id;
            var url = $"{UrlStrings.baseUrl}" +
                $"{UrlStrings.sets}/delete";

            using var client = new HttpClient();
            using var response = await client.PostAsJsonAsync<int>(url, id);

            if (response.IsSuccessStatusCode)
            {
                await GetCardSets();
                SelectedSet = new();
                StateHasChanged();
            }
        }
    }
}
