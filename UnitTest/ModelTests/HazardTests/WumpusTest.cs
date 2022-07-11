using HuntTheWumpus.Models.Hazards;

namespace UnitTest.ModelTests.HazardTests
{
    [TestClass]
    public class WumpusTest
    {
        private Wumpus Wumpus = new("Wumpus", "Warning", "Speech");

        [TestMethod]
        public void ConstructorEmptyString()
        {
            Assert.IsNotNull(new Wumpus("", "", ""));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ConstructorNull()
        {
            //We don't want this object to be created
            //So it throwing an error is okay
            //Since it is not silently failing
            Assert.IsNull(new Wumpus(null, null, null));
        }
        [TestMethod]
        public void ConstructorRealValues()
        {
            Assert.IsNotNull(new Wumpus("Wumpus", "Warning", "Speech"));
        }
        [TestMethod]
        public void Move()
        {
            //Checking for Void is a little weird
            //We have to check if expected values changed
            //In this case it would be the State
            Wumpus = new("Wumpus", "Warning", "Speech");
            var state = Wumpus.State;

            Wumpus.Move(1);
            Assert.IsTrue(state != Wumpus.State);
        }
        [TestMethod]
        public void NewLocationSleep()
        {
            Wumpus = new("Wumpus", "Warning", "Speech");
            Wumpus.NewLocation(new List<int>());

            Assert.IsTrue(Wumpus.State == Wumpus.WumpusState.Sleep);
        }
        [TestMethod]
        public void NewLocationMoveZero()
        {
            Wumpus = new("Wumpus", "Warning", "Speech");
            var moveCounter = Wumpus.MoveCounter;

            Wumpus.State = Wumpus.WumpusState.Awake;
            Wumpus.NewLocation(new List<int>());

            Assert.IsTrue(moveCounter != Wumpus.MoveCounter);
        }
        [TestMethod]
        public void NewLocationMoveLessThanZero()
        {
            Wumpus = new("Wumpus", "Warning", "Speech")
            {
                MoveCounter = -1,
                State = Wumpus.WumpusState.Awake
            };
            var state = Wumpus.State;
            Wumpus.NewLocation(new List<int>());

            Assert.IsTrue(state != Wumpus.State);
        }
        [TestMethod]
        public void NewLocationMoveMoreThanZero()
        {
            Wumpus = new("Wumpus", "Warning", "Speech")
            {
                MoveCounter = 5,
                State = Wumpus.WumpusState.Awake
            };
            var location = Wumpus.Location;
            var list = new List<int>() { int.MinValue, int.MaxValue };
            Wumpus.NewLocation(list);

            Assert.IsTrue(location != Wumpus.Location);
        }
        [TestMethod]
        public void RoundMoveGreaterThanPlayMove()
        {
            var connectedRooms = new List<int>() { int.MinValue, int.MaxValue };
            var rooms = new List<int>() { int.MinValue, int.MaxValue };

            Wumpus = new("Wumpus", "Warning", "Speech")
            {
                PlayerMoveCounter = 10,
            };

            Wumpus.RoundMove(connectedRooms, rooms);

            Assert.IsTrue(Wumpus.PlayerMoveCounter != 10);
        }
        [TestMethod]
        public void RoundMoveActiveWumpus()
        {
            var connectedRooms = new List<int>() { int.MinValue, int.MaxValue };
            var rooms = new List<int>() { int.MinValue, int.MaxValue };

            Wumpus = new("Wumpus", "Warning", "Speech")
            {
                PlayerMoveCounter = 0,
                ActiveWumpus = true
            };

            Wumpus.RoundMove(connectedRooms, rooms);

            Assert.IsTrue(Wumpus.PlayerMoveCounter != 0);
        }
    }
}