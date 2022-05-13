using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.CardSets;

public partial class ViewSet : ComponentBase
{
    [Parameter]
    public CardSet SelectedSet { get; set; } = new();
}
