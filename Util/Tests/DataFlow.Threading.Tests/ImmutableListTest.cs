using System.Linq;
using AISTek.DataFlow.Threading;
using AISTek.UnitTestUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DataFlow.Threading.Tests
{
    /// <summary>
    /// This is a test class for ImmutableListTest and is intended
    /// to contain all ImmutableListTest Unit Tests
    /// </summary>
    [TestClass]
    public class ImmutableListTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        public void EmptyListTest<T>()
        {
            var list = ImmutableList<T>.Empty;

            Test.Throws<InvalidOperationException>(() =>
            {
                T item;
                list.Take(out item);
            }, "Empty ImmutableList<T> must throw InvalidOperationException when Take() invoked");
        }

        [TestMethod]
        [DeploymentItem("DataFlow.Threading.dll")]
        public void EmptyListTest()
        {
            EmptyListTest<GenericParameterHelper>();
        }

        public void FifoTest<T>(IEnumerable<T> items)
        {
            var list = items.Aggregate(ImmutableList<T>.Empty, (current, item) => current.Add(item));

            foreach (var expected in items)
            {
                T item;
                list = list.Take(out item);
                Assert.IsTrue(expected.Equals(item), "ImmutableList<T> must implement FIFO behavior");
            }
        }

        [TestMethod]
        public void FifoTest()
        {
            FifoTest(new[]
                         {
                             new GenericParameterHelper(0),
                             new GenericParameterHelper(1),
                             new GenericParameterHelper(2),
                             new GenericParameterHelper(3),
                             new GenericParameterHelper(4)
                         });
        }
        
        public void ImmutabilityTest<T>(T x)
        {
            var list1 = ImmutableList<T>.Empty.Add(x);

            T y;
            list1.Take(out y);
            Assert.AreEqual(x, y, "first take()");

            list1.Take(out y);
            Assert.AreEqual(x, y, "second take()");
        }

        [TestMethod]
        public void ImmutabilityTest()
        {
            ImmutabilityTest(new GenericParameterHelper());
        }
    }
}
