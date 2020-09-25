using System.Diagnostics;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;

namespace Tracer
{
    class Tracer : ITracer
    {
        private Stopwatch stopwatch;

        private ConcurrentDictionary<int, Stopwatch> stopwatches = new ConcurrentDictionary<int, Stopwatch>();
        private ConcurrentDictionary<int, ThreadResult> threadResults = new ConcurrentDictionary<int, ThreadResult>();

        public TraceResult[] GetTraceResult()
        {
            return null;
        }

        public void StartTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            if (!threadResults.ContainsKey(threadId))
            {
                var threadResult = new ThreadResult();
                threadResults.TryAdd(threadId, threadResult);
            }

            var st = new StackTrace();
            MethodBase tracedMethod = st.GetFrame(1).GetMethod();            

            var temp = new TraceResult(tracedMethod.Name, tracedMethod.DeclaringType.Name);
            int hash = tracedMethod.GetHashCode();

            threadResults[threadId].AddTraceResult(temp, threadResults[threadId].Level, hash);
            threadResults[threadId].Level++;

            stopwatch = new Stopwatch();
            stopwatches.TryAdd(hash, stopwatch);
            stopwatch.Start();       
        }

        public void StopTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            var st = new StackTrace();
            MethodBase tracedMethod = st.GetFrame(1).GetMethod();

            int hash = tracedMethod.GetHashCode();

            var stopwatch = new Stopwatch();
            stopwatches.TryGetValue(hash, out stopwatch);

            stopwatch.Stop();

            threadResults[threadId].SetTime(hash, stopwatch.ElapsedMilliseconds);
            threadResults[threadId].Level--;

            //stopwatches.TryRemove(hash, out stopwatch);
        }
    }
}
