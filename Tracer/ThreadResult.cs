using System.Collections.Generic;
using System.Threading;

namespace Tracer
{
    class ThreadResult
    {
        public int ThreadId { get; private set; }

        public int Level { get; set; }

        private List<TraceResult> traceResults = new List<TraceResult>();

        private Dictionary<int, TraceResult> traceTable = new Dictionary<int, TraceResult>();

        public ThreadResult()
        {
            ThreadId = Thread.CurrentThread.ManagedThreadId;
            Level = 0;
        }

        public void AddTraceResult(TraceResult traceResult, int level, int hash)
        {
            traceTable.Add(hash, traceResult);
            AddTraceResultRec(traceResult, level);
        }

        public void SetTime(int hash, long time)
        {
            traceTable[hash].Time = time;
        }

        private void AddTraceResultRec(TraceResult traceResult, int level)
        {
            if (level == 0)
            {
                traceResults.Add(traceResult);
            }
            else
            {
                AddTraceResultRec(traceResult, level - 1);
            }
        }
    }
}
