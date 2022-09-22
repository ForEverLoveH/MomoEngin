using MomoEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomoEngineConsole
{
    public class Test : Behaviour
    {
        public void Awake()
        {
            Console.WriteLine("Test Awake");
        }


        public void Start()
        {
            Console.WriteLine("Test Start");
        }
    }
}
