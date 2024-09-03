using CleanCodeGaming.MooGame.Interfaces;

namespace CleanCodeGaming.MooGame.Services;

public class GameController : IGameController
{
    private readonly IUserInterface _ui;
    private readonly ILeaderboard _leaderboard;
    private readonly IRandomGenerateNumber _numberGenerator;
    private string _name;
    private GameLogic _gameLogic;

    public GameController(IUserInterface ui, ILeaderboard leaderboard, IRandomGenerateNumber numberGenerator, GameLogic gameLogic)
    {
        _ui = ui;
        _leaderboard = leaderboard;
        _numberGenerator = numberGenerator;
        _gameLogic = gameLogic;
    }

    public void RunProgram()
    {
        PromptForName();
        bool playOn = true;

        while (playOn)
        {
            string goal = StartNewGame();
            int numberOfGuesses = GamePlay(goal);
            RecordResult(numberOfGuesses);
            playOn = AskToPlayAgain();
            Console.Clear();
        }
    }

    public void PromptForName()
    {
        _ui.WriteLine("Enter your name:");
        _name = _ui.ReadLine();
    }

    public string StartNewGame()
    {
        string goal = _numberGenerator.GenerateGoalNumber();
        _ui.WriteLine($"\nNew game (for practice, your number is {goal}):\n");
        return goal;
    }

    public int GamePlay(string goal)
    {
        int numberOfGuesses = 1;
        string guess = _gameLogic.CheckValidGuess(_ui);
        string bbcc = _gameLogic.CheckBullsAndCows(goal, guess);
        _ui.WriteLine($"{bbcc}\n");

        while (bbcc != "BBBB,")
        {
            numberOfGuesses++;
            guess = _gameLogic.CheckValidGuess(_ui);
            bbcc = _gameLogic.CheckBullsAndCows(goal, guess);
            _ui.WriteLine($"{bbcc}\n");
        }

        _ui.WriteLine($"Correct, it took {numberOfGuesses} guesses\n");
        return numberOfGuesses;
    }

    public void RecordResult(int numberOfGuesses)
    {
        _leaderboard.SaveResult(_name, numberOfGuesses);
        _leaderboard.ShowTopList();
    }

    public bool AskToPlayAgain()
    {
        _ui.WriteLine("\nDo you want to play again?");
        _ui.WriteLine("Yes/No");

        string answer;
        while (true)
        {
            answer = _ui.ReadLine().Trim().ToLower();

            if (answer == "yes")
            {
                return true;
            }
            else if (answer == "no")
            {
                return false;
            }
            else
            {
                _ui.WriteLine("Invalid input. Please enter either yes or no");
            }
        }
    }

}
