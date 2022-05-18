using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.Assessments
{
    public partial class AddAssessment : ComponentBase
    {
        private Assessment Assessment { get; set; }
            = new();

        [Parameter]
        public EventCallback<Assessment> CreateAssessment { get; set; }

        private async Task OnCreate()
        {
            Assessment.Date = DateTime.Now;
            await CreateAssessment.InvokeAsync(Assessment);
        }
    }
}
