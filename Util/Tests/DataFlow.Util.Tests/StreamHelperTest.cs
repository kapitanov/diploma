using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AISTek.DataFlow.Util.Tests
{
    /// <summary>
    /// This is a test class for StreamHelperTest and is intended
    /// to contain all StreamHelperTest Unit Tests
    ///</summary>
    [TestClass]
    public class StreamHelperTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// A test for ReadToBuffer
        /// </summary>
        [TestMethod]
        public void ReadToBufferTest()
        {
            const string testString = "test string 123";
            var encoding = Encoding.Unicode;
            Stream stream = new MemoryStream(encoding.GetBytes(testString));
            var actual = encoding.GetString(StreamHelper.ReadToBuffer(stream));
            Assert.AreEqual(testString, actual);
        }
    }
}


