﻿/*********************************************************************
 * CLR Version： 4.0.30319.18444
 * file version：  V1.0.0.0
 * creater：  wangxiaoying
 * email：wangxiaoying_op@163.com
 * create time：2015/4/6 19:06:19
 * description：
 * ManualResetEventSlim is a hybrid construct
 * 
 * **********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManualResetEventSlimDemo
{
    class Program
    {
        static ManualResetEventSlim _mainEvent = new ManualResetEventSlim(false);
        static void Main(string[] args)
        {
            var t1 = new Thread(() => TravelThroughGates("Thread 1", 5));
            var t2 = new Thread(() => TravelThroughGates("Thread 2", 6));
            var t3 = new Thread(() => TravelThroughGates("Thread 3", 12));
            t1.Start();
            t2.Start();
            t3.Start();
            Thread.Sleep(TimeSpan.FromSeconds(6));
            Console.WriteLine("The gates are now open!");
            _mainEvent.Set();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            _mainEvent.Reset();
            Console.WriteLine("The gates have been closed!");
            Thread.Sleep(TimeSpan.FromSeconds(10));
            Console.WriteLine("The gates are now open for the second time!");
            _mainEvent.Set();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("The gates have been closed!");
            _mainEvent.Reset();
        }

        static void TravelThroughGates(string threadName, int seconds)
        {
            Console.WriteLine("{0} falls to sleep",threadName);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine("{0} waits for the gates to open!",threadName);
            _mainEvent.Wait();
            Console.WriteLine("{0} enters the gates!",threadName);
        }
    }
}