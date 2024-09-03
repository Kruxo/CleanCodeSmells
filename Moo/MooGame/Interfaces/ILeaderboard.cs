namespace CleanCodeGaming.MooGame.Interfaces;

public interface ILeaderboard
{
    void SaveResult(string name, int guesses);
    void ShowTopList();
}
