using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.Cards
{
    public partial class ListCards: ComponentBase
    {
        [Parameter]
        public IEnumerable<Card> Cards { get; set; }
            = new List<Card>();

        [Parameter]
        public EventCallback<int> OnDelete { get; set; }

        [Parameter]
        public EventCallback<int> OnShowCard { get; set; }

        private async Task ShowCard(int id)
        {
            await OnShowCard.InvokeAsync(id);
        }
    }
}
