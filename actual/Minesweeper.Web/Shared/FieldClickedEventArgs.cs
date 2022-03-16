namespace Minesweeper.Web.Shared;


public sealed class FieldClickedEventArgs : EventArgs
{
    public int X { get; set; }

    public int Y { get; set; }

    public bool IsRightClick { get; set; }
}
