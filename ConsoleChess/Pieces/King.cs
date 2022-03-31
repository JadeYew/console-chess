namespace ConsoleApp1;

public class King: ChessPiece
{
    public bool InCheck { get; set; }
    public King(Color color) : base(color)
    {
        Capturable = false;
        InCheck = false;
        _symbol = 'K';
    }

    public override List<List<Move>> PossibleMoves()
    {
        return new List<List<Move>>();
    }
}