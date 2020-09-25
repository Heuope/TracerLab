namespace Tracer
{
    interface ITracer
    {
        void StartTrace();

        void StopTrace();

        ThreadResult[] GetTraceResult();
    }
}
