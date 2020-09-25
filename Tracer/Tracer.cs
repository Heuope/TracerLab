using System.Diagnostics;
using System.Collections.Concurrent;
using System.Reflection;

namespace Tracer
{
    class Tracer : ITracer
    {
        private Stopwatch stopwatch;

        private ConcurrentDictionary<int, Stopwatch> stopwatches = new ConcurrentDictionary<int, Stopwatch>();
        private ConcurrentQueue<TraceResults> traceQueue = new ConcurrentQueue<TraceResults>();

        /*
         * ThreadDict <- TraceResultQueue
         * 
         * 
         */


        public TraceResults[] GetTraceResult()
        {
            return traceQueue.ToArray();
        }

        public void StartTrace()
        {
            var st = new StackTrace();
            var hash = st.GetFrame(1).GetMethod().GetHashCode();

            stopwatch = new Stopwatch();

            stopwatches.TryAdd(hash, stopwatch);

            stopwatch.Start();       
        }

        public void StopTrace()
        {
            var st = new StackTrace();
            var hash = st.GetFrame(1).GetMethod().GetHashCode();

            var stopwatch = new Stopwatch();
            stopwatches.TryGetValue(hash, out stopwatch);

            stopwatch.Stop();

            MethodBase tracedMethod = st.GetFrame(1).GetMethod();

            var temp = new TraceResults(tracedMethod.Name, tracedMethod.DeclaringType.Name, stopwatch.ElapsedMilliseconds);

            traceQueue.Enqueue(temp);

            //stopwatches.TryRemove(hash, out stopwatch);
        }
    }
}
