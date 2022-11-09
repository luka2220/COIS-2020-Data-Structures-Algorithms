using System;
using System.Linq.Expressions;

namespace List 
{
    // Supporting methods
    public interface IContainer<T>
    {
        void MakeEmpty();   // Resets list to empty
        bool Empty();       // Returns true if list is empty; false otherwise
        int Size();         // Returns number if items in the list
        void Print();       // Prints all items in the list
    }

    // Primary Methods
    public interface IList<T> : IContainer<T>
    {
        
        void Add(T item);              // Adds an item to the end of the list
        void Insert(T item, int p);    // Inserts an item at position p in list
        void RemoveAt(int p);          // Removes an item at a specific index in the list
        bool Remove(T item);           // Removes last item in array
        T Retrieve(int p);             // Return true if item is removed from the list; false otherwise
        bool Contains(T item);         // Returns true if item is in list; false otherwise
    }

    // Object for List
    public class List<T> : IList<T>
    {
        private T[] A;          // Linear array of list items
        private int capacity;   // Capacity of array
        private int count;      // Number of items in list

        public List() 
        {
            A = new T[8];   
            capacity = 8;
            MakeEmpty();
        }

        public void MakeEmpty() 
        {
            count = 0;
        }

        public bool Empty() 
        {
            return count == 0;
        }

        public int Size() 
        { 
            return count; 
        }

        public void Print() 
        {
            for (int i = 0; i < count; i++)
            {
                Console.Write("{0} ", A[i]);
            }
        }

        public void DoubleCapacity()
        {
            T[] oldA = A;
            A = new T[capacity * 2];
            for (int i = 0; i < count; i++)
            {
                A[i] = oldA[i];
            }
            capacity *= 2;
        }

        public void Add(T item) 
        {
            if (count == capacity)
            {
                DoubleCapacity();
            }
            A[count] = item;
            count++;
        }

        public void Insert(T item, int p) 
        {
            if (p < 0 || p > count)
            {
                throw new InvalidOperationException("Index is out of range");
            }
            else {
                if (count == capacity)
                {
                    DoubleCapacity();
                }
                T[] oldA = A;
                A = new T[capacity];
                int i, j;
                for (i = j = 0; i <= count; i++, j++)
                {
                    if (i == p)
                    {
                        A[i] = item;
                        i++;
                    }
                    else
                    {
                        A[i] = oldA[j];
                    }
                }
                count++;
            }
        }

        public void RemoveAt(int p) 
        {
            if (Empty() || p < 0 || p > count)
            {
                throw new InvalidOperationException("List is empty or Index is out of range");
            }
            else
            {
                for (int i = p; i <= count - 1; i++)
                {
                    A[i - 1] = A[i];
                }
                count--;
            } 
        }

        public bool Remove(T item)
        {
            if (Empty())
            {
                throw new InvalidOperationException("List is empty");
            }
            else {
                for (int i = 0; i < count; i++)
                {
                    if (A[i].Equals(item)) 
                    {
                        RemoveAt(i);
                        return true;
                    }
                }
            }
            return false;
        }

        public T Retrieve(int p)
        {
            if (p < 0 || p > count)
            {
                throw new InvalidOperationException("Index is out of range");
            }
            else {
                return A[p - 1];
            }
        }

        public bool Contains(T item) 
        {
            if (Empty())
            {
                throw new InvalidOperationException("List is empty");
            }
            else {
                for (int i = 0; i < count; i++)
                {
                    if (A[i].Equals(item))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }

    // Main program for executing List operations
    class Program
    {
        public static void Main(String[] args)
        {
            List<int> list = new List<int>();

            Console.WriteLine("Creating list of numbers");

            for (int i = 1; i <= 20; i++)
            {
                list.Add(i);
            }

            list.Print();

            Console.WriteLine("\nDoes list contain 50?");
            Console.WriteLine(list.Contains(50));

            Console.WriteLine("Does list contain 12?");
            Console.WriteLine(list.Contains(12));

            Console.WriteLine("Does list contain -5?");
            Console.WriteLine(list.Contains(-5));

            Console.WriteLine("Retrieve item at position 7");
            Console.WriteLine(list.Retrieve(7));

            Console.WriteLine("Remove item value 10 from list");
            Console.WriteLine(list.Remove(10));

            list.Print();


            Console.ReadLine();
        }
    }   
}
