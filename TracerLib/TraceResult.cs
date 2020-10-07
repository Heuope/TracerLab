using System;
using System.Collections.Generic;

namespace TracerLib
{
    public class TraceResult
    {
        public long Time { get; set; }

        public string MethodName { get; set; }

        public string ClassMethodName { get; set; }

        public List<TraceResult> Methods = new List<TraceResult>();

        public TraceResult() { }

        public TraceResult(string methodName, string classMethodName)
        {
            ClassMethodName = classMethodName;
            MethodName = methodName;
            Time = 0;
        }
    }
}
