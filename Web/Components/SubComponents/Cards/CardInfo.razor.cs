using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.Cards
{
    public partial class CardInfo: ComponentBase
    {
        [Parameter]
        public Card Card { get; set; }
    }
}
