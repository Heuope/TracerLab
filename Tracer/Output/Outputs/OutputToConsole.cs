using System;

namespace TracerLab
{
    public class OutputToConsole : IOutput
    {
        public void OutputResult(string result)
        {
            Console.WriteLine(result);
        }
    }
}