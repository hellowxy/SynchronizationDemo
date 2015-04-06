/*********************************************************************
 * CLR Version： 4.0.30319.18444
 * file version：  V1.0.0.0
 * creater：  wangxiaoying
 * email：wangxiaoying_op@163.com
 * create time：2015/4/6 18:50:50
 * description：
 * AutoResetEvent notifies a waiting thread that an event has occurred.
 * Any thread calling the WaitOne method of one of AutoResetEvent will be
 * blocked until we call the Set method. If we initialize the event state to 
 * true, it becomes signaled and the first thread calling WaitOne would
 * proceed immediately. Then event state becomes unsignaled automatically.
 * AutoResetEvent is a kernel-time construct.
 * **********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoResetEventDemo
{
    class Program
    {
        private static AutoResetEvent _workerEvent = new AutoResetEvent(false);
        private static AutoResetEvent _mainEvent = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            var t = new Thread(() => Process(10));
            t.Start();
            Console.WriteLine("Waiting for another thread to complete work");
            _workerEvent.WaitOne();
            Console.WriteLine("First operation is completed");
            Console.WriteLine("Performing an operation on a mainthread");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            _mainEvent.Set();
            Console.WriteLine("now running the second operation on a second thread");
            _workerEvent.WaitOne();
            Console.WriteLine("second operation is completed!");
        }

        static void Process(int seconds)
        {
            Console.WriteLine("Starting a long running work...");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine("work is done!");
            _workerEvent.Set();
            Console.WriteLine("Waiting for a main thread to complete its work");
            _mainEvent.WaitOne();
            Console.WriteLine("Starting second operation...");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine("Work is done!");
            _workerEvent.Set();
        }
    }
}
