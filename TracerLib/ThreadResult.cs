using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TracerLib
{
    public class ThreadResult
    {
        public int ThreadId { get; set; }

        public long ElapsedTime { get; set; }

        public List<TraceResult> MethodList { get; private set; }

        private Stack<TraceResult> methodStack = new Stack<TraceResult>();

        public ThreadResult()
        {
            MethodList = new List<TraceResult>();
            ThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        public void CalculatedElapsedTime()
        {
            ElapsedTime = MethodList.Sum(method => method.Time);
        }

        public void AddTraceResult(TraceResult traceResult)
        {
            if(methodStack.Count == 0)
            {
                MethodList.Add(traceResult);
            }
            else
            {
                methodStack.Peek().Methods.Add(traceResult);
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
