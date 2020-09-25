namespace Tracer
{
    class TraceResults
    {


        public long Time { get; private set; }

        public string MethodName { get; private set; }

        public string ClassMethodName { get; private set; }

        public TraceResults(string methodName, string classMethodName, long time)
        {
            ClassMethodName = classMethodName;
            MethodName = methodName;
            Time = time;
        }
    }
}
