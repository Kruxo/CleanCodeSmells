using Moq;
using CleanCodeGaming.MooGame.Interfaces;
using CleanCodeGaming.MooGame.Services;

namespace CleanCodeGaming.UnitTests
{
    [TestClass]
    public class GameLogicTests
    {
        private Mock<IUserInterface> _mockUI;
        private Mock<ILeaderboard> _mockLeaderboard;
        private Mock<IRandomGenerateNumber> _mockNumberGenerator;
        private IGameController _gameController;
        private GameLogic _gameLogic;

        [TestInitialize]
        public void Setup()
        {
            _mockUI = new Mock<IUserInterface>();
            _mockLeaderboard = new Mock<ILeaderboard>();
            _mockNumberGenerator = new Mock<IRandomGenerateNumber>();
            _gameLogic = new GameLogic(_mockUI.Object);
            _gameController = new GameController(_mockUI.Object, _mockLeaderboard.Object, _mockNumberGenerator.Object, _gameLogic);
        }

        [TestMethod]
        public void CheckBullsAndCows_UsersGuessHasBullsAndCows_ReturnsBBCommaCC()
        {
            // Arrange
            string goal = "1234";
            string guess = "1243";

            // Act
            string result = _gameLogic.CheckBullsAndCows(goal, guess);

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
            string result = _gameLogic.CheckBullsAndCows(goal, guess);

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
            string result = _gameLogic.CheckBullsAndCows(goal, guess);

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
            string result = _gameLogic.CheckValidGuess(_mockUI.Object);

            // Assert
            Assert.AreEqual(validGuess, result);

            // Ensure that the correct error message is output exactly once
            _mockUI.Verify(ui => ui.WriteLine(It.Is<string>(s => s.Contains("Invalid input"))), Times.Once);
        }
    }
}
