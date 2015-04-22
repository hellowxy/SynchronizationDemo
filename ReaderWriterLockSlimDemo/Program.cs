/*********************************************************************
 * CLR Version： 4.0.30319.18444
 * file version：  V1.0.0.0
 * creater：  wangxiaoying
 * email：wangxiaoying_op@163.com
 * create time：2015/4/6 19:52:43
 * description：
 * ReaderWriterLockSlim represents a lock that is used to manage access to a 
 * resource, allowing multiple threads for reading or exclusive access for writing.
 * It has two kinds of locks: a read lock that allows multiple threads reading and a 
 * write lock that blocks every operation from other threads until this write lock
 * is released. There is also an interesting scenario when we obtain a read lock, read
 * some data from the collection, and depending on that data, decide to obtain a write
 * lock and change the collection. If we get the write locks at once, too much time is
 * spent not allowing our readers to read the data, to minimize this time, there are 
 * EnterUpgradeableReadLock/ExitUpgradeableReadLock methods.
 * 
 * **********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReaderWriterLockSlimDemo
{
    class Program
    {
        static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();
        static Dictionary<int, int> _items = new Dictionary<int, int>();
        static void Main(string[] args)
        {
            new Thread(Read) { IsBackground = true }.Start();
            new Thread(Read) { IsBackground = true }.Start();
            new Thread(Read) { IsBackground = true }.Start();
            new Thread(() => Write("Thread 1")) { IsBackground = true }.Start();
            new Thread(() => Write("Thread 2")) { IsBackground = true }.Start();
            Thread.Sleep(TimeSpan.FromSeconds(30));
        }

        static void Read()
        {
            Console.WriteLine("Reading contents of a dictionary");
            while (true)
            {
                try
                {
                    _rw.EnterReadLock();
                    foreach (var item in _items)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(0.1));
                    }
                }
                finally
                {
                    _rw.ExitReadLock();
                }
            }
        }

        static void Write(string threadName)
        {
            while (true)
            {
                try
                {
                    int newKey = new Random().Next(250);
                    _rw.EnterUpgradeableReadLock();
                    if (!_items.ContainsKey(newKey))
                    {
                        try
                        {
                            _rw.EnterWriteLock();
                            _items[newKey] = 1;
                            Console.WriteLine("New key {0} is added to a dictionary by a {1}", newKey, threadName);
                        }
                        finally
                        {
                            _rw.ExitWriteLock();
                        }
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(0.1));
                }
                finally
                {
                    _rw.ExitUpgradeableReadLock();
                }
            }
        }
    }
}
