using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            CoreServer cs = new CoreServer();
            cs.Start().Wait();
            Console.ReadLine();
        }
    }
}
