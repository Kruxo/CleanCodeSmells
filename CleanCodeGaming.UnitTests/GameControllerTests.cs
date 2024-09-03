using Moq;
using CleanCodeGaming.MooGame.Interfaces;
using CleanCodeGaming.MooGame.Services;

namespace CleanCodeGaming.UnitTests
{
    [TestClass]
    public class GameControllerTests
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
        public void AskToPlayAgain_UserSelectsYes_ReturnsTrue()
        {
            // Arrange
            _mockUI.SetupSequence(ui => ui.ReadLine())
                   .Returns("yes");

            // Act
            bool result = _gameController.AskToPlayAgain();

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
            bool result = _gameController.AskToPlayAgain();

            // Assert
            Assert.IsFalse(result);
        }
    }
}
