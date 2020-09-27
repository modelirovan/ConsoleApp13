using System;
using System.Threading.Tasks;
using static ConsoleApp13.Test;

namespace ConsoleApp13
{
    class Program
    {

        static void Main(string[] args)
        {
            Test test = new Test();
            var summa2Awaiter = WaitInput(test).GetAwaiter();
            summa2Awaiter.OnCompleted(() => Console.WriteLine(summa2Awaiter.GetResult()));
            test.Message();
            test.Message();
            // test.Notify += Display;

            //  test.WaitInput();


            Console.WriteLine("Hello World!");
            Console.Read();
        }

        public async static Task<string> WaitInput(Test test)
        {
            var tcs = new TaskCompletionSource<string>();
            AccountHandler handler = (s) => tcs.SetResult(s);
            test.Notify += handler;
            try
            {
                return await tcs.Task;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                test.Notify -= handler;
            }
          //  return tcs.Task;
        }

    }
    class Test
    {
        public delegate void AccountHandler(string message);
        public event AccountHandler Notify;
        public void Message()
        {
            Notify?.Invoke($"На счет поступило: ");   // 2.Вызов события 
        }

        public void Display()
        {
            Notify?.Invoke("method dysplay");
        }
    }
}
