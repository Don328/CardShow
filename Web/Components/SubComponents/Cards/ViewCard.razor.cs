using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.Cards
{
    public partial class ViewCard : ComponentBase
    {
        [Parameter]
        public Card Card { get; set; }
            = new();
    }
}
