using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.Assessments
{
    public partial class AssessmentInfo: ComponentBase
    {
        [Parameter]
        public Assessment Assessment { get; set; } = new();
    }
}
