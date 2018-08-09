using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListApp
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList list = new LinkedList();

            list.Add("Test1");
            list.Add(0,"Test2");
            list.Add(0, "Test3");
            list.Add(0, "Test4");
            list.Add(0, "Test5");

            list.PrintList();
            Console.WriteLine();

            Console.WriteLine(list.Get(7));
            Console.WriteLine();

            Console.WriteLine(list[0]);
        }
    }
}
