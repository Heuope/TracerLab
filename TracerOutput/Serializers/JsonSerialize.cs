using TracerLib;
using Newtonsoft.Json;

namespace TracerOutput
{
    class JsonSerialize : ISerializeOutput
    {
        public string Serialize(ITracer tracer)
        {
            return JsonConvert.SerializeObject(tracer.GetTraceResult(), Formatting.Indented);
        }
    }
}
