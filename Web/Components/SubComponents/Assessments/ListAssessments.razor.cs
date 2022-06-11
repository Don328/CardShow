using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.Assessments
{
    public partial class ListAssessments: ComponentBase
    {
        [Parameter]
        public IEnumerable<Assessment> Assessments { get; set; }
            = new List<Assessment>();

        [Parameter]
        public EventCallback<int> OnDelete { get; set; }

        private Assessment selectedAssessment = new();


        private async Task Delete(int id)
        {
            await OnDelete.InvokeAsync(id);
        }
    }
}
