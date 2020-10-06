using System;

namespace TracerOutput
{
    class OutputToConsole : IOutput
    {
        public void OutputResult(string result)
        {
            Console.WriteLine(result);
        }
    }
}