/*********************************************************************
 * CLR Version： 4.0.30319.18444
 * file version：  V1.0.0.0
 * creater：  wangxiaoying
 * email：wangxiaoying_op@163.com
 * create time：2015/4/6 19:16:37
 * description：
 * This demo describe how to use a CountDownEvent signaling construct to 
 * wait until a certain number of operations complete. It is very 
 * convenient to wait for multiple asynchronous operations to complete.
 * **********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CountDownEventDemo
{
    class Program
    {
        static CountdownEvent _countdown = new CountdownEvent(2);
        static void Main(string[] args)
        {
            Console.WriteLine("Starting two operations");
            var t1 = new Thread(() => PerformOperation("Operation 1 is completed", 4));
            var t2 = new Thread(() => PerformOperation("Operation 2 is completed", 8));
            t1.Start();
            t2.Start();
            _countdown.Wait();
            Console.WriteLine("Both operations have been completed.");
            _countdown.Dispose();
        }

        static void PerformOperation(string message, int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine(message);
            _countdown.Signal();
        }
    }
}
