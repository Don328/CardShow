using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.Cards
{
    public partial class CardList : ComponentBase
    {
        [Parameter]
        public IEnumerable<Card> Cards { get; set; }
            = new List<Card>();
    }
}
