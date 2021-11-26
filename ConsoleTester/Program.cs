using ConsoleTester.Service;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTester
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var a = "Hello world";
            var date = "10-10-2000 10:10";
            var b = "Hello world, this is test";
            Console.WriteLine($"{a, -100} {date, -50}");
            Console.WriteLine($"{b, -100} {date, -50}");
            Console.WriteLine($"{a, -100} {date, -50}");
            Console.WriteLine($"{b, -100} {date, -50}");
        }
    }
}
