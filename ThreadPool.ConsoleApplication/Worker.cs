using System;

namespace ThreadPool.ConsoleApplication
{
    public static class Worker
    {
        public static void DoWork()
        {
            for (var i = 0; i < 10; i++)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(Work, i); // use ThreadPool instance of create new Thread() by yourself
                System.Threading.Thread.Sleep(200);
            }
        }

        private static void Work(object i) =>
            Console.WriteLine($"Thread id {System.Threading.Thread.CurrentThread.ManagedThreadId} and parameter {i}");
    }
}
