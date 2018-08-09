using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListApp
{
    class LinkedList
    {
        public Node Head { get; set; }

        public int Count { get; private set; }

        public LinkedList()
        {
            Head = null;
            Count = 0;
        }

        public bool Empty
        {
            get { return Count == 0; }
        }

        

        public object Add(int index, object o)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("Index can't be less than 0: Your index is " + index);

            if (index > Count)
                index = Count;

            Node current = Head;

            if (this.Empty || index == 0)
            {
                this.Head = new Node(o, this.Head);
            }
            else
            {
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }

                current.Next = new Node(o, current.Next);
            }

            Count++;
            return o;
        }

        public object Add(object o)
        {
            return Add(Count, o);
        }

        public object Remove(int index)
        {
            if (this.Empty)
                return null;

            if (index < 0)
                throw new ArgumentOutOfRangeException("Index can't be less than 0: Your index is " + index);

            if (index >= this.Count)
                index = Count - 1;

            Node current = this.Head;
            object result = null;

            if (index == 0)
            {
                result = current.Data;
                this.Head = current.Next;
            }
            else
            {
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }

                result = current.Next.Data;
                current.Next = current.Next.Next;
            }
            Count--;
            return result;
        }

        public void Clear()
        {
            Head = null;
            Count = 0;
        }

        public int IndexOf(object o)
        {
            Node current = Head;

            for (int i = 0; i < Count; i++)
            {
                if (current.Data.Equals(o))
                    return i;

                current = current.Next;
            }

            return -1;
        }

        public bool Contains(object o)
        {
            return this.IndexOf(o) >= 0;
        }

        public object Get(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("The index can't be less than 0. Your index is: " + index);

            if (index >= this.Count)
                index = Count - 1;

            Node current = this.Head;

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            return current.Data;
        }

        public object this[int index]
        {
            get { return this.Get(index); }
        }

        public void PrintList()
        {
            Node current = this.Head;

            for (int i = 0; i < this.Count; i++)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }
    }
}

