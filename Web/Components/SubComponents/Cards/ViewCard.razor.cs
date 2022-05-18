using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.Cards
{
    public partial class ViewCard : ComponentBase
    {
        [Parameter]
        public Card Card { get; set; }
            = new();

        [Parameter]
        public EventCallback<int> DeleteCard { get; set; }

        [Parameter]
        public EventCallback<Assessment> CreateAssessment { get; set; }

        private bool deleteEnabled = false;

        private void ToggleDeleteEnabled()
        {
            deleteEnabled = !deleteEnabled;
        }

        public async Task Delete()
        {
            await DeleteCard.InvokeAsync(Card.Id);
        }
    }
}
