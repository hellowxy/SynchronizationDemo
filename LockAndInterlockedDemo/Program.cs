using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LockAndInterlockedDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Incorrect counter");
            var sw = new Stopwatch();
            var c = new Counter();
            var t1 = new Thread(() => TestCounter(c));
            var t2 = new Thread(() => TestCounter(c));
            var t3 = new Thread(() => TestCounter(c));
            sw.Start();
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();
            sw.Stop();
            Console.WriteLine("Total count:{0}, time {1}",c.Count,sw.Elapsed);
            Console.WriteLine("-------------------------");
            Console.WriteLine("Correct counter with no lock");
            sw.Reset();
            var c1 = new CounterNoLock();
            t1 = new Thread(() => TestCounter(c1));
            t2 = new Thread(() => TestCounter(c1));
            t3 = new Thread(() => TestCounter(c1));
            sw.Start();
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();
            sw.Stop();
            Console.WriteLine("Total count:{0}, time {1}", c1.Count,sw.Elapsed);
            Console.WriteLine("-------------------------");
            Console.WriteLine("Correct counter with lock");
            sw.Reset();
            var c2 = new CounterWithLock();
            t1 = new Thread(() => TestCounter(c2));
            t2 = new Thread(() => TestCounter(c2));
            t3 = new Thread(() => TestCounter(c2));
            sw.Start();
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();
            sw.Stop();
            Console.WriteLine("Total count:{0}, time {1}", c2.Count,sw.Elapsed);

            Console.Read();
        }

        static void TestCounter(CounterBase c)
        {
            for (int i = 0; i < 100000; i++)
            {
                c.Increment();
                c.Decrement();
            }
        }
    }
}
