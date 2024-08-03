using CleanCodeGaming.Interfaces;
using CleanCodeGaming.Services;

namespace CleanCodeGaming;

class Program
{
    static void Main(string[] args)
    {
        IUserInterface ui = new UserInterface();
        ILeaderboard leaderboard = new Leaderboard("result.txt");
        IGenerateNumber numberGenerator = new NumberGenerator();
        IGame game = new Game(ui, leaderboard, numberGenerator);
        game.Play();
    }
}
