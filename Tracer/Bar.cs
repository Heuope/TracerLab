using System;
using System.Threading;
using TracerLib;

namespace TracerLab
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
                Console.WriteLine($"{item.ThreadId}");
                foreach (var method in item.MethodList)
                {
                    Console.WriteLine($"{method.ClassMethodName} {method.MethodName} {method.Time}");
                }
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
