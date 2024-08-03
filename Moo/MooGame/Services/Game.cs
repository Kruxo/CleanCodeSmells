using CleanCodeGaming.Interfaces;
using CleanCodeGaming.Models;

namespace CleanCodeGaming.Services;

//Bug 1: Right now you can guess four of the same numbers,
//and it will show one bull and three cows if that number is one of the correct numbers
//Bug 2: Right now if you guess wrong more than once the console will repeat that number
//Bug 3: No error handling when it comes to making a guess with other than numbers example characters
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

			_ui.WriteLine("\nNew game:\nFor practice, number is: " + goal + "\n");

			string guess = _ui.ReadLine();
			int nGuess = 1;
			string bbcc = CheckBC(goal, guess);
			_ui.WriteLine(bbcc + "\n");

			while (bbcc != "BBBB,")
			{
				nGuess++;
				guess = _ui.ReadLine();
				_ui.WriteLine(guess + "\n");
				bbcc = CheckBC(goal, guess);
				_ui.WriteLine(bbcc + "\n");
			}

            _ui.WriteLine("Correct, it took " + nGuess + " guesses\n");

            _leaderboard.SaveResult(_name, nGuess);
			_leaderboard.ShowTopList();

            _ui.WriteLine("\nDo you wish to continue?");

            string answer = _ui.ReadLine();
			playOn = answer != null && answer != "" && answer.Substring(0, 1).ToLower() != "n";
			Console.Clear();

        }
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
