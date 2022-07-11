using HuntTheWumpus.Models.Hazards;

namespace UnitTest.ModelTests.HazardTests
{
    [TestClass]
    public class PitfallTest
    {
        [TestMethod]
        public void ConstructorEmptyString()
        {
            Assert.IsNotNull(new Pitfall("", "", ""));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ConstructorNull()
        {
            //We don't want this object to be created
            //So it throwing an error is okay
            //Since it is not silently failing
            Assert.IsNull(new Pitfall(null, null, null));
        }
        [TestMethod]
        public void ConstructorRealValues()
        {
            Assert.IsNotNull(new Pitfall("Pit", "Warning", "Speech"));
        }
    }
}