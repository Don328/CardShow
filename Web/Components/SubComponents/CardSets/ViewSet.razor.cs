using CardShow.Shared.Interfaces;
using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.CardSets;

public partial class ViewSet : ComponentBase
{
    private enum ViewMode
    {
        Default,
        CardDetails,
        AddCard,
        DeleteSet
    }

    [Inject]
    public ILogger<ViewSet> Logger { get; set; }

    [Inject]
    public IAPIService<Card> Api { get; set; }

    [Parameter]
    public CardSet SelectedSet { get; set; } = new();

    [Parameter]
    public EventCallback<int> OnDelete { get; set; }
    private async Task Delete() =>
        await OnDelete.InvokeAsync(SelectedSet.Id);

    private ViewMode viewMode = ViewMode.Default;
    private IEnumerable<Card> cards = Enumerable.Empty<Card>();
    private Card selectedCard = new();

    protected async override Task OnParametersSetAsync()
    {
        cards = await GetCards(SelectedSet.Id);
    }

    private void EnableDelete() =>
        viewMode = ViewMode.DeleteSet;

    private void ShowNewCardForm() =>
        viewMode = ViewMode.AddCard;
    
    private void ShowSet() =>
        viewMode = ViewMode.Default;

    private async Task<IEnumerable<Card>> GetCards(int setId)
    {
        Logger.LogInformation($"Requesting Cards for Selected Set (id:{setId})");
        var cardList = await Api.Get(setId);
        return await Task.FromResult(cardList);
    }

    private void ShowCard(int id)
    {
        selectedCard = (from c in cards
                        where c.Id == id
                        select c).First();

        viewMode = ViewMode.CardDetails;
    }

    private async Task CreateCard(Card card)
    {
        card.SetId = SelectedSet.Id;
        Logger.LogInformation($"Requesting to Create new Card for {card.Name}");
        await Api.Add(card);
        await GetCards(SelectedSet.Id);
        viewMode = ViewMode.Default;
    }

    private async Task DeleteCard(int id)
    {
        Logger.LogInformation($"Requesting to delete card (id:{id})");
        await Api.Delete(id);
        cards = await GetCards(SelectedSet.Id);
        viewMode= ViewMode.Default;
    }
}
