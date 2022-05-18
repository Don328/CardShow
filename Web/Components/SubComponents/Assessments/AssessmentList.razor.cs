using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.Assessments
{
    public partial class AssessmentList : ComponentBase
    {
        [Parameter]
        public IEnumerable<Assessment> Assessments { get; set; }
            = new List<Assessment>();

        [Parameter]
        public int CardId { get; set; }

        [Parameter]
        public EventCallback<Assessment> CreateAssessment { get; set; }

        [Parameter]
        public EventCallback<int> DeleteAssessment { get; set; }

        private bool showForm = false;

        private void ToggleForm()
        {
            showForm = !showForm;
        }

        private async Task OnCreateAssessment(Assessment assessment)
        {
            assessment.CardId = CardId;
            await CreateAssessment.InvokeAsync(assessment);
            ToggleForm();
            await Task.CompletedTask;
        }
    }
}
