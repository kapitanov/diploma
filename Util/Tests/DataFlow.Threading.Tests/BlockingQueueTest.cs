using System.Threading;
using AISTek.DataFlow.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataFlow.Threading.Tests
{
    /// <summary>
    /// This is a test class for BlockingQueueTest and is intended
    /// to contain all BlockingQueueTest Unit Tests
    /// </summary>
    [TestClass]
    public class BlockingQueueTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void EnqueueBeforeDequeueTest()
        {
            var queue = new BlockingQueue<object>();
            var isEnqueued = new ManualResetEvent(false);
            var isDequeued = new ManualResetEvent(false);

            object value = null;

            ThreadPool.QueueUserWorkItem(_ =>
            {
                queue.Enqueue(new object());
                isEnqueued.Set();
            });
            ThreadPool.QueueUserWorkItem(_ =>
            {
                isEnqueued.WaitOne();
                value = queue.Dequeue();
                isDequeued.Set();
            });

            if (!isDequeued.WaitOne(10))
                Assert.Fail("Dequeue after Enqueue failed: Event hasn't been raised");

            if(value == null)
                Assert.Fail("Dequeue after Enqueue failed: Wrong value returned");
        }

        [TestMethod]
        public void EnqueueAfterDequeueTest()
        {
            var queue = new BlockingQueue<object>();
            var isEnqueued = new ManualResetEvent(false);
            var isDequeued = new ManualResetEvent(false);

            object value = null;

            ThreadPool.QueueUserWorkItem(_ =>
            {
                Thread.Sleep(100);
                queue.Enqueue(new object());
                isEnqueued.Set();
            });
            ThreadPool.QueueUserWorkItem(_ =>
            {
                value = queue.Dequeue();
                isDequeued.Set();
            });

            isEnqueued.WaitOne();
            if (!isDequeued.WaitOne(10))
                Assert.Fail("Dequeue before Enqueue failed: Event hasn't been raised");

            if (value == null)
                Assert.Fail("Dequeue after before failed: Wrong value returned");
        }
    }
}
