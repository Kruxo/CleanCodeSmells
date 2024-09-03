namespace CleanCodeGaming.MooGame.Interfaces;

public interface IGame
{
    void Play();
    string CheckValidGuess();
    string CheckBullsAndCows(string goal, string guess);
    bool AskToPlayAgain();


}
