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
            M3();
        }

        private void M1()
        {
            _tracer.StartTrace();

            Thread.Sleep(20);

            _tracer.StopTrace();
        }

        private void M2()
        {
            _tracer.StartTrace();

            Thread.Sleep(20);

            _tracer.StopTrace();
        }

        private void M3()
        {
            _tracer.StartTrace();

            Thread.Sleep(20);

            _tracer.StopTrace();
        }
    }
}
