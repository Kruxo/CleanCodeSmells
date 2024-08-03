using CleanCodeGaming.Interfaces;
using System.Text.RegularExpressions;

namespace CleanCodeGaming.Services;

//Bug 4: Fix when answering continue or not at the end of the game

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
			string bbcc = CheckBC(goal, guess);
			_ui.WriteLine(bbcc + "\n");

			while (bbcc != "BBBB,")
			{
				nGuess++;
				guess = GetValidGuess();
                bbcc = CheckBC(goal, guess);
				_ui.WriteLine(bbcc + "\n");
			}

            _ui.WriteLine("Correct, it took " + nGuess + " guesses\n");

            _leaderboard.SaveResult(_name, nGuess);
			_leaderboard.ShowTopList();

            _ui.WriteLine("\nDo you wish to continue? (y/n)?");

            string answer = _ui.ReadLine();
			playOn = answer != null && answer != "" && answer.Substring(0, 1).ToLower() != "n"; //inte tydligt nog
			Console.Clear();

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

    public string CheckBC(string goal, string guess)
	{
		int cows = 0, bulls = 0;
		guess += "    "; // if player entered less than 4 chars
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
