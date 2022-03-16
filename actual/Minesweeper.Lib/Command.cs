namespace Minesweeper.Lib;


public class Command
{
    public CommandTypes Type { get; set; }

    public int X { get; set; }

    public int Y { get; set; }


    public static Command CheckCommand(int x, int y) =>
        new Command { Type = CommandTypes.Open, X = x, Y = y };


    public static Command MarkCommand(int x, int y) =>
        new Command { Type = CommandTypes.Mark, X = x, Y = y };
}
