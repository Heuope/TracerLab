using System.Collections.Generic;

namespace Tracer
{
    class TraceResult
    {
        public long Time { get; set; }

        public string MethodName { get; private set; }

        public string ClassMethodName { get; private set; }

        public List<TraceResult> TracerResults = new List<TraceResult>();

        public TraceResult(string methodName, string classMethodName)
        {
            ClassMethodName = classMethodName;
            MethodName = methodName;
            Time = 0;
        }
    }
}
