using MomoEngine.Core;

namespace MomoEngineConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MomoEngine Start!");
            Engine engine = new Engine();
            engine.Run();
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(10 * 1000) > DateTime.Now)
            {
            }
            engine.RemoveBehaviour(typeof(EngineTest));
            //engine.RemoveBehaviour("EngineTest");
            //EngineTest engineTest = new EngineTest();
            //Test test = new Test();
            while (Console.ReadLine() != "close") { }
            engine.ApplicationQuit();
            Console.WriteLine("MomoEngine Close!");
        }
    }
}