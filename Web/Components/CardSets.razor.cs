using CardShow.Shared.Constants.API;
using CardShow.Shared.Models;
using CardShow.Web.Services;
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
            Sets = await CardSetService.GetAll();
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
            using var response = await CardSetService.Add(set);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var id = Int32.Parse(content);
                Sets = await CardSetService.GetAll();
                showAddSet = false;
                SelectedSet = Sets.Where(s =>
                    s.Id == id).First();
                StateHasChanged();
            }
        }

        private async Task DeleteSet()
        {
            var id = SelectedSet.Id;
            using var response = await CardSetService.Delete(id);


            if (response.IsSuccessStatusCode)
            {
                Sets = await CardSetService.GetAll();
                SelectedSet = new();
                StateHasChanged();
            }
        }
    }
}
