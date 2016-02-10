using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FTetris.Model.Tests
{
    [TestClass()]
    public class TwoDimensionaArrayExtensionTests
    {
        [TestMethod()]
        public void IsEqualTest()
        {
            var array1 = new int[,] {
                { 1, 2, 3, 4},
                { 5, 6, 7, 8}
            };
            var array1_1 = array1.Turn();
            var array1_2 = array1_1.Turn(false);

            Assert.IsFalse(array1  .IsEqual(array1_1));
            Assert.IsTrue (array1  .IsEqual(array1_2));
            Assert.IsFalse(array1_1.IsEqual(array1_2));
        }

        [TestMethod()]
        public void GetRowTest()
        {
            var array1 = new int[,] {
                { 1, 2, 3, 4},
                { 5, 6, 7, 8}
            };

            var row1_0 = array1.GetRow(0);
            Assert.AreEqual(1, row1_0.ElementAt(0));
            Assert.AreEqual(5, row1_0.ElementAt(1));
            var row1_1 = array1.GetRow(2);
            Assert.AreEqual(3, row1_1.ElementAt(0));
            Assert.AreEqual(7, row1_1.ElementAt(1));
        }

        [TestMethod()]
        public void GetColumnTest()
        {
            var array1 = new int[,] {
                { 1, 2, 3, 4},
                { 5, 6, 7, 8}
            };

            var row1_0 = array1.GetColumn(0);
            Assert.AreEqual(1, row1_0.ElementAt(0));
            Assert.AreEqual(4, row1_0.ElementAt(3));
            var row1_1 = array1.GetColumn(1);
            Assert.AreEqual(6, row1_1.ElementAt(1));
            Assert.AreEqual(7, row1_1.ElementAt(2));
        }

        [TestMethod()]
        public void SizeTest()
        {
            var array1 = new int[,] {
                { 1, 2, 3, 4},
                { 5, 6, 7, 8}
            };
            var size1 = array1.Size();
            Assert.AreEqual(new Size<int> { Width = 2, Height = 4 }, size1);
        }
    }
}