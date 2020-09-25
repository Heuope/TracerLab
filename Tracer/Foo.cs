using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Tracer
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

            _tracer.StopTrace();
        }
    }
}
