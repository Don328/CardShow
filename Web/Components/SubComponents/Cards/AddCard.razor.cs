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
        public EventCallback<Card> OnCreate { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        private async Task Create() =>
            await OnCreate.InvokeAsync(NewCard);

        private async Task Cancel() =>
            await OnCancel.InvokeAsync();
    }
}
