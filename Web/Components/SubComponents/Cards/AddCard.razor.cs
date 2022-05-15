using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.Cards
{
    public partial class AddCard : ComponentBase
    {
        [Parameter]
        public Card NewCard { get; set; }
            = new();

        [Parameter]
        public EventCallback<Card> CreateCard { get; set; }

        private async Task OnCreate()
        {
            await CreateCard.InvokeAsync(NewCard);
        }
    }
}
