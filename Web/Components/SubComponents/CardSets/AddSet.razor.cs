using CardShow.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace CardShow.Web.Components.SubComponents.CardSets;

public partial class AddSet : ComponentBase
{
    private CardSet NewSet = new();

    [Parameter]
    public EventCallback<CardSet> OnSubmit { get; set;  }

    [Parameter]
    public EventCallback OnCancel { get; set; }

    private async Task Cancel() =>
        await OnCancel.InvokeAsync();

    private async Task Submit()
    {
        await OnSubmit.InvokeAsync(NewSet);
    }
}
