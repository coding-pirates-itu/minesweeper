namespace Minesweeper.Lib;


public class Command
{
    #region Properties

    public Operations Operation { get; set; }

    /// <summary>
    /// 0..Width-1
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// 0..Height-1
    /// </summary>
    public int Y { get; set; }

    #endregion


    #region Init and clean-up

    private Command()
    {
    }


    public static Command ExitCommand() => new() { Operation = Operations.Exit };


    public static Command CheckCommand(int x, int y) => new() { Operation = Operations.Check, X = x, Y = y };

    #endregion
}
