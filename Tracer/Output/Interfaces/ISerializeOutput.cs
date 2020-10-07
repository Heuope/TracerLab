using System;
using System.Collections.Generic;
using System.Text;
using TracerLib;

namespace TracerLab
{
    interface ISerializeOutput
    {
        string Serialize(ITracer tracer);
    }
}
