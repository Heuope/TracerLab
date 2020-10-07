using TracerLib;
using Newtonsoft.Json;

namespace TracerLab
{
    public class JsonSerialize : ISerializeOutput
    {
        public string Serialize(ITracer tracer)
        {
            return JsonConvert.SerializeObject(tracer.GetTraceResult(), Formatting.Indented);
        }
    }
}
