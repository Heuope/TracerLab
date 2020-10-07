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

            var xml = new XmlSerialize();
            var xmlString = xml.Serialize(tracer);

            var outputToConsole = new OutputToConsole();

            outputToConsole.OutputResult(jsonString);
            outputToConsole.OutputResult(xmlString);

            var fileSaverJson = new OutputToFile("test.json");
            var fileSaverXml = new OutputToFile("test.xml");

            fileSaverJson.OutputResult(jsonString);
            fileSaverXml.OutputResult(xmlString);
        }
    }
}
