/*********************************************************************
 * CLR Version： 4.0.30319.18444
 * file version：  V1.0.0.0
 * creater：  wangxiaoying
 * email：wangxiaoying_op@163.com
 * create time：2015/4/6 18:36:15
 * description：
 * SemaphoreSlim is a light weight version of Semaphore, it limits the number
 * of threads that can access a resource concurrently.
 * Here we use a hybrid construct, which allows us to save a context switch
 * in cases where the wait time is less. Semaphore is a pure, kernel-time 
 * construct. We can create a named semaphore like a named mutex and use it
 * to synchronize threads in different programs. SemaphoreSlim does not use
 * windows kernel semaphores and does not support interprocess synchronization.
 * **********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphoreSlimDemo
{
    class Program
    {
        static SemaphoreSlim _semaphore = new SemaphoreSlim(4);
        static void Main(string[] args)
        {
            for (int i = 0; i <= 6; i++)
            {
                var threadName = "Thread" + i;
                var secondsToWait = 2 + 2*i;
                var t = new Thread(() => AccessDatabase(threadName, secondsToWait));
                t.Start();
            }
        }

        static void AccessDatabase(string name, int seconds)
        {
            Console.WriteLine("{0} waits to access a database",name);
            _semaphore.Wait();
            Console.WriteLine("{0} was granted an access to a database",name);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine("{0} is completed",name);
            _semaphore.Release();
        }
    }
}
