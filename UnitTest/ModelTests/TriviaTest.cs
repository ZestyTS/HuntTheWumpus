using HuntTheWumpus.Models;

namespace UnitTest.ModelTests
{
    [TestClass]
    public class TriviaTest
    {
        [TestMethod]
        public void ContructorEmpty()
        {
            Assert.IsNotNull(new Trivia());
        }
        [TestMethod]
        public void GetTrivias()
        {
            var trivia = new Trivia();
            Assert.IsTrue(trivia.GetTrivias().Count > 1);
        }
        [TestMethod]
        public void SetupTriviaBattle()
        {
            var trivia = new Trivia();
            var max = 10;
            Assert.IsTrue(trivia.SetupTriviaBattle(max).Count == max);
        }
    }
}