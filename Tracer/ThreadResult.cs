using System.Collections.Generic;
using System.Threading;

namespace Tracer
{
    class ThreadResult
    {
        public int ThreadId { get; private set; }

        public List<TraceResult> MethodList = new List<TraceResult>();

        private Stack<TraceResult> methodStack = new Stack<TraceResult>();

        public ThreadResult()
        {
            ThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        public void AddTraceResult(TraceResult traceResult)
        {
            if(methodStack.Count == 0)
            {
                MethodList.Add(traceResult);
            }
            else
            {
                methodStack.Peek().TracerResults.Add(traceResult);
            }
            methodStack.Push(traceResult);
        }

        public void SetTime(long time)
        {
            TraceResult t = methodStack.Pop();
            t.Time = time;
        }
    }
}
