using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnoAutomatedTestsApp.UWPUnitTests
{
    [TestClass]
    public class DemoUtilsTests
    {
        [TestMethod]
        public void VerifyEvenNumberIsEven()
        {
            Assert.IsTrue(DemoUtils.IsEven(2), "Number 2 should be even");
        }
    }
}
