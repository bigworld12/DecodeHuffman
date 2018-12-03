using System;
using System.Collections.Generic;

namespace DecodeHuffman
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

    }
    public class MinPriortyQueue<T>
    {
        public MinPriortyQueue(int n)
        {
            arr = new T[n];
        }
        private readonly T[] arr;

        private int start = 0;
        public int Count { get; private set; }

        public T ExtractMin()
        {
            if (IsEmpty())
            {
                throw new Exception("Can't extract from an empty Queue");
            }
            var ret = arr[0];
            IncStart();
            Count--;
            return ret;
        }
        public void Insert(T elem)
        {
            //do binary search then insert
        }
        private void DecStart()
        {
            if (start == 0)
            {
                start = arr.Length;
            }
            else
            {
                start--;
            }
        }
        private void IncStart()
        {
            if (start == arr.Length - 1)
            {
                start = 0;
            }
            else
            {
                start++;
            }
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }
        public bool IsFull()
        {
            return Count == arr.Length;
        }

    }

}
