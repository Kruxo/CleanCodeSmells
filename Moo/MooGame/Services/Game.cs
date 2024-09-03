using CleanCodeGaming.MooGame.Interfaces;

using System.Text.RegularExpressions;

namespace CleanCodeGaming.MooGame.Services;

public class Game : IGame
{
	private readonly IUserInterface _ui;
	private readonly ILeaderboard _leaderboard;
	private readonly IGenerateNumber _numberGenerator;
	private string _name;

	public Game(IUserInterface ui, ILeaderboard leaderboard, IGenerateNumber numberGenerator)
	{
		_ui = ui;
        _leaderboard = leaderboard;
		_numberGenerator = numberGenerator;
	}

    //Game controller, logs the user's name and the amount of guesses (valid guesses with 4 unique digits) 
    //until the user answers correctly and gets four B's. The result then gets saved on a text file locally
    //and game shows the leaderboard and also asks the user if they want to continue playing
	public void Play()
	{
		_ui.WriteLine("Enter your name:");
		_name = _ui.ReadLine();

		bool playOn = true;

		while (playOn)
		{
			string goal = _numberGenerator.GenerateGoalNumber();

			_ui.WriteLine("\nNew game (for practice, your number is " + goal + "):\n");

			string guess = CheckValidGuess();
            int numberOfGuesses = 1; //variable name change?
			string bbcc = CheckBullsAndCows(goal, guess);
			_ui.WriteLine(bbcc + "\n");

			while (bbcc != "BBBB,")
			{
                numberOfGuesses++;
				guess = CheckValidGuess();
                bbcc = CheckBullsAndCows(goal, guess);
				_ui.WriteLine(bbcc + "\n");
			}

            _ui.WriteLine("Correct, it took " + numberOfGuesses + " guesses\n");

            _leaderboard.SaveResult(_name, numberOfGuesses);
			_leaderboard.ShowTopList();

            playOn = AskToPlayAgain();

            Console.Clear();
        }
	}

    //Game Logic, i and j loop iterates throuch each digit from the generated goal and guess input from the user
    //Then a comparison between the digits in the two loops either adds an number increment to bulls or cows
    //Then returns a construct of the two substrings to create a character combination of B's/comma/C's depending on the position of the iterated digits
    public string CheckBullsAndCows(string goal, string guess)
    {
        int cows = 0, bulls = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (goal[i] == guess[j])
                {
                    if (i == j)
                    {
                        bulls++;
                    }
                    else
                    {
                        cows++;
                    }
                }
            }
        }
        return "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);
    }

    //Two way validation:
    //Regex validates the user's input is exactly 4 digits long (with no other characters involved)
    //Distinct and Count validates that the 4 digits are all unique 
    public string CheckValidGuess()
    {
        bool IsValidGuess(string guess)
        {
            return Regex.IsMatch(guess, @"^\d{4}$") && guess.Distinct().Count() == 4;
        }

        while (true)
        {
            _ui.WriteLine("Enter your guess: ");
            string guess = _ui.ReadLine();

            if (IsValidGuess(guess))
            {
                return guess;
            }
            else
            {
                _ui.WriteLine("\nInvalid input. Please enter exactly 4 unique digits.");
            }
        }
    }
    public bool AskToPlayAgain()
    {
        _ui.WriteLine("\nDo you want to play again?");
        _ui.WriteLine("1: Yes");
        _ui.WriteLine("2: No");

        string answer;
        while (true)
        {
            answer = _ui.ReadLine().Trim().ToLower();

            if (answer == "1" || answer == "yes")
            {
                return true;
            }
            else if (answer == "2" || answer == "no")
            {
                return false;
            }
            else
            {
                _ui.WriteLine("Invalid input. Please enter '1' for Yes or '2' for No.");
            }
        }
    }

}
