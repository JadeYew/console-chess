using System.Text;

namespace ConsoleApp1;

public class GameBoard
{
    private ChessPiece?[,] Board { get; }

    public GameBoard()
    {
        Board = new ChessPiece[8, 8];
    }

    public ChessPiece? MoveChessPiece(Move move)
    {
        var piece = Board[RowIndex(move.startingRow), ColumnIndex(move.startingColumn)];
        Board[RowIndex(move.startingRow), ColumnIndex(move.startingColumn)] = null;
        var otherPiece = Board[RowIndex(move.endingRow), ColumnIndex(move.endingColumn)];
        piece.Column = move.endingColumn;
        piece.Row = move.endingRow;
        Board[RowIndex(move.endingRow), ColumnIndex(move.endingColumn)] = piece;
        return otherPiece;
    }

    public void SetCell(char column, char row, ChessPiece? piece)
    {
        Board[RowIndex(row), ColumnIndex(column)] = piece;
        piece.Column = column;
        piece.Row = row;
    }

    public ChessPiece? GetCell(char column, char row)
    {
        return Board[RowIndex(row), ColumnIndex(column)];
    }

    public List<Move> LegalMoves(char column, char row)
    {
        var piece = GetCell(column, row);
        var legalMoves = new List<Move>();
        if (piece == null)
            return legalMoves;
        foreach (var moveSequence in piece.PossibleMoves())
        {
            // We iterate over each move in a sequence and checking if they are legal.
            // If a move is illegal we stop checking the sequence because all consecutive moves are also illegal.
            foreach (var move in moveSequence)
            {
                var otherPiece = GetCell(move.endingColumn, move.endingRow);
                if (move.moveType == MoveType.MOVE_AND_CAPTURE)
                {
                    if (otherPiece == null || otherPiece.color != piece.color)
                    {
                        legalMoves.Add(move);
                    }
                    else
                    {
                        break;
                    }
                }
                else if(move.moveType == MoveType.MOVE)
                {
                    if (otherPiece == null)
                    {
                        legalMoves.Add(move);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (otherPiece != null && otherPiece.color != piece.color)
                    {
                        legalMoves.Add(move);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        return legalMoves;
    }

    private int RowIndex(char row)
    {
        return row - 49;
    }

    private int ColumnIndex(char column)
    {
        return column - 97;
    }

    public void PrintBoard(List<Move>? moves)
    {
        var startingCells = new HashSet<(char column, char row)>();
        var endingCells = new HashSet<(char column, char row)>();
        if (moves != null)
        {
            foreach (var move in moves)
            {
                startingCells.Add(ValueTuple.Create(move.startingColumn, move.startingRow));
                endingCells.Add(ValueTuple.Create(move.endingColumn, move.endingRow));
            }
        }

        var oldForegroundColor = Console.ForegroundColor;
        var oldBackgroundColor = Console.BackgroundColor;

        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write("O");
        Console.ForegroundColor = ConsoleColor.Black;
        for (int i = 0; i < 8; i++)
        {
            Console.Write($"{(char)(i + 'a')}");
        }
        Console.WriteLine();

        var bgColor = Color.WHITE;
        for (char row = '8'; row >= '1'; row--)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(row);
            for (char column = 'a'; column <= 'h'; column++)
            {
                var cell = ValueTuple.Create(column, row);
                if (Board[RowIndex(row), ColumnIndex(column)] != null)
                {
                    Board[RowIndex(row), ColumnIndex(column)].PrintSymbol(bgColor, startingCells.Contains(cell), endingCells.Contains(cell));
                }
                else
                {
                    if (startingCells.Contains(cell))
                    {
                        Console.BackgroundColor = Utils.MOVE_START_COLOR;
                        Console.ForegroundColor = Utils.MOVE_START_COLOR;
                    }
                    else if(endingCells.Contains(cell))
                    {
                        Console.BackgroundColor = Utils.MOVE_END_COLOR;
                        Console.ForegroundColor = Utils.MOVE_END_COLOR;
                    }
                    else
                    {
                        if (bgColor is Color.WHITE)
                        {
                            Console.BackgroundColor = Utils.CELL_WHITE;
                            Console.ForegroundColor = Utils.CELL_WHITE;
                        }
                        else
                        {
                            Console.BackgroundColor = Utils.CELL_BLACK;
                            Console.ForegroundColor = Utils.CELL_BLACK;
                        }
                    }
                    
                    Console.Write("O");
                }

                bgColor = bgColor == Color.WHITE ? Color.BLACK : Color.WHITE;
            }
            bgColor = bgColor == Color.WHITE ? Color.BLACK : Color.WHITE;
            Console.WriteLine();
        }

        Console.BackgroundColor = oldBackgroundColor;
        Console.ForegroundColor = oldForegroundColor;
    }
}