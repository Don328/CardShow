using CardShow.Shared.Models;
using CardShow.Shared.APIServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json;

namespace CardShow.Web.Components
{
    public partial class CardSets : ComponentBase
    {
        [Inject]
        public ILogger<CardSets> Logger { get; set; }

        [Parameter]
        public IEnumerable<CardSet> Sets { get; set; }
            = new List<CardSet>();

        [Parameter]
        public IEnumerable<Card> Cards { get; set; }
            = new List<Card>();

        [Parameter]
        public CardSet SelectedSet { get; set; } = new();

        private string errorMessage = string.Empty;

        private bool showAddSet = false;
        private bool hasOrphans = true;
        private bool deleteEnabled = false;

        private bool showAddCard = false;
        private void ShowNewCardForm() => showAddCard = true;
        private void HideNewCardForm() => showAddCard = false;

        
        protected async override Task OnParametersSetAsync()
        {
            Sets = await CardSetsAPIService.GetAll();
        }

        private async Task ViewSet(int id)
        {
            showAddSet = false;
            showAddCard = false;
            hasOrphans = false;
            Logger.LogInformation("Getting Cards for selected set");
            SelectedSet = Sets.Where(s =>
                s.Id == id).First();
            await GetCards();
            hasOrphans = Cards.Any();
        }

        private void ShowAddSetForm()
        {
            SelectedSet = new();
            showAddCard = false;
            showAddSet = true;
            hasOrphans = true;
        }

        private async void AddSet(CardSet set)
        {
            Logger.LogInformation($"Sending Request to add a new Set ({set.Year} {set.Name})");
            using var response = await CardSetsAPIService.Add(set);
            Logger.LogInformation($"Request response status code: {response.StatusCode}");
            if (response.IsSuccessStatusCode)
            {
                Logger.LogInformation("CardSet was created");
                var content = await response.Content.ReadAsStringAsync();
                var id = Int32.Parse(content);
                Sets = await CardSetsAPIService.GetAll();
                showAddSet = false;
                hasOrphans = false;
                SelectedSet = Sets.Where(s =>
                    s.Id == id).First();
                StateHasChanged();
            }
        }

        private async Task EnableDelete()
        {
            if (hasOrphans)
            {
                deleteEnabled = false;
                await ShowErrorMessage(
                    "Set is referenced by one " +
                    "or more card. Cannot delete");
            }
            else
            {
                deleteEnabled = !deleteEnabled;
            }
        }


        private async Task DeleteSet()
        {
            var id = SelectedSet.Id;
            Logger.LogInformation($"Sending request to delete Set (id:{id})");
            using var response = await CardSetsAPIService.Delete(id);
            Logger.LogInformation($"Response status code: {response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                Logger.LogInformation("Set Deleted");
                Sets = await CardSetsAPIService.GetAll();
                SelectedSet = new();
                showAddCard = false;
                deleteEnabled = false;
                StateHasChanged();
            }
        }

        private async Task GetCards()
        {
            var id = SelectedSet.Id;
            Logger.LogInformation($"Requesting Cards for Selected Set (id:{id})");
            Cards = new List<Card>();
            var cards = await CardsAPIService.GetBySet(id);
            Cards = cards;
            await Task.CompletedTask;
        }


        private async Task CreateNewCard(Card card)
        {
            card.SetId = SelectedSet.Id;
            Logger.LogInformation($"Requesting to Create new Card for {card.Name}");
            await CardsAPIService.Add(card);
            await RefreshCardsList();
        }

        private async Task DeleteCard(int id)
        {
            Logger.LogInformation($"Requesting to delete card (id:{id})");
            await CardsAPIService.Delete(id);
            await RefreshCardsList();
            hasOrphans = Cards.Any();
        }

        private async Task RefreshCardsList()
        {
            Cards = new List<Card>();
            await GetCards();
            showAddCard = false;
            StateHasChanged();
            await Task.CompletedTask;
        }

        private async Task CreateAssessment(Assessment assessment)
        {
            Logger.LogInformation($"Requesting to Create new Assessment for Card (Id: {assessment.CardId}");
            await AssessmentAPIService.Add(assessment);
            await RefreshCardsList();
        }

        private async Task ShowErrorMessage(string text)
        {
            errorMessage = text;
            await Task.Delay(5000);
            errorMessage = string.Empty;
            await Task.CompletedTask;
        }
    }
}
