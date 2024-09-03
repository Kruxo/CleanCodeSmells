using CleanCodeGaming.MooGame.Interfaces;
using CleanCodeGaming.MooGame.Models;

namespace CleanCodeGaming.MooGame.Services;

//Reads and writes player data from and to a file 
//Sorts, updates and displays the leaderboard 
public class Leaderboard : ILeaderboard
{
    private readonly string _filePath;
    private readonly IUserInterface _ui;

    public Leaderboard(string filePath, IUserInterface ui)
    {
        _filePath = filePath;
        _ui = ui;
    }

    //StreamWriter opens the file in append mode and adds data/results as a string, if file doesn't exist it will be created to designated filepath
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
            _ui.WriteLine($"Error saving the result: {ex.Message}");
        }
    }

    public void ShowTopList()
    {
        List<PlayerData> results = new List<PlayerData>();

        using (StreamReader input = new StreamReader(_filePath))
        {

            string line;
            while ((line = input.ReadLine()) != null)
            {
                string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string name = nameAndScore[0];
                int guesses = Convert.ToInt32(nameAndScore[1]);
                PlayerData pd = new PlayerData(name, guesses);
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
        _ui.WriteLine("Leaderboard:");
        _ui.WriteLine("Player     games  average");
        foreach (PlayerData p in results)
        {
            _ui.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NumberOfGames, p.Average()));
        }
    }
}
