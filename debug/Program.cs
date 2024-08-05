using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace debug
{
    #region  条件断点

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        var text = "Hello,world!";
    //        int sum = 0;
    //        for (int i = 0; i < text.Length; i++)
    //        {
    //            int c = text[i];
    //            sum += c;
    //            Foo(c);

    //        }
    //        Console.WriteLine($"Sum:{sum}");
    //        Foo(text);
    //    }
    //    static void Foo<T>(T obj)
    //    {
    //        Console.WriteLine($"Type:{typeof(T).Name},Value:{obj}");
    //    }

    //}
    #endregion

    #region 函数断点
    class Program
    {
        
        static void Main(string[] args)
        {
            #region 跳转与快进
            Console.WriteLine("第一行");
            Console.WriteLine("第二行");
            Console.WriteLine("第三行");
            Console.WriteLine("第四行");
            Console.WriteLine("第五行");
           
        #endregion
            var th1 = new Thread(() =>
              {
                  for (int i = 0; i < 100; i++)
                  {
                      Thread.Sleep(250);
                      if (i == 40)
                      {
                          var demo = new Demo();
                          demo.Foo();
                      }
                      Console.WriteLine($"Thread{Environment.CurrentManagedThreadId}:{i}");
                  }
              });

            var th2 = new Thread(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(250);
                    if (i == 30)
                    {
                        var demo = new Demo();
                        demo.Foo();
                    }
                    Console.WriteLine($"Thread{Environment.CurrentManagedThreadId}:{i}");
                }
            });

            th1.Start();
            th2.Start();

            th1.Join();
            th2.Join();

        }

        static void Foo()
        {
            Console.WriteLine($"Threa{Environment.CurrentManagedThreadId} called Foo");
        }

        class Demo
        {
            public void Foo()
            {
                Console.WriteLine($"Theard{Environment.CurrentManagedThreadId} called in Demo");
            }
        }
    }
    #endregion
}
