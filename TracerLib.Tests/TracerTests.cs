using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace TracerLib.Tests
{
    [TestClass]
    public class TracerTests
    {
        public static ITracer tracer;

        private static readonly int sleepTime = 100;

        [TestInitialize]
        public void TestInitialize()
        {
            tracer = new Tracer();
        }

        public static class OneMethod
        {
            public static void SimpleCall()
            {
                tracer.StartTrace();

                Thread.Sleep(sleepTime);

                tracer.StopTrace();
            }
        }

        public static class OneRecMethod
        { 
            public static void Method(int i)
            {
                tracer.StartTrace();
                if (i == 2)
                {
                    tracer.StopTrace();
                }
                else
                {
                    Thread.Sleep(sleepTime);
                    Method(i + 1);
                    tracer.StopTrace();
                }
            }
        }

        public static class SequenceMethods
        {
            public static void SequenceCall()
            {
                Method1();
                Method2();
                Method3();
            }

            private static void Method1()
            {
                tracer.StartTrace();

                Thread.Sleep(sleepTime);

                tracer.StopTrace();
            }

            private static void Method2()
            {
                tracer.StartTrace();

                Thread.Sleep(sleepTime);

                tracer.StopTrace();
            }

            private static void Method3()
            {
                tracer.StartTrace();

                Thread.Sleep(sleepTime);

                tracer.StopTrace();
            }
        }

        public static class MethodInThread
        { 
            public static void MainMethod()
            {
                OneMethod.SimpleCall();

                var thread = new Thread(() => ThreadMethod());
                thread.Start();
                thread.Join();
            }

            public static void ThreadMethod()
            {
                tracer.StartTrace();

                Thread.Sleep(sleepTime + sleepTime);

                tracer.StopTrace();
            }
        }

        [TestMethod]
        public void ComparingMethodName()
        {
            OneMethod.SimpleCall();
            string expected = "SimpleCall";
            string actual = tracer.GetTraceResult().ThreadResults[0].MethodList[0].MethodName;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ComparingMethodClassName()
        {
            OneMethod.SimpleCall();
            string expected = "OneMethod";
            string actual = tracer.GetTraceResult().ThreadResults[0].MethodList[0].ClassMethodName;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ComparingMethodTime()
        {
            OneMethod.SimpleCall();
            long expected = sleepTime;
            long actual = tracer.GetTraceResult().ThreadResults[0].MethodList[0].Time;
            Assert.AreEqual(true, actual >= expected);
        }

        [TestMethod]
        public void ComparingMethodsNamesInSequenceCall()
        {
            SequenceMethods.SequenceCall();
            string expected = "Method1Method2Method3";
            string actual = "";
            foreach (var item in tracer.GetTraceResult().ThreadResults[0].MethodList)
            {
                actual += item.MethodName;
            }
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ComparingMethodsTimesInSequenceCall()
        {
            SequenceMethods.SequenceCall();
            long expected = 3 * sleepTime;
            long actual = tracer.GetTraceResult().ThreadResults[0].ElapsedTime;
            Assert.AreEqual(true, actual >= expected);
        }

        [TestMethod]
        public void ComparingMethodsNamesInRecursion()
        {
            OneRecMethod.Method(2);
            string expected = "Method";
            string actual = "";
            foreach (var item in tracer.GetTraceResult().ThreadResults[0].MethodList)
            {
                actual += item.MethodName;
            }
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ComparingMethodsNamesInThread()
        {
            MethodInThread.MainMethod();
            string expected = "SimpleCallThreadMethod";
            string actual = "";
            for (int i = 0; i < tracer.GetTraceResult().ThreadResults.Length; i++)
            {
                for (int j = 0; j < tracer.GetTraceResult().ThreadResults[i].MethodList.Count; j++)
                    actual += tracer.GetTraceResult().ThreadResults[i].MethodList[j].MethodName;
            }
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ComparingCountOfThreads()
        {
            MethodInThread.MainMethod();
            int expected = 2;
            int actual = tracer.GetTraceResult().ThreadResults.Length;
            Assert.AreEqual(expected, actual);
        }
    }
}
