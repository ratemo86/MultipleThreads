using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MultipleThreads
{
    class Program
    {
        delegate void ThreadDelegate(Thread thread);
 
        //Shared by threads created by the main thread
        static int c = 0;
        private static void DisplayThreadId(Thread thread)

        {
            thread.Start();
            
            Console.WriteLine("Thread number: " + thread.ManagedThreadId + " has started.");
            thread.Abort();
           

        }
        private static void MyCallBack(IAsyncResult asychResult)
        {

            ThreadDelegate t = ((ThreadDelegate)asychResult.AsyncState);
            t.EndInvoke(asychResult);
            asychResult.AsyncWaitHandle.Close();
          

            Interlocked.Decrement(ref c);
        }
        
        static void Main(string[] args)
        {
           
            for (int i = 0; i < 10; i++)
            {
                ThreadDelegate myDelegate = DisplayThreadId;
                // ThreadDelegate myDel = DisplayThreadId(String threadId); 
                Interlocked.Increment(ref c);
                Thread thread = new Thread(() => { });
                Console.WriteLine("Created thread number  " + c);

                IAsyncResult res = myDelegate.BeginInvoke(thread, new AsyncCallback(MyCallBack), myDelegate);
                
                
                // 
            }
            while (c > 0)
            {
                Thread.Sleep(1000);
            }
            Thread.CurrentThread.Join();
            Console.ReadKey();
        }
    }
}
