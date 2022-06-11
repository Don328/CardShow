using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.Assessments
{
    public partial class ViewAssessment: ComponentBase
    {
        private enum ViewMode
        {
            Default,
            Detail,
            Delete
        }

        [Parameter]
        public Assessment Assessment { get; set; }
            = new();

        [Parameter]
        public EventCallback<int> OnDelete { get; set; }

        private ViewMode viewMode = ViewMode.Default;

        private void EnableDelete() => viewMode = ViewMode.Delete;
        private void ShowDetails() => viewMode = ViewMode.Detail;
        private void DefaultView() => viewMode = ViewMode.Default;


        private async Task Delete() =>
            await OnDelete.InvokeAsync(Assessment.Id);
    }
}
