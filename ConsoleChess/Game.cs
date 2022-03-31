namespace ConsoleApp1;

public class Game
{
    public GameBoard Board { get; set; }
    public Player WhitePlayer { get; set; }
    public Player BlackPlayer { get; set; }
    public uint TurnCounter { get; set; }

    public Player CurrentPlayer
    {
        get
        {
            return TurnCounter % 2 == 0 ? BlackPlayer : WhitePlayer;
        }
    }

    public Player NextPlayer
    {
        get
        {
            return TurnCounter % 2 == 0 ? WhitePlayer : BlackPlayer;
        }
    }

    public Game()
    {
        TurnCounter = 1;
        Board = new GameBoard();
        WhitePlayer = new Player();
        BlackPlayer = new Player();
        for (char c = 'a'; c < 'i'; c++)
        {
            var pawn = new Pawn(Color.WHITE);
            WhitePlayer.Pieces.Add(pawn);
            Board.SetCell(c, '2', pawn);
            pawn = new Pawn(Color.BLACK);
            BlackPlayer.Pieces.Add(pawn);
            Board.SetCell(c, '7', pawn);
        }

        var rook = new Rook(Color.WHITE);
        WhitePlayer.Pieces.Add(rook);
        Board.SetCell('a', '1', rook);
        rook = new Rook(Color.WHITE);
        WhitePlayer.Pieces.Add(rook);
        Board.SetCell('h', '1', rook); 
        rook = new Rook(Color.BLACK);
        BlackPlayer.Pieces.Add(rook);
        Board.SetCell('a', '8', rook);
        rook = new Rook(Color.BLACK);
        BlackPlayer.Pieces.Add(rook);
        Board.SetCell('h', '8', rook);

        var knight = new Knight(Color.WHITE);
        WhitePlayer.Pieces.Add(knight);
        Board.SetCell('b', '1', knight);
        knight = new Knight(Color.WHITE);
        WhitePlayer.Pieces.Add(knight);
        Board.SetCell('g', '1', knight);
        knight = new Knight(Color.BLACK);
        BlackPlayer.Pieces.Add(knight);
        Board.SetCell('b', '8', knight);
        knight = new Knight(Color.BLACK);
        BlackPlayer.Pieces.Add(knight);
        Board.SetCell('g', '8', knight);

        var bishop = new Bishop(Color.WHITE);
        WhitePlayer.Pieces.Add(bishop);
        Board.SetCell('c', '1', bishop);
        bishop = new Bishop(Color.WHITE);
        WhitePlayer.Pieces.Add(bishop);
        Board.SetCell('f', '1', bishop);
        bishop = new Bishop(Color.BLACK);
        BlackPlayer.Pieces.Add(bishop);
        Board.SetCell('c', '8', bishop);
        bishop = new Bishop(Color.BLACK);
        BlackPlayer.Pieces.Add(bishop);
        Board.SetCell('f', '8', bishop);

        var queen = new Queen(Color.WHITE);
        WhitePlayer.Pieces.Add(queen);
        Board.SetCell('d', '1', queen);
        queen = new Queen(Color.BLACK);
        BlackPlayer.Pieces.Add(queen);
        Board.SetCell('d', '8', queen);

        var king = new King(Color.WHITE);
        WhitePlayer.Pieces.Add(king);
        WhitePlayer.King = king;
        Board.SetCell('e', '1', king);
        king = new King(Color.BLACK);
        BlackPlayer.Pieces.Add(king);
        BlackPlayer.King = king;
        Board.SetCell('e', '8', king);
    }

    private List<Move> GetMoves(char column, char row)
    {
        var piece = Board.GetCell(column, row);
        if (piece == null || !CurrentPlayer.Pieces.Contains(piece))
            return new List<Move>();
        if (CurrentPlayer.King.InCheck)
        {
            throw new NotImplementedException();
        }
        else
        {
            return Board.LegalMoves(column, row);
        }
    }

    public void MakeMove(Move move)
    {
        if (!GetMoves(move.startingColumn, move.startingRow).Contains(move))
            return;

        var otherPiece = Board.MoveChessPiece(move);
        if (otherPiece != null)
        {
            NextPlayer.Pieces.Remove(otherPiece);
        }
        
        foreach (var piece in CurrentPlayer.Pieces)
        {
            foreach (var legalMove in Board.LegalMoves(piece.Column, piece.Row))
            {
                if (legalMove.moveType is MoveType.CAPTURE or MoveType.MOVE_AND_CAPTURE)
                {
                    var capturedPiece = Board.GetCell(move.endingColumn, move.endingRow);
                    if (capturedPiece != null && capturedPiece is King)
                    {
                        NextPlayer.King.InCheck = true;
                        break;
                    }
                }
            }

            if (NextPlayer.King.InCheck)
            {
                break;
            }
        }
        TurnCounter++;
    }

    public bool Round()
    {
        Board.PrintBoard(null);
        var input = "";
        while (!InputValid(input))
        {
            Console.WriteLine("What piece movements you want to check?(type ColumnRow to see possibilities, type Q to quit)");
            input = Console.ReadLine();
            if (input is "Q" or "q")
            {
                return false;
            }
        }

        var moves = GetMoves(input[0], input[1]);
        Board.PrintBoard(moves);

        return true;

        bool InputValid(string? input)
        {
            if (input == null) return false;
            if (input.Length != 2) return false;
            return input[0] is >= 'a' and <= 'h' && input[1] is >= '1' and <= '8';
        }
    }
}