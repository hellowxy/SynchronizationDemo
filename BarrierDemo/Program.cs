/*********************************************************************
 * CLR Version： 4.0.30319.18444
 * file version：  V1.0.0.0
 * creater：  wangxiaoying
 * email：wangxiaoying_op@163.com
 * create time：2015/4/6 19:31:20
 * description：
 * The Barrier construct helps to organize several threads to meet at 
 * some point in time, provding a callback that will be executed each
 * time the threads have called the SignalAndWait method. 
 * We create a Barrier construct, specifying that we want to synchronize two
 * threads, and after each of those two threads have called the _barrier.SignalAndWait
 * method, we need to execute a callback that will print out the number of phases completed.
 * It is useful for working with multithreaded iteration algorithms, to execute some 
 * calculations on each iteration end.
 * **********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarrierDemo
{
    class Program
    {
        private static Barrier _barrier = new Barrier(2,
            x => Console.WriteLine("End of phase {0}", x.CurrentPhaseNumber + 1));
        static void Main(string[] args)
        {
            var t1 = new Thread(() => PlayMusic("the guitarist", "play an amazing solo", 5));
            var t2 = new Thread(() => PlayMusic("the singer", "sing his song", 2));
            t1.Start();
            t2.Start();
            Console.Read();
        }

        static void PlayMusic(string name, string message, int seconds)
        {
            for (int i = 1; i < 3; i++)
            {
                Console.WriteLine("---------------------------------------------");
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
                Console.WriteLine("{0} starts to  {1}",name,message);
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
                Console.WriteLine("{0} finishes to {1}",name,message);
                Console.WriteLine("{0} signals to barrier",name);
                _barrier.SignalAndWait();
            }
        }
    }
}
