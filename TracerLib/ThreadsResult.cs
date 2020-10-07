using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib
{
    public class ThreadsResult
    {
        public ThreadResult[] ThreadResults { get; set; }

        public ThreadsResult() 
        {
            
        }

        public ThreadsResult(ThreadResult[] threadResults)
        {
            this.ThreadResults = threadResults;
        }
    }
}
