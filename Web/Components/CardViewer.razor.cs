using CardShow.Shared.Interfaces;
using CardShow.Shared.Models;
using CardShow.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components
{
    public partial class CardViewer: ComponentBase
    {
        private enum ViewMode
        {
            SetList,
            ShowSet,
            AddSet,
        }

        [Inject]
        public ILogger<CardViewer> Logger { get; set; }

        [Inject]
        public IAPIService<CardSet> Api { get; set; }

        private ViewMode viewMode;

        private IEnumerable<CardSet> sets
            = Enumerable.Empty<CardSet>();

        private CardSet selectedSet = new();
        
        protected async override Task OnParametersSetAsync()
        {
            sets = await Api.Get();
            viewMode = ViewMode.SetList;
        }

        private async Task ShowSet(int id)
        {
            Logger.LogInformation("Getting Cards for selected set");
            selectedSet = sets.Where(s =>
                s.Id == id).First();
            viewMode = ViewMode.ShowSet;
        }

        private void ShowSetsList() => 
            viewMode = ViewMode.SetList;

        private void ShowAddSetForm() =>
            viewMode = ViewMode.AddSet;



        private async Task CreateSet(CardSet set)
        {
            Logger.LogInformation($"Sending Request to add a new Set ({set.Year} {set.Name})");
            using var response = await Api.Add(set);
            Logger.LogInformation($"Request response status code: {response.StatusCode}");
            if (response.IsSuccessStatusCode)
            {
                Logger.LogInformation("CardSet was created");
                var content = await response.Content.ReadAsStringAsync();
                var id = Int32.Parse(content);
                sets = await Api.Get();
                selectedSet = sets.Where(s =>
                    s.Id == id).First();
            }
            
            ShowSetsList();
        }

        private async Task DeleteSet(int id)
        {
            Logger.LogInformation($"Sending Request to delete set (id:{id})");
            using var response = await Api.Delete(id);
            Logger.LogInformation($"Request response status code: {response.StatusCode}");
            if (response.IsSuccessStatusCode)
            {
                Logger.LogInformation("Set was deleted");
                sets = await Api.Get();
                selectedSet = new();
                ShowSetsList();
            }
        }
    }
}
