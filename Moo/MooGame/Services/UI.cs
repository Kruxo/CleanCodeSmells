using CleanCodeGaming.MooGame.Interfaces;

namespace CleanCodeGaming.MooGame.Services;

public class UI : IUserInterface
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public string ReadLine()
    {
        return Console.ReadLine();
    }
}
