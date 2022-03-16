namespace Minesweeper.Web.Shared;


public sealed class CellClickedEventArgs : EventArgs
{
    public bool IsRightClick { get; set; }


    public static readonly CellClickedEventArgs LeftClick = new();


    public static readonly CellClickedEventArgs RightClick = new() { IsRightClick = true };
}
