﻿using CardShow.Shared.Constants.API;
using CardShow.Shared.Models;
using CardShow.Shared.APIServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json;

namespace CardShow.Web.Components
{
    public partial class CardSets : ComponentBase
    {
        [Parameter]
        public IEnumerable<CardSet> Sets { get; set; }
            = new List<CardSet>();

        [Parameter]
        public IEnumerable<Card> Cards { get; set; }
            = new List<Card>();

        [Parameter]
        public CardSet SelectedSet { get; set; } = new();

        private bool showAddSet = false;
        private bool hideDeleteSet = true;

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
            hideDeleteSet = false;
            SelectedSet = Sets.Where(s =>
                s.Id == id).First();
            await GetCards();
        }

        private void ShowAddSetForm()
        {
            SelectedSet = new();
            showAddSet = true;
            hideDeleteSet = true;
        }

        private async void AddSet(CardSet set)
        {
            using var response = await CardSetsAPIService.Add(set);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var id = Int32.Parse(content);
                Sets = await CardSetsAPIService.GetAll();
                showAddSet = false;
                hideDeleteSet = false;
                SelectedSet = Sets.Where(s =>
                    s.Id == id).First();
                StateHasChanged();
            }
        }

        private async Task DeleteSet()
        {
            var id = SelectedSet.Id;
            using var response = await CardSetsAPIService.Delete(id);

            if (response.IsSuccessStatusCode)
            {
                Sets = await CardSetsAPIService.GetAll();
                SelectedSet = new();
                StateHasChanged();
            }
        }

        private async Task GetCards()
        {
            Cards = new List<Card>();
            var cards = await CardsAPIService.GetBySet(SelectedSet.Id);
            Cards = cards;
            await Task.CompletedTask;
        }


        private async Task CreateNewCard(Card card)
        {
            card.SetId = SelectedSet.Id;
            await CardsAPIService.Add(card);
            await RefreshCardsList();
        }

        private async Task DeleteCard(int id)
        {
            await CardsAPIService.Delete(id);
            await RefreshCardsList();
        }

        private async Task RefreshCardsList()
        {
            Cards = new List<Card>();
            await GetCards();
            showAddCard = false;
            StateHasChanged();
            await Task.CompletedTask;
        }
    }
}
