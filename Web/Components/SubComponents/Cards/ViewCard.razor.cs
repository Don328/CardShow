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

        [Parameter]
        public EventCallback<int> DeleteAssessment { get; set; }

        private bool deleteDisabled = true;

        private const string deleteWarning =
            "Deleting card will delete attached assessments";

        private void ToggleDeleteEnabled()
        {
            deleteDisabled = !deleteDisabled;
        }

        public async Task Delete()
        {
            await DeleteCard.InvokeAsync(Card.Id);
        }
    }
}
