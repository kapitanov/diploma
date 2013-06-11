using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AISTek.UnitTestUtils
{
    public static class Test
    {
        [DebuggerStepThrough]
        public static void Throws<T>(this Action action)
            where T : Exception
        {
            Throws<T>(action, string.Empty);
        }

        [DebuggerStepThrough]
        public static void Throws<T>(this Action action, string message)
            where T : Exception
        {
            try
            {
                action();
            }
            catch (T)
            {
                return;
            }
            catch(Exception e)
            {
                Assert.Fail(string.Format("Failed: {0} \rExpected exception: {1} \rActual exception: {2}", message, typeof(T), e.GetType()));
            }

            Assert.Fail(string.Format("Failed: {0} \rExpected exception: {1} \rActual exception: none", message, typeof(T)));
        }

        [DebuggerStepThrough]
        public static void Success(this Action action, string message)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("Failed: {0} \rException: {1}", message, e.GetType()));
            }

            return;
        }
    }
}