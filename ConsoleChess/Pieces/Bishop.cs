namespace ConsoleApp1;

public class Bishop: ChessPiece
{
    public Bishop(Color color) : base(color)
    {
        _symbol = 'B';
    }

    public override List<List<Move>> PossibleMoves()
    {
        return new List<List<Move>>();
    }
}