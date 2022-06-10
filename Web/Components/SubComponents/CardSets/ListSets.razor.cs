using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.CardSets
{
    public partial class ListSets: ComponentBase
    {
        [Parameter]
        public IEnumerable<CardSet> Sets { get; set; }
            = new List<CardSet>();

        [Parameter]
        public EventCallback<int> OnViewSet { get; set; }

        private async Task ViewSet(EventArgs e, int id)
        {
            await OnViewSet.InvokeAsync(id);
        }
    }
}
