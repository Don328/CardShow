using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.Assessments
{
    public partial class ViewAssessment: ComponentBase
    {
        [Parameter]
        public Assessment Assessment { get; set; }
            = new();

        [Parameter]
        public EventCallback<int> Delete { get; set; }

        private bool showDetails = false;
        private bool deleteEnabled = false;

        private void ToggleDetails()
        {
            showDetails = !showDetails;
        }

        private void EnableDelete()
            => deleteEnabled = !deleteEnabled;

        private async Task OnDelete()
        {
            await Delete.InvokeAsync(Assessment.Id);
            await Task.CompletedTask;
        }
    }
}
