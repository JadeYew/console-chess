namespace ConsoleApp1;

public static class Utils
{
    public static bool IsColumnValid(char column)
    {
        return column is >= 'a' and <= 'h';
    }
    public static bool IsRowValid(char row)
    {
        return row is >= '1' and <= '8';
    }

    public const ConsoleColor MOVE_START_COLOR = ConsoleColor.DarkGreen;

    public const ConsoleColor MOVE_END_COLOR = ConsoleColor.Green;

    public const ConsoleColor CELL_WHITE = ConsoleColor.White;

    public const ConsoleColor CELL_BLACK = ConsoleColor.Black;
}