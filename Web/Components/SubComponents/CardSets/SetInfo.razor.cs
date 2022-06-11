using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.CardSets
{
    public partial class SetInfo: ComponentBase
    {
        [Parameter]
        public CardSet Set { get; set; } = new();
    }
}
