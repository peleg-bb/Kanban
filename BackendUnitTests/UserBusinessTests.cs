using NUnit.Framework;

namespace BackendUnitTests
{
    /// <summary>
    /// This class should be used as a unit testing class for user features
    /// </summary>
    public class UserBusinessTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
            Assert.AreEqual(0, 0, "Should be 0");
        }
    }
}