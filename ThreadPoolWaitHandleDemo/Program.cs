using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolWaitHandleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            RunOpeartions(TimeSpan.FromSeconds(5));
            RunOpeartions(TimeSpan.FromSeconds(7));
            Console.ReadLine();
        }

        static void RunOpeartions(TimeSpan workerOperationTimeout)
        {
            using (var evt = new ManualResetEvent(false))
            using (var cts = new CancellationTokenSource())
            {
                Console.WriteLine("Registering timeout operations...");
                //This method allows us to queue a callback on a thread pool, and this callback will be executed when
                //the provided wait handle is singaled or a timeout has occurred. This allows us to implement a timeout
                //for thread pool operations.
                var worker = ThreadPool.RegisterWaitForSingleObject(evt,
                    (state, isTimedOut) => WorkerOperationWait(cts, isTimedOut), null, workerOperationTimeout, true);
                Console.WriteLine("Starting long running operation...");
                ThreadPool.QueueUserWorkItem(_ => WorkerOperation(cts.Token, evt));
                Thread.Sleep(workerOperationTimeout.Add(TimeSpan.FromSeconds(2)));
                worker.Unregister(evt);
            }
        }

        static void WorkerOperation(CancellationToken token, ManualResetEvent evt)
        {
            for (int i = 0; i < 6; i++)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                Thread.Sleep(1000);
            }
            evt.Set();
        }

        static void WorkerOperationWait(CancellationTokenSource cts, bool isTimedOut)
        {
            if (isTimedOut)
            {
                cts.Cancel();
                Console.WriteLine("Worker operation timed out and was canceled");
            }
            else
            {
                Console.WriteLine("Worker operation succeded");
            }
        }
    }
}
