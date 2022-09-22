using MomoEngine.Core;
using System.Collections;

namespace MomoEngineConsole
{
    public class EngineTest : Behaviour
    {
        private static void Awake()
        {
            Console.WriteLine("EngineTest Awake");
        }

        public void Start()
        {
            Console.WriteLine("EngineTest Start");
            StartCoroutine(Thread());
            Invoke("Test", 5f);
            Task.Run(() =>
            {
                DateTime current = DateTime.Now;
                while (current.AddMilliseconds(2 * 1000) > DateTime.Now) { }
                StopCoroutine(Thread());
            });
            //CancelInvoke("Test");
        }

        IEnumerator Thread()
        {
            yield return new WaitForSeconds(3.0f);
            Console.WriteLine("Thread Start");
            Console.WriteLine("Thread End");
        }

        public void Test()
        {
            Console.WriteLine("Test Start");
        }

        public void FixedUpdate()
        {
            //Console.WriteLine("EngineTest FixedUpdate");
        }


        public void Update()
        {
            //Console.WriteLine($"{i}EngineTest Update");
        }

        public void LateUpdate()
        {
            //Console.WriteLine("EngineTest LateUpdate");
        }

        public void OnDestory()
        {
            Console.WriteLine("EngineTest OnDestory");
        }

        public void OnApplicationQuit()
        {
            Console.WriteLine("EngineTest OnApplicationQuit");
        }
    }
}
