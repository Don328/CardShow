using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.Assessments
{
    public partial class AddAssessment : ComponentBase
    {
        private Assessment Assessment { get; set; }
            = new();

        [Parameter]
        public EventCallback<Assessment> OnCreate { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        private async Task Create()
        {
            Assessment.Date = DateTime.Now;
            await OnCreate.InvokeAsync(Assessment);
        }

        private async Task Cancel() =>
            await OnCancel.InvokeAsync();
    }
}
