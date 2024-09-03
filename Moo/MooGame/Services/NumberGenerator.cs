using CleanCodeGaming.MooGame.Interfaces;

namespace CleanCodeGaming.MooGame.Services;

public class NumberGenerator : IGenerateNumber //Maybe change the class to GoalGenerator and method to NumberGenerator? Update: Keep it as it is in case future games need this class
{
    //Generates random digits up to 4 and adds it to the goal variable
    public string GenerateGoalNumber()
	{
		Random randomGenerator = new Random();
		string goal = "";
		for (int i = 0; i < 4; i++)
		{
			int random = randomGenerator.Next(10);
			string randomDigit = "" + random;
			while (goal.Contains(randomDigit))
			{
				random = randomGenerator.Next(10);
				randomDigit = "" + random;
			}
			goal += randomDigit;
		}
		return goal;
	}
}
