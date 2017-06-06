using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Factory.StartNew(()=> {
                Thread.Sleep(200);
                Console.WriteLine(1);
            }));
            tasks.Add(Task.Factory.StartNew(() => {
                Thread.Sleep(300);
                Console.WriteLine(2);
            }));
            tasks.Add(Task.Factory.StartNew(() => {
                Thread.Sleep(400);
                Console.WriteLine(3);
            }));
            Task.Factory.ContinueWhenAny(tasks.ToArray(),(x)=> {
                Console.WriteLine("Hello World!");
            });
            Console.ReadLine();
            Console.WriteLine("Hello World!");
        }
    }
}