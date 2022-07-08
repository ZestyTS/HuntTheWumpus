using HuntTheWumpus.Models.Hazards;

namespace UnitTest.ModelTests.HazardTests
{
    [TestClass]
    public class BatTest
    {
        [TestMethod]
        public void ConstructorEmptyString()
        {
            Assert.IsNotNull(new Bat("", "", ""));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ConstructorNull()
        {
            //We don't want this object to be created
            //So it throwing an error is okay
            //Since it is not silently failing
            Assert.IsNull(new Bat(null, null, null));
        }
        [TestMethod]
        public void ConstructorRealValues()
        {
            Assert.IsNotNull(new Bat("Bat", "Warning", "Speech"));
        }
    }
}