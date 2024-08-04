using CleanCodeGaming.Interfaces;
using System.Text.RegularExpressions;

namespace CleanCodeGaming.Services;

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

    //Game Controller
    //Logs the user's name and the amount of guesses (valid guesses with 4 unique digits) 
    //until the user answers correctly and gets four B's. The result then gets saved on a text file locally
    //and game shows the leaderboard and also asks the user with if they want to continue playing
	public void Play()
	{
		_ui.WriteLine("Enter your name:");
		_name = _ui.ReadLine();

		bool playOn = true;

		while (playOn)
		{
			string goal = _numberGenerator.GenerateGoalNumber();

			_ui.WriteLine("\nNew game (for practice, your number is " + goal + "):\n");

			string guess = GetValidGuess();
            int nGuess = 1;
			string bbcc = CheckBullsAndCows(goal, guess);
			_ui.WriteLine(bbcc + "\n");

			while (bbcc != "BBBB,")
			{
				nGuess++;
				guess = GetValidGuess();
                bbcc = CheckBullsAndCows(goal, guess);
				_ui.WriteLine(bbcc + "\n");
			}

            _ui.WriteLine("Correct, it took " + nGuess + " guesses\n");

            _leaderboard.SaveResult(_name, nGuess);
			_leaderboard.ShowTopList();

            playOn = AskToPlayAgain();

            Console.Clear();
        }
	}

    private bool AskToPlayAgain()
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

    private string GetValidGuess()
    {
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

    //Two way validation:
    //Regex to validate the user's input is exactly 4 digits long (with no other characters involved)
    //Distinct and Count validates that the input is 4 digits with unique numbers thus making sure no number is repeated
    private bool IsValidGuess(string guess)
    {
        return Regex.IsMatch(guess, @"^\d{4}$") && guess.Distinct().Count() == 4; 
    }

    //Game Logic
    //i and j loop iterates throuch each digit in the goal and guess
    //Then a comparison between the digits in the two loops either adds an number increment to bulls and/or cows
    //Then returns a construct of the two substrings to create a character combination of the two depending on the position of the iterated digits
    private string CheckBullsAndCows(string goal, string guess)
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
}
