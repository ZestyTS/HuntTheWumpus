using HuntTheWumpus.Models;

namespace UnitTest.ModelTests
{
    [TestClass]
    public class GameLocationTest
    {
        private readonly GameLocation GameLocation;
        private readonly GameControl GameControl = new GameControl(new Theme(1));
        public GameLocationTest()
        {
            GameControl.Cave = new Cave(1);
            GameControl.GameSetup();

            GameLocation = GameControl.GameLocation;
        }
        [TestMethod]
        public void ConstructorEmptyList()
        {
            Assert.IsNotNull(new GameLocation(new List<Room>()));
        }

        [TestMethod]
        public void ConstructorNull()
        {
            Assert.IsNotNull(new GameLocation(null));
        }
        [TestMethod]
        public void ConstructorRealValues()
        {
            Assert.IsNotNull(new GameLocation(new Cave(1).Rooms));
        }
        
        [TestMethod]
        public void ArrowHitWumpus()
        {
            GameLocation.WumpusLocation = 1;
            Assert.IsTrue(GameLocation.DidArrowHitWumpus(1));
        }
        [TestMethod]
        public void WarningString()
        {
            var neighbors = GameControl.Cave.GetNeighbors(1);
            Assert.IsNotNull(GameLocation.BuildWarningString(neighbors, GameControl.Bat, GameControl.Pitfall, GameControl.Wumpus));
        }
        [TestMethod]
        public void BuildNearByRoomsConnected()
        {
            var connected = GameControl.Cave.GetConnections(GameControl.Player.Location);

            Assert.IsNotNull(GameLocation.BuildNearByRooms(connected));
        }
        [TestMethod]
        public void BuildNearByRoomsNeighbors()
        {
            var neighbors = GameControl.Cave.GetNeighbors(GameControl.Player.Location);

            Assert.IsNotNull(GameLocation.BuildNearByRooms(neighbors));
        }
        [TestMethod]
        public void RoomHasHazard()
        {
            var neighbors = GameControl.Cave.GetNeighbors(GameControl.Player.Location);

            Assert.IsNotNull(GameLocation.CheckIfRoomHasHazard(neighbors[0]));
        }
    }
}