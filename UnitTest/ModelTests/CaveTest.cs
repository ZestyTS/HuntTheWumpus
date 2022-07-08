using HuntTheWumpus.Models;

namespace UnitTest.ModelTests
{
    [TestClass]
    public class CaveTest
    {
        private Cave Cave = new(1);
        [TestMethod]
        public void ConstructorWithZero()
        {
            var cave = new Cave(0);
            Assert.IsTrue(cave.Size == 0 && cave.Number == 0);
        }
        [TestMethod]
        public void ConstructorWithRealData()
        {
            Assert.IsNotNull(new Cave(1));
        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetNeighborsBadData()
        {
            Cave = new(1);
            Cave.GetNeighbors(-1);
        }
        [TestMethod]
        public void GetNeighborsGoodData()
        {
            Cave = new(1);
            Assert.IsTrue(Cave.GetNeighbors(1).Count > 0);
        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetConnectionsBadData()
        {
            Cave = new(1);
            Cave.GetNeighbors(-1);
        }
        [TestMethod]
        public void GetConnectionsGoodData()
        {
            Cave = new(1);
            Assert.IsTrue(Cave.GetNeighbors(1).Count > 0);
        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetRoomNumbersBadData()
        {
            Cave = new(1);
            Cave.GetNeighbors(-1);
        }
        [TestMethod]
        public void GetRoomNumbersGoodData()
        {
            Cave = new(1);
            Assert.IsTrue(Cave.GetNeighbors(1).Count > 0);
        }
    }
}