namespace ConsoleApp1;

public class Queen: ChessPiece
{
    public Queen(Color color) : base(color)
    {
        _symbol = 'Q';
    }

    public override List<List<Move>> PossibleMoves()
    {
        throw new NotImplementedException();
        return new List<List<Move>>();
    }
}