namespace TracerLib
{
    public interface ITracer
    {
        void StartTrace();

        void StopTrace();

        ThreadResult[] GetTraceResult();
    }
}
