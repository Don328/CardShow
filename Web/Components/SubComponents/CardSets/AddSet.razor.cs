using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.CardSets;

public partial class AddSet : ComponentBase
{
    private CardSet NewSet = new();

    [Parameter]
    public EventCallback<CardSet> OnSubmit { get; set;  }

    private async Task SubmitNewSet()
    {
        await OnSubmit.InvokeAsync(NewSet);
    }
}
