namespace ConsoleApp1;

public class Player
{
    public HashSet<ChessPiece> Pieces { get; set; }
    public King King { get; set; }

    public Player()
    {
        Pieces = new HashSet<ChessPiece>();
    }
}