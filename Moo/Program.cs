using CleanCodeGaming.MooGame.Interfaces;
using CleanCodeGaming.MooGame.Services;

namespace CleanCodeGaming;

//Remember to go through all comments and reduce if possible
//Remember to go through all naming of methods, variables... etc

class Program
{
    static void Main(string[] args)
    {
        IUserInterface ui = new UserInterface();
        ILeaderboard leaderboard = new Leaderboard("result.txt"); //file is locally at .../bin/Debug/net8.0 in the project's directory
        IGenerateNumber numberGenerator = new NumberGenerator();
        IGame game = new Game(ui, leaderboard, numberGenerator);
        game.Play();
    }
}
