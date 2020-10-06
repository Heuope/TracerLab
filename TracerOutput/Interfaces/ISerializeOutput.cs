using System;
using System.Collections.Generic;
using System.Text;
using TracerLib;

namespace TracerOutput
{
    interface ISerializeOutput
    {
        string Serialize(ITracer tracer);
    }
}
