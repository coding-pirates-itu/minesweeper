namespace Minesweeper.Lib;


public class Game
{
    #region Fields

    private readonly bool[,] mField;

    private readonly DisplayedCell[,] mCells;

    #endregion


    #region Properties

    /// <summary>
    /// The number of cells across the game field.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// The number of cells along the game field.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Mines left unmarked.
    /// </summary>
    public int MinesLeft { get; private set; }

    public bool IsWon { get; private set; }

    public bool IsLost { get; private set; }

    public bool IsEnded => IsWon || IsLost;

    #endregion


    #region Init and clean-up

    /// <summary>
    /// Initialize the game field and distribute the mines.
    /// </summary>
    public Game(int width, int height, int mines)
    {
        Width = width;
        Height = height;
        MinesLeft = mines;
        mField = new bool[width, height];
        mCells = new DisplayedCell[width, height];

        SetMines(mField, mines);
        InitializeField(mCells);
    }

    #endregion


    #region API

    /// <summary>
    /// Use for rendering the field.
    /// </summary>
    public DisplayedCell Cell(int x, int y) => mCells[x, y];


    /// <summary>
    /// Execute the given command, update the game field.
    /// Then use <see cref="Cell"/> to get the update field.
    /// </summary>
    public void Execute(Command command)
    {
        switch (command.Operation)
        {
            case Operations.Exit:
                IsLost = true;
                return;

            case Operations.Step:
                ExecuteCheck(command.X, command.Y);
                break;

            case Operations.ToggleMark:
                ExecuteMark(command.X, command.Y);
                break;
        }
    }

    #endregion


    #region Utility

    private void SetMines(bool[,] mField, int mines)
    {
        var rnd = new Random();
        
        while (mines > 0)
        {
            var x = rnd.Next(Width);
            var y = rnd.Next(Height);
            if (mField[x, y]) continue;

            mField[x, y] = true;
            mines--;
        }
    }


    private void InitializeField(DisplayedCell[,] mCells)
    {
        for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
            {
                mCells[x, y] = new DisplayedCell { Neighbours = CalculateNeighbours(x, y) };
            }
    }


    /// <summary>
    /// How many mines are there in the neighbourhood of this cell?
    /// </summary>
    private int CalculateNeighbours(int x, int y)
    {
        var minX = Math.Max(x - 1, 0);
        var maxX = Math.Min(x + 1, Width - 1);
        var minY = Math.Max(y - 1, 0);
        var maxY = Math.Min(y + 1, Height - 1);

        var c = 0;
        for (int iy = minY; iy <= maxY; iy++)
            for (var ix = minX; ix <= maxX; ix++)
                if (mField[ix, iy])
                    c++;

        return c;
    }


    private void ExecuteCheck(int x, int y)
    {
        if (mField[x, y])
        {
            mCells[x, y].State = DisplayStates.OpenBomb;
            IsLost = true;
        }
        else
        {
            FloodFill(x, y);
        }
    }


    private void FloodFill(int x, int y)
    {
        if (mCells[x, y].State == DisplayStates.ShowUnarmed) return;

        mCells[x, y].State = DisplayStates.ShowUnarmed;

        if (mCells[x, y].Neighbours > 0) return;

        var minX = Math.Max(x - 1, 0);
        var maxX = Math.Min(x + 1, Width - 1);
        var minY = Math.Max(y - 1, 0);
        var maxY = Math.Min(y + 1, Height - 1);
        for (int iy = minY; iy <= maxY; iy++)
            for (var ix = minX; ix <= maxX; ix++)
                FloodFill(ix, iy);
    }


    private void ExecuteMark(int x, int y)
    {
        if (mCells[x, y].State == DisplayStates.Hide)
        {
            mCells[x, y].State = DisplayStates.MarkedBomb;
            MinesLeft--;

            if (MinesLeft == 0 && VerifyMines())
                IsWon = true;
        }
        else
        {
            mCells[x, y].State = DisplayStates.Hide;
            MinesLeft++;
        }
    }


    private bool VerifyMines()
    {
        for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                if ((mCells[x, y].State == DisplayStates.MarkedBomb) != mField[x, y])
                    return false;

        return true;
    }

    #endregion
}
