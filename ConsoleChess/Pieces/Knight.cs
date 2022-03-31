namespace ConsoleApp1;

public class Knight: ChessPiece
{
    public Knight(Color color) : base(color)
    {
        _symbol = 'H';
    }

    public override List<List<Move>> PossibleMoves()
    {
        return new List<List<Move>>();
    }
}