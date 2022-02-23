var game = new Game(8, 8, 4);
var cx = 0;
var cy = 0;

while (!game.IsEnded)
{
    Renderer.WriteField(game, cx, cy);
    var command = GetInput();
    if (command != null)
        game.Execute(command);
}

Renderer.WriteField(game, cx, cy);


Command? GetInput()
{
    var key = Console.ReadKey();
    
    switch (key.Key)
    {
        case ConsoleKey.UpArrow:
            if (cy > 0) cy--;
            return null;
        case ConsoleKey.DownArrow:
            if (cy < game.Height - 1) cy++;
            return null;
        case ConsoleKey.LeftArrow:
            if (cx > 0) cx--;
            return null;
        case ConsoleKey.RightArrow:
            if (cx < game.Width - 1) cx++;
            return null;
        case ConsoleKey.Spacebar when key.Modifiers.HasFlag(ConsoleModifiers.Shift):
            return new Command { Type = CommandTypes.Mark, X = cx, Y = cy };
        case ConsoleKey.Spacebar:
            return new Command { Type = CommandTypes.Open, X = cx, Y = cy };
        case ConsoleKey.Escape:
            return new Command { Type = CommandTypes.Quit };
    }

    return null;
}
