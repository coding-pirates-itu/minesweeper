using Minesweeper.Lib;
using System.Text.RegularExpressions;

var coordsRx = new Regex("(?<m> !)? (?<y> [A-Za-z]) (?<x> \\d+)", RegexOptions.IgnorePatternWhitespace);
var game = new Game(8, 8, 4);

while (!game.IsEnded)
{
    Renderer.WriteField(game);
    var command = GetInput();
    if (command != null)
        game.Execute(command);
}

Renderer.WriteField(game);


Command? GetInput()
{
    var s = Console.ReadLine();

    if (s is null || s == "quit")
        return Command.ExitCommand();

    var m = coordsRx.Match(s);
    if (!m.Success)
        return null;

    var x = int.Parse(m.Groups["x"].Value) - 1;
    var y = m.Groups["y"].Value.ToUpperInvariant()[0] - 'A';
    if (x < 0 || x >= game.Width || y < 0 || y >= game.Height)
        return null;

    return m.Groups["m"].Success ? Command.MarkCommand(x, y) : Command.CheckCommand(x, y);
}
