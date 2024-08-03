using CleanCodeGaming.Interfaces;

namespace CleanCodeGaming.Services;

public class NumberGenerator : IGenerateNumber
{
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
