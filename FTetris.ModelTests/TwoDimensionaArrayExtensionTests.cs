using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FTetris.Model.Tests
{
    [TestClass()]
    public class TwoDimensionaArrayExtensionTests
    {
        [TestMethod()]
        public void IsTheSameTest()
        {
            var array1 = new int[,] {
                { 1, 2, 3, 4},
                { 5, 6, 7, 8}
            };
            var array1_1 = array1  .Turn(     );
            var array1_2 = array1_1.Turn(false);

            Assert.IsFalse(array1  .IsEqual(array1_1));
            Assert.IsTrue (array1  .IsEqual(array1_2));
            Assert.IsFalse(array1_1.IsEqual(array1_2));
        }
    }
}