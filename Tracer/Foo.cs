using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TracerLib;

namespace TracerLab
{
    class Foo
    {
        private ITracer _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void Method()
        {
            _tracer.StartTrace();

            Thread.Sleep(156);
            M3();

            _tracer.StopTrace();
        }

        private void M3()
        {
            _tracer.StartTrace();

            Thread.Sleep(13);

            _tracer.StopTrace();
        }
    }
}
