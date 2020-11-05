using System;
using System.Threading.Tasks;

namespace TestCSharpInConsoleApp
{
    class Program
    {
        public static string XXX
        {
            get { return YYY().Result; }
        }

        static async Task<string> YYY()
        {
            return "Hello World!";
        }

        static void Main(string[] args)
        {
            Console.WriteLine(XXX);
            Console.ReadLine();
        }
    }
}
