namespace CleanCodeGaming.MooGame.Interfaces;

public interface IGameController
{
    void RunProgram();
    void PromptForName();
    string StartNewGame();
    int GamePlay(string goal);
    void RecordResult(int numberOfGuesses);
    bool AskToPlayAgain();
}
