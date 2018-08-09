using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApp
{
    public class Graph<T> : IEnumerable, ICloneable where T: IComparable
    {
        static Random rd = new Random();
        public List<Node<T>> Vertices { get; set; }
        public int Count { get; set; }
        public Graph()
        {
            Vertices = new List<Node<T>>();
            Count = 0;
        }

        public void DFSStack(Node<T> source)
        {
            Stack<Node<T>> stack = new Stack<Node<T>>();
            stack.Push(source);
            source.IsSeen = true;
            DFSStack(source, stack);
        }
        private void DFSStack(Node<T> source, Stack<Node<T>> stack)
        {
            Console.WriteLine(source.Value);
            foreach (var root in stack.Pop().Roots)
            {
                if (!root.IsSeen)
                {
                    stack.Push(root);
                    root.IsSeen = true;
                }
            }

            if (stack.Count != 0)
                DFSStack(stack.Peek(), stack);
        }

        public void DFS(Node<T> sourec)
        {
            sourec.IsSeen = true;
            Console.WriteLine(sourec.Value);

            foreach (var root in sourec.Roots)
            {
                if (!root.IsSeen)
                    DFS(root);
            }
        }

        public void BFS(Node<T> source)
        {
            source.IsSeen = true;
            Queue<Node<T>> q = new Queue<Node<T>>();
            q.Enqueue(source);
            while (q.Count != 0)
            {
                foreach (var root in q.First().Roots)
                {
                    if (root.IsSeen == false)
                    {
                        q.Enqueue(root);
                        root.IsSeen = true;
                    }
                }
                Console.WriteLine(q.First().Value);
                q.Dequeue();
            }
        }

        public void Group()
        {
            foreach (var vertex in Vertices)
            {
                if (!vertex.IsSeen)
                {
                    BFS(vertex);
                    vertex.IsSeen = false;
                }
            }
        }
        public bool IsEmpty()
        {
            return Count == 0;
        }

        public void AddNode(T value)
        {
            if (SearchVertex(value))
                return;

            Vertices.Add(new Node<T>(value));
            Count++;
        }

        public bool SearchVertex(T value)
        {
            foreach (var vertex in Vertices)
            {
                if (vertex.Value.CompareTo(value) == 0)
                    return true;
            }
            return false;
        }

        public bool IsConnected(T v1, T v2)
        {
            Node<T> node1 = null;
            Node<T> node2 = null;

            foreach (var item in Vertices)
            {
                if (v1.Equals(item.Value))
                    node1 = item;
                if (v2.Equals(item.Value))
                    node2 = item;
            }

            if (node1 == null || node2 == null)
                return false;

            foreach (var item in node1.Roots)
            {
                if (item.Value.Equals(node2.Value))
                    return true;
            }
            return false;
        }
        public void Connect(T v1, T v2)
        {
            if (IsConnected(v1, v2))
                return;
            Node<T> node1 = null;
            Node<T> node2 = null;

            foreach (var item in Vertices)
            {
                if (v1.Equals(item.Value))
                    node1 = item;
                if (v2.Equals(item.Value))
                    node2 = item;
            }

            if (node1 == null)
            {
                node1 = new Node<T>(v1);
                Vertices.Add(node1);
                Count++;
            }
            if (node2 == null)
            {
                node2 = new Node<T>(v2);
                Vertices.Add(node2);
                Count++;
            }

            node1.Roots.Add(node2);
            node2.Roots.Add(node1);
        }

        public void ConnectAll(List<T> values)
        {
            for (int i = 0; i < values.Count - 1; i++)
            {
                for (int j = i + 1; j < values.Count; j++)
                {
                    Connect(values[i], values[j]);
                }
            }
        }

        public void Disconnect(T value1, T value2)
        {
            Node<T> t1 = null;
            Node<T> t2 = null;

            foreach (var item in this.Vertices)
            {
                if (item.Value.Equals(value1))
                    t1 = item;
                else if (item.Value.Equals(value2))
                    t2 = item;
            }

            if (t1 == null || t2 == null)
                return;

            for (int i = 0; i < t1.Roots.Count; i++)
            {
                if (t1.Roots[i].Equals(t2))
                {
                    t1.Roots.RemoveAt(i);
                    break;
                }
            }

            for (int i = 0; i < t2.Roots.Count; i++)
            {
                if (t2.Roots[i].Equals(t1))
                {
                    t2.Roots.RemoveAt(i);
                    break;
                }
            }
        }

        public void DeleteNode(T value)
        {
            if (!SearchVertex(value))
                return;
            for (int i = 0; i < Vertices.Count; i++)
            {
                if (Vertices[i].Value.Equals(value))
                {
                    for (int j = 0; j < Vertices[i].Roots.Count; j++)
                    {
                        for (int k = 0; k < Vertices[i].Roots[j].Roots.Count; k++)
                        {
                            if (Vertices[i].Roots[j].Roots[k].Value.Equals(Vertices[i].Value))
                            {
                                Vertices[i].Roots[j].Roots.RemoveAt(k);
                                break;
                            }
                        }
                    }
                    Vertices.RemoveAt(i);
                    break;
                }
            }
            Count--;
        }
        public int MinCut()
        {
            Graph<T> gClone;
            int index;

            List<int> minCut = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                gClone = Clone() as Graph<T>;
                index = rd.Next(gClone.Count);
                minCut.Add(MinCut(gClone, gClone.Vertices[index]));
            }
            return minCut.Min();
        }
        private int MinCut(Graph<T> graph, Node<T> source)
        {
            if (graph.Count <= 2)
                return graph.Vertices[0].Roots.Count;

            int index = rd.Next(source.Roots.Count);
            foreach (var item in source.Roots[index].Roots)
            {
                if (!item.Value.Equals(source.Value))
                {
                    source.Roots.Add(item);
                    item.Roots.Add(source);
                }
            }

            graph.DeleteNode(source.Roots[index].Value);

            return MinCut(graph, graph.Vertices[rd.Next(graph.Count)]);
        }

        
        public IEnumerator GetEnumerator()
        {
            foreach (var item in Vertices)
            {
                yield return item.Value;
            }
        }

        public object Clone()
        {
            Graph<T> clone = new Graph<T>();

            foreach (var vertex in Vertices)
            {
                if (!clone.SearchVertex(vertex.Value))
                {
                    clone.Vertices.Add((Node<T>)vertex.Clone());
                }

                foreach (var root in vertex.Roots)
                {
                    if (!clone.SearchVertex(root.Value))
                    {
                        clone.Vertices.Add((Node<T>)root.Clone());
                    }
                    clone.Connect(vertex.Value, root.Value);
                }
            }
            return clone;
        }
       
    }
}
