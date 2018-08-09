using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MinHeap<int> heap = new MinHeap<int>();
            heap.Add(8);
            heap.Add(2);
            heap.Add(4);
            heap.Add(7);
            heap.Add(6);
            heap.Add(5);
            heap.Add(1);
            heap.Add(9);
            heap.Add(0);


            foreach (var item in heap)
            {
                Console.Write(item + " ");
            }

            heap.Pop();
            Console.WriteLine();
            foreach (var item in heap)
            {
                Console.Write(item + " ");
            }
        }
    }
}
