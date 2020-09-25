using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Tracer
{
    class Bar
    {
        private Foo _foo;
        private ITracer _tracer;

        internal Bar(ITracer tracer)
        {
            _tracer = tracer;
            _foo = new Foo(_tracer);
        }

        public void MyMehtod()
        {
            _foo.Method();

            M1();
            M2();

            foreach (var item in _tracer.GetTraceResult())
            {
                Console.WriteLine($"{item.ClassMethodName} {item.MethodName} {item.Time}");
            }
        }

        private void M1()
        {
            _tracer.StartTrace();

            Thread.Sleep(130);

            _tracer.StopTrace();
        }

        private void M2()
        {
            _tracer.StartTrace();

            Thread.Sleep(12);

            _tracer.StopTrace();
        }
    }
}
