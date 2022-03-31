namespace ConsoleApp1;

public class Rook: ChessPiece
{
    public Rook(Color color) : base(color)
    {
        _symbol = 'R';
    }

    public override List<List<Move>> PossibleMoves()
    {
        return new List<List<Move>>();
    }
}