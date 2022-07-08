using HuntTheWumpus.Helper;
namespace UnitTest.HelperTests
{
    [TestClass]
    public class UserInputTest
    {
        //All of these methods throw an error because they have a very specific requirement
        //Which is that it needs to be ran via the Console that means this isn't a Helper file
        //This is a required file and should be part of the GameController instead since these
        //functions should be private instead of public
        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void GetIntegerTest()
        {
            UserInput.GetInteger(0, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void GetStringTest()
        {
            UserInput.GetString();
        }
        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void ClearInputTest()
        {
            UserInput.ClearInput(0);
        }
        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void UserActionTest()
        {
            UserInput.UserAction(0);
        }
    }
}