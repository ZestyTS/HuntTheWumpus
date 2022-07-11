using HuntTheWumpus.Models;

namespace UnitTest.ModelTests
{
    [TestClass]
    public class RoomTest
    {
        [TestMethod]
        public void ConstructorZero()
        {
            Assert.IsNotNull(new Room(0));
        }
        [TestMethod]
        public void ConstructorRealData()
        {
            Assert.IsNotNull(new Room(2));
        }
    }
}