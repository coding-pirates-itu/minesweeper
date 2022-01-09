using Microsoft.AspNetCore.Components;

namespace Minesweeper.Web.Shared;


public partial class ModeControl
{
    private bool mIsStepMode;

    [Parameter]
    public bool IsStepMode
    {
        get => mIsStepMode;
        set
        {
            if (mIsStepMode == value) return;
            mIsStepMode = value;
            IsStepModeChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public EventCallback<bool> IsStepModeChanged { get; set; }
}
