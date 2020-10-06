using System;
using System.Collections.Generic;
using System.Text;
using TracerLib;

namespace TracerLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Bar(new Tracer());

            test.MyMehtod();
        }
    }
}
