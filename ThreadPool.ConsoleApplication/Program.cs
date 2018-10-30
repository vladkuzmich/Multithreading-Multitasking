using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadPool.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker.DoWork();
            Console.ReadLine();
        }
    }
}
