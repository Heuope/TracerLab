﻿using System;

namespace Tracer
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