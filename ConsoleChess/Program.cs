namespace ConsoleApp1;

public class Program
{
    public static void Main()
    {
        Game game = new Game();
        while (game.Round())
        {
            Console.WriteLine();
            Console.WriteLine("PRESS ENTER TO CONTINUE");
            Console.ReadLine();
            Console.Clear();
        }
    }
}