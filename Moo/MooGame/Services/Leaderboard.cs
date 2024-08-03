using CleanCodeGaming.Interfaces;
using CleanCodeGaming.Models;

namespace CleanCodeGaming.Services;

public class Leaderboard : ILeaderboard
{
	private readonly string _filePath;

	public Leaderboard(string filePath)
	{
		_filePath = filePath;
	}

	public void SaveResult(string name, int guesses)
	{
		using (StreamWriter output = new StreamWriter(_filePath, append: true))
		{
			output.WriteLine($"{name}#&#{guesses}");
		}
	}

	public void ShowTopList()
	{
		List<Player> results = new List<Player>();

		using (StreamReader input = new StreamReader(_filePath))
		{
			string line;
			while ((line = input.ReadLine()) != null)
			{
				string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
				string name = nameAndScore[0];
				int guesses = Convert.ToInt32(nameAndScore[1]);
				Player pd = new Player(name, guesses);
				int pos = results.IndexOf(pd);
				if (pos < 0)
				{
					results.Add(pd);
				}
				else
				{
					results[pos].Update(guesses);
				}
			}
		}

		results.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));
        Console.WriteLine("Leaderboard:\n");
        Console.WriteLine("Player     games  average");
		foreach (Player p in results)
		{
			Console.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NGames, p.Average()));
		}
	}
}
