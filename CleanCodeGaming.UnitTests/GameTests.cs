using Moq;
using CleanCodeGaming.MooGame.Interfaces;
using CleanCodeGaming.MooGame.Services;

namespace CleanCodeGaming.UnitTests
{
    [TestClass]
    public class GameTests
    {
        //mock object för att kunna utföra tester 
        private Mock<IUserInterface> _mockUI;
        private Mock<ILeaderboard> _mockLeaderboard;
        private Mock<IGenerateNumber> _mockNumberGenerator;
        private Game _game;

        [TestInitialize]
        public void Setup()
        {
            _mockUI = new Mock<IUserInterface>();
            _mockLeaderboard = new Mock<ILeaderboard>();
            _mockNumberGenerator = new Mock<IGenerateNumber>();
            _game = new Game(_mockUI.Object, _mockLeaderboard.Object, _mockNumberGenerator.Object);
        }

        [TestMethod]
        public void AskToPlayAgain_UserSelectsYes_ReturnsTrue()
        {
            // Arrange
            _mockUI.SetupSequence(ui => ui.ReadLine())
                   .Returns("yes");

            // Act
            bool result = _game.AskToPlayAgain();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AskToPlayAgain_UserSelectsNo_ReturnsFalse()
        {
            // Arrange
            _mockUI.SetupSequence(ui => ui.ReadLine())
                   .Returns("no");

            // Act
            bool result = _game.AskToPlayAgain();

            // Assert
            Assert.IsFalse(result);
        }
        //Kända slumpsiffror som det stod i labb pdfen
        [TestMethod]
        public void CheckBullsAndCows_UsersGuessHasBullsAndCows_ReturnsBBCommaCC()
        {
            // Arrange
            string goal = "1234";
            string guess = "1243";

            // Act
            string result = _game.CheckBullsAndCows(goal, guess);

            // Assert
            Assert.AreEqual("BB,CC", result);
        }

        [TestMethod]
        public void CheckBullsAndCows_UsersGuessHasNoBullsOrCows_ReturnsComma()
        {
            // Arrange
            string goal = "1234";
            string guess = "6789";

            // Act
            string result = _game.CheckBullsAndCows(goal, guess);

            // Assert
            Assert.AreEqual(",", result);
        }

        [TestMethod]
        public void CheckBullsAndCows_UsersGuessIsCorrect_ReturnsBBBBComma()
        {
            // Arrange
            string goal = "1234";
            string guess = "1234";

            // Act
            string result = _game.CheckBullsAndCows(goal, guess);

            // Assert
            Assert.AreEqual("BBBB,", result);
        }

        [TestMethod]
        public void CheckValidGuess_UserInputsInvalidGuess_ReturnsInvalidPrompt()
        {
            // Arrange
            string invalidGuess = "1122"; // Not unique digits
            string validGuess = "1234";

             _mockUI.SetupSequence(ui => ui.ReadLine())
                   .Returns(invalidGuess)
                   .Returns(validGuess);

            // Act
            string result = _game.CheckValidGuess();

            // Assert
            Assert.AreEqual(validGuess, result);

            // Ensures that the correct error message is output exactly once
            _mockUI.Verify(ui => ui.WriteLine(It.Is<string>(s => s.Contains("Invalid input"))), Times.Once);
        }


    }
}
