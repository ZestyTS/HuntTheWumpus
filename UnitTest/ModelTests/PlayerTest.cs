using HuntTheWumpus.Models;

namespace UnitTest.ModelTests
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void ConstructorEmptyString()
        {
            Assert.IsNotNull(new Player(""));
        }

        [TestMethod]
        public void ConstructorNull()
        {
            Assert.IsNotNull(new Player(null));
        }
        [TestMethod]
        public void ConstructorRealValues()
        {
            Assert.IsNotNull(new Player("UnitTest"));
        }
        [TestMethod]
        public void Move()
        {
            var player = new Player("");
            player.Movement = 0;
            player.Move(1);

            Assert.IsTrue(player.Movement != 0);
        }
        [TestMethod]
        public void Shoot()
        {
            var player = new Player("");
            player.Arrow = 1;
            player.Shoot();

            Assert.IsTrue(player.Arrow == 0);
        }
        [TestMethod]
        public void CalculateScore()
        {
            var player = new Player("");
            var score = player.Score;
            player.CalculateScore(false);

            Assert.IsTrue(score != player.Score);
        }

    }
}