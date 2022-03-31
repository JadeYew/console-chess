using System.Text;

namespace ConsoleApp1;

public class Pawn: ChessPiece
{
    public Pawn(Color color) : base(color)
    {
        _symbol = 'P';
    }

    public override List<List<Move>> PossibleMoves()
    {

        var moves = new List<List<Move>>();

        sbyte moveDirection = this.color == Color.WHITE ? (sbyte)1 : (sbyte)-1;

        var move = new Move(Column, Row, Column, (char)(Row + moveDirection), MoveType.MOVE);
        if (Utils.IsRowValid(move.endingRow))
            moves.Add(new List<Move>{ move });

        if (!_hasMoved)
        {
            move = new Move(Column, Row, Column, (char)(Row + moveDirection + moveDirection), MoveType.MOVE);
            if (Utils.IsRowValid(move.endingRow))
                moves[0].Add(move);
        }

        move = new Move(Column, Row, (char)(Column + 1), (char)(Row + moveDirection), MoveType.CAPTURE);
        if (Utils.IsRowValid(move.endingRow) && Utils.IsColumnValid(move.endingColumn))
            moves.Add(new List<Move> { move });

        move = new Move(Column, Row, (char)(Column - 1), (char)(Row + moveDirection), MoveType.CAPTURE);
        if (Utils.IsRowValid(move.endingRow) && Utils.IsColumnValid(move.endingColumn))
            moves.Add(new List<Move> { move });

        return moves;
    }
}