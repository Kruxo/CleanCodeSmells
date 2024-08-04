using CleanCodeGaming.Interfaces;
using CleanCodeGaming.Models;

namespace CleanCodeGaming.Services;

//Reads and writes player data to a file
//Sorts and displays the leaderboard to the console
public class Leaderboard : ILeaderboard
{
	private readonly string _filePath;

	public Leaderboard(string filePath)
	{
		_filePath = filePath;
	}

	//Saves the data that consist of the player's name and guesses into the file from Leaderboard's designated filepath
	//by using StreamWriter to open the file in append mode and adds data as a string, if file doesn't exist it will be created
	public void SaveResult(string name, int guesses)
	{
        try
        {
            using (StreamWriter output = new StreamWriter(_filePath, append: true))
            {
                output.WriteLine($"{name}#&#{guesses}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving the result: {ex.Message}");
        }
    }

	public void ShowTopList()
	{
        //Create list 'result' that store 'Players'. StreamReader opens the file in filepath and reads the data
        List<PlayerData> results = new List<PlayerData>();

		using (StreamReader input = new StreamReader(_filePath))
		{
			//While loop til every line in the file have been read
			string line;
			while ((line = input.ReadLine()) != null)
			{
                //Splits each line with "#&#" to separate playername and number of guesses as two elements into an array
                string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
				//Extracts the first element of the array which happens to be the player's name
				string name = nameAndScore[0];
				//Extract the second element of the array and converts it into an int
				int guesses = Convert.ToInt32(nameAndScore[1]);
				//Creates a new Player object with the extracted names and guesses
				PlayerData pd = new PlayerData(name, guesses);
                //Checks if player's name already exist in the result list of Players
				//and returns the index of the existing player, or return '-1' if not found in the list
                int pos = results.IndexOf(pd);
				//Adds Player to list if index is -1
				if (pos < 0)
				{
					results.Add(pd); //change variable names
				}
				//Update the existing player's guesses
				else
				{
					results[pos].Update(guesses);
				}
			}
		}

		results.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));
        Console.WriteLine("Leaderboard:");
        Console.WriteLine("Player     games  average");
		foreach (PlayerData p in results)
		{
			Console.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NumberOfGames, p.Average()));
		}
	}
}
