using System.Threading;
using TracerLib;
using TracerLab;

namespace TracerLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var tracer = new Tracer();

            var test = new Bar(tracer);

            test.MyMehtod();

            var thread = new Thread(() => test.MyMehtod());
            thread.Start();
            thread.Join();

            var json = new JsonSerialize();
            var jsonString = json.Serialize(tracer);

            var fileSaver = new OutputToFile("test.json");
            fileSaver.OutputResult(jsonString);
        }
    }
}
