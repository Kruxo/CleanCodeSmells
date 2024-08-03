namespace CleanCodeGaming.Models;

public class Player
{
    public string Name { get; private set; }
    public int NGames { get; private set; }
    private int _totalGuess;

    public Player(string name, int guesses)
    {
        Name = name;
        NGames = 1;
        _totalGuess = guesses;
    }

    public void Update(int guesses)
    {
        _totalGuess += guesses;
        NGames++;
    }

    public double Average()
    {
        return (double)_totalGuess / NGames;
    }

    public override bool Equals(object obj)
    {
        return obj is Player data && Name == data.Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
