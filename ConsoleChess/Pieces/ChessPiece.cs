namespace ConsoleApp1;

public abstract class ChessPiece
{
    public readonly Color color;
    protected bool _hasMoved;
    protected char _symbol;
    public bool Capturable { get; protected set; }

    public char Column
    {
        get;
        set;
    }

    public char Row
    {
        get;
        set;
    }


    public ChessPiece(Color color)
    {
        this.color = color;
        _hasMoved = false;
        Capturable = true;
    }

    public abstract List<List<Move>> PossibleMoves();


    public void PrintSymbol(Color background, bool moveStart = false, bool moveEnd = false)
    {
        if (moveStart)
        {
            Console.BackgroundColor = Utils.MOVE_START_COLOR;
        }
        else if (moveEnd)
        {
            Console.BackgroundColor = Utils.MOVE_END_COLOR;
        }
        else
        {
            if (background is Color.WHITE)
            {
                Console.BackgroundColor = Utils.CELL_WHITE;
            }
            else
            {
                Console.BackgroundColor = Utils.CELL_BLACK;
            }
        }

        if (color is Color.WHITE)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
        }
        Console.Write(_symbol);
    }
}