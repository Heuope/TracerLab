using System.Diagnostics;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;
using System.Collections.Generic;

namespace TracerLib
{
    public class Tracer : ITracer
    {
        private ConcurrentDictionary<int, Stopwatch> stopwatches = new ConcurrentDictionary<int, Stopwatch>();
        private ConcurrentDictionary<int, ThreadResult> threadResults = new ConcurrentDictionary<int, ThreadResult>();

        public ThreadResult[] GetTraceResult()
        {
            var temp = new List<ThreadResult>();

            foreach (var item in threadResults.Values)
            {
                temp.Add(item);
            }

            return temp.ToArray();
        }

        public void StartTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            if (!threadResults.ContainsKey(threadId))
            {
                var threadResult = new ThreadResult();
                threadResults.TryAdd(threadId, threadResult);
            }

            var stackTrace = new StackTrace();
            MethodBase tracedMethod = stackTrace.GetFrame(1).GetMethod();            

            var temp = new TraceResult(tracedMethod.Name, tracedMethod.DeclaringType.Name);

            threadResults[threadId].AddTraceResult(temp);

            var stopwatch = new Stopwatch();

            stopwatches.TryAdd(tracedMethod.GetHashCode(), stopwatch);

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

            threadResults[threadId].SetTime(stopwatch.ElapsedMilliseconds);

            //stopwatches.TryRemove(hash, out _);
        }
    }
}
