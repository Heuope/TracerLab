using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLab
{
    public class OutputToFile : IOutput
    {
        private string savePath;

        public OutputToFile(string filePath)
        {
            savePath = filePath;
        }

        public void OutputResult(string result)
        {
            System.IO.File.WriteAllText(savePath, result);
        }
    }
}
