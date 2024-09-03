namespace CleanCodeGaming.MooGame.Interfaces;

public interface IGameLogic
{
    string CheckValidGuess(IUserInterface _ui);
    string CheckBullsAndCows(string goal, string guess);
}
