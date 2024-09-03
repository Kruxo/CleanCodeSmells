using CleanCodeGaming.MooGame.Interfaces;

namespace CleanCodeGaming.MooGame.Services;

public class RandomNumberGenerator : IRandomGenerateNumber 
{
    public string GenerateGoalNumber()
    {
        Random random = new Random();
        string goalNumber = "";

        for (int i = 0; i < 4; i++)
        {
            int randomDigitValue = random.Next(10);
            string randomDigit = randomDigitValue.ToString();

            while (goalNumber.Contains(randomDigit))
            {
                randomDigitValue = random.Next(10);
                randomDigit = randomDigitValue.ToString();
            }

            goalNumber += randomDigit;
        }

        return goalNumber;
    }
}
