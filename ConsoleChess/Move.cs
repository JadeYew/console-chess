namespace ConsoleApp1;

public struct Move
{
    public readonly char startingColumn;
    public readonly char startingRow;
    public readonly char endingColumn;
    public readonly char endingRow;
    public readonly MoveType moveType;

    public Move(char startingColumn, char startingRow, char endingColumn, char endingRow, MoveType moveType)
    {
        this.startingColumn = startingColumn;
        this.startingRow = startingRow;
        this.endingColumn = endingColumn;
        this.endingRow = endingRow;
        this.moveType = moveType;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        var move = obj is Move ? (Move) obj : default;
        if (this.startingColumn != move.startingColumn) return false;
        if (this.startingRow != move.startingRow) return false;
        if (this.endingColumn != move.endingColumn) return false;
        if (this.endingRow != move.endingRow) return false;
        if (this.moveType != move.moveType) return false;
        return true;
    }

    public override int GetHashCode()
    {
        return $"{startingColumn}{startingRow}{endingColumn}{endingRow}{moveType}".GetHashCode();
    }
}

public enum MoveType
{
    MOVE,
    CAPTURE,
    MOVE_AND_CAPTURE
}