using CardShow.Shared.Interfaces;
using CardShow.Shared.Models;
using CardShow.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.Cards
{

    public partial class ViewCard : ComponentBase
    {
        private enum ViewMode
        {
            Default,
            AddAssessment,
            AssessmentDetails,
            DeleteCard
        }

        [Inject]
        public ILogger<ViewCard> Logger { get; set; }

        [Inject]
        public IAPIService<Assessment> Api { get; set; }

        [Parameter]
        public Card Card { get; set; }
            = new();

        [Parameter]
        public EventCallback<int> OnDelete { get; set; }

        private ViewMode viewMode = ViewMode.Default;
        private Assessment selectedAssessment = new();
        private IEnumerable<Assessment> assessments
            = Enumerable.Empty<Assessment>();
        
        private void ShowCard() => viewMode = ViewMode.Default;
        private void EnableDelete() => viewMode = ViewMode.DeleteCard;
        private void ShowNewAssessmentForm() => viewMode = ViewMode.AddAssessment;

        protected async override Task OnParametersSetAsync()
        {
            assessments = await Api.Get(Card.Id);
        }

        private async Task Delete()
        {
            await OnDelete.InvokeAsync(Card.Id);
        }

        private void ShowAssessment(Assessment assessment)
        {
            selectedAssessment = assessment;
            viewMode = ViewMode.AssessmentDetails;
        }

        private async Task CreateAssessment(Assessment assessment)
        {
            assessment.CardId = Card.Id;
            await Api.Add(assessment);
            assessments = await Api.Get(Card.Id);
            viewMode = ViewMode.Default;
        }

        private async Task DeleteAssessment(int id)
        {
            await Api.Delete(id);
            assessments = await Api.Get(Card.Id);
            viewMode = ViewMode.Default;
        }
    }
}
