namespace Minesweeper.Lib;


public enum Operations
{
    /// <summary>
    /// Assume there is no mine and step on the cell.
    /// </summary>
    Step,

    /// <summary>
    /// Assume there is a mine and mark it.
    /// </summary>
    ToggleMark,

    /// <summary>
    /// Leave the game.
    /// </summary>
    Exit
}
