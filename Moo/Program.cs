using CleanCodeGaming.MooGame.Interfaces;
using CleanCodeGaming.MooGame.Services;

namespace CleanCodeGaming;

class Program
{
    static void Main(string[] args)
    {
        IUserInterface ui = new UI();
        ILeaderboard leaderboard = new Leaderboard("result.txt"); //file is locally at .../bin/Debug/net8.0 in the project's directory
        IRandomGenerateNumber numberGenerator = new RandomNumberGenerator();
        GameLogic gameLogic = new GameLogic(ui);
        IGameController game = new GameController(ui, leaderboard, numberGenerator, gameLogic);
        game.RunProgram();
    }
}
