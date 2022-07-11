using HuntTheWumpus.Models;
using HuntTheWumpus.Models.Hazards;

namespace UnitTest.ModelTests
{
    [TestClass]
    public class ThemeTest
    {
        [TestMethod]
        public void ConstructorZero()
        {
            var theme = new Theme(0);
            Assert.AreEqual(theme.GameName, string.Empty);
        }
        [TestMethod]
        public void ConstructorRealData()
        {
            var theme = new Theme(1);
            Assert.IsTrue(theme.GameName != string.Empty);
        }
        [TestMethod]
        public void GetWumpusObject()
        {
            var theme = new Theme(1);
            Assert.IsInstanceOfType(theme.GetWumpusObject(), typeof(Wumpus));
        }
        [TestMethod]
        public void GetBatObject()
        {
            var theme = new Theme(1);
            Assert.IsInstanceOfType(theme.GetBatObject(), typeof(Bat));
        }
        [TestMethod]
        public void GetPitfallObject()
        {
            var theme = new Theme(1);
            Assert.IsInstanceOfType(theme.GetPitfallObject(), typeof(Pitfall));
        }
    }
}