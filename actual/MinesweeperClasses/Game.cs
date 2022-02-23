using System.Runtime.CompilerServices;
using static System.Reflection.Metadata.BlobBuilder;

[assembly:InternalsVisibleTo("MineSweeper.Test")]


internal class Game
{
    #region Fields

    private readonly bool[,] mField;

    private readonly DisplayedCell[,] mCells;

    #endregion


    #region Properties

    public int Width { get; }

    public int Height { get; }

    public int Bombs { get; private set; }

    public bool IsEnded { get; private set; }

    public GameStates State { get; private set; }

    #endregion


    public Game(int width, int height, int bombs)
    {
        Width = width;
        Height = height;
        Bombs = bombs;

        mField = new bool[width, height];
        mCells = new DisplayedCell[Width, Height];
        SetBombs(mField, bombs);
        InitializeDisplay();
    }


    public DisplayedCell GetCell(int x, int y) => mCells[x, y];


    public void Execute(Command command)
    {
        var x = command.X;
        var y = command.Y;

        switch (command.Type)
        {
            case CommandTypes.Quit:
                IsEnded = true;
                State = GameStates.Aborted;
                break;
            case CommandTypes.Mark:
                switch (mCells[x, y].State)
                {
                    case DisplayStates.ShowUnarmed:
                        return;
                    case DisplayStates.MarkedBomb:
                        Bombs++;
                        mCells[x, y].State = DisplayStates.Hide;
                        break;
                    default:
                        Bombs--;
                        mCells[x, y].State = DisplayStates.MarkedBomb;

                        if (Bombs == 0)
                            CheckBombs();
                        break;
                }
                break;
            case CommandTypes.Open:
                if (mCells[x, y].State == DisplayStates.ShowUnarmed)
                    return;

                if (mField[x, y])
                {
                    mCells[x, y].State = DisplayStates.OpenBomb;
                    IsEnded = true;
                    State = GameStates.Loose;
                    return;
                }

                if (mCells[x, y].Neighbours == 0)
                    OpenEmpty(x, y);
                else
                    mCells[x, y].State = DisplayStates.ShowUnarmed;
                break;
        }
    }


    private void SetBombs(bool[,] mField, int bombs)
    {
        if (bombs > Width * Height)
            throw new ArgumentException("Too many bombs");

        var rnd = new Random();

        while (bombs > 0)
        {
            var x = rnd.Next(Width);
            var y = rnd.Next(Height);

            if (! mField[x, y])
            {
                mField[x, y] = true;
                bombs--;
            }
        }
    }


    private void InitializeDisplay()
    {
        for (var x = 0; x < Width; x++)
            for (var y = 0; y < Height; y++)
            {
                var n = 0;

                var minX = Math.Max(x - 1, 0);
                var maxX = Math.Min(x + 1, Width - 1);
                var minY = Math.Max(y - 1, 0);
                var maxY = Math.Min(y + 1, Height - 1);

                for (var tx = minX; tx <= maxX; tx++)
                {
                    for (var ty = minY; ty <= maxY; ty++)
                    {
                        if (mField[tx, ty])
                            n++;
                    }
                }

                mCells[x, y] = new DisplayedCell
                {
                    State = DisplayStates.Hide,
                    Neighbours = n
                };
            }
    }


    private void OpenEmpty(int x, int y)
    {
        if (mCells[x, y].State != DisplayStates.Hide) return;
        mCells[x, y].State = DisplayStates.ShowUnarmed;

        if (mCells[x, y].Neighbours == 0)
        {
            var minX = Math.Max(x - 1, 0);
            var maxX = Math.Min(x + 1, Width - 1);
            var minY = Math.Max(y - 1, 0);
            var maxY = Math.Min(y + 1, Height - 1);

            for (var cy = minY; cy <= maxY; cy++)
                for (var cx = minX; cx <= maxX; cx++)
                    OpenEmpty(cx, cy);
        }
    }


    private void CheckBombs()
    {
        for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                if (mCells[x, y].State == DisplayStates.MarkedBomb && !mField[x, y])
                {
                    IsEnded = true;
                    State = GameStates.Loose;
                    return;
                }

        IsEnded = true;
        State = GameStates.Win;
    }
}
