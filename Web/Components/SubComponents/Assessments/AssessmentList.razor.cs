using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.Assessments
{
    public partial class AssessmentList : ComponentBase
    {
        [Parameter]
        public IEnumerable<Assessment> Assessments { get; set; }
            = new List<Assessment>();
    }
}
