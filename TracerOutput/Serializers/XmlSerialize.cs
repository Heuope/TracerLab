using TracerLib;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace TracerOutput
{
    class XmlSerialize : ISerializeOutput
    {
        public string Serialize(ITracer tracer)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ThreadResult[]));

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, tracer.GetTraceResult());
                return textWriter.ToString();
            }
        }
    }
}
