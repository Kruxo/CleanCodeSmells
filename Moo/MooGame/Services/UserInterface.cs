using CleanCodeGaming.Interfaces;

namespace CleanCodeGaming.Services;

public class UserInterface : IUserInterface
{
	public void WriteLine(string message)
	{
		Console.WriteLine(message);
	}

	public string ReadLine()
	{
		return Console.ReadLine();
	}
}
