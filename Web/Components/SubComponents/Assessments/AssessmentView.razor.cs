using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.Assessments
{
    public partial class AssessmentView : ComponentBase
    {
        [Parameter]
        public Assessment Assessment { get; set; }
            = new();

    }
}
