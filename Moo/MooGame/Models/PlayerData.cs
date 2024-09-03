namespace CleanCodeGaming.MooGame.Models;

public class PlayerData
{
    public string Name { get; private set; }
    public int NumberOfGames { get; private set; } 
    private int _numberOfGuesses; 

    public PlayerData(string name, int guesses)
    {
        Name = name;
        NumberOfGames = 1;
        _numberOfGuesses = guesses;
    }

    //Is it better to name all the methods Player...?
    public void Update(int guesses) 
    {
        _numberOfGuesses += guesses;
        NumberOfGames++;
    }

    public double Average()
    {
        return (double)_numberOfGuesses / NumberOfGames;
    }

    public override bool Equals(object obj)
    {
        return obj is PlayerData data && Name == data.Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
