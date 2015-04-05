using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutexDemo
{
    class Program
    {
        //Mutex对象可用于在多个进程之间进行同步
        private const string MUTEX_NAME = "MYMUTEX";
        static void Main(string[] args)
        {
            //mutex第一个参数传递false，允许获取已创建的mutex对象
            //mutex对象是一个全局的操作系统对象，使用完毕后切记释放
            using (var mutext = new Mutex(false, MUTEX_NAME))
            {
                if (!mutext.WaitOne(TimeSpan.FromSeconds(5), false))
                {
                    Console.WriteLine("There is already a instance running");
                }
                else
                {
                    Console.WriteLine("Running");
                    Console.Read();
                    mutext.ReleaseMutex();
                }
            }
        }
    }
}
