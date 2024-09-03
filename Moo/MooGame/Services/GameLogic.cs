using CleanCodeGaming.MooGame.Interfaces;
using System.Text.RegularExpressions;

namespace CleanCodeGaming.MooGame.Services;

public class GameLogic : IGameLogic
{
    private readonly IUserInterface _ui;

    public GameLogic(IUserInterface ui)
    {
        _ui = ui;
    }

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

    public string CheckValidGuess(IUserInterface _ui)
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
}
