using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List
{
    // Common interface for ALL linear data structures

    public interface IContainer<T>
    {
        void MakeEmpty();  // Reset an instance to empty
        bool Empty();      // Test if an instance is empty 
        int Size();        // Return the number of items in an instance
    }

    //-----------------------------------------------------------------------------

    public interface IList<T> : IContainer<T>
    {
        void Add(T item);                // Add item at the end of the list
        void Insert(T item, int p);      // Insert item at position p in the list      
        void RemoveAt(int p);            // Remove the item at position p in the list
        bool Remove(T item);             // Return true if item is removed from the list; false otherwise
        T Retrieve(int p);               // Retrieve the item at positiono p in the list
        bool Contains(T item);           // Return true if item is found in the list; false otherwise
    }

    //-----------------------------------------------------------------------------

    // List
    // Implementation:  Linear Array

    class List<T> : IList<T>
    {
        // Data members

        private T[] A;                 // Linear array of items 
        private int capacity;          // Maximum capacity of the list
        private int count;             // Number of items in the list

        // Constructor
        // Create an empty list
        // Time complexity:  O(1)

        public List() 
        {
            capacity = 8;
            A = new T[capacity];
            MakeEmpty();
        }

        // MakeEmpty
        // Resets list to empty
        // Time complexity: O(1)

        public void MakeEmpty()
        {
            count = 0;
        }

        // Empty
        // Returns true if the list is empty; false otherwise
        // Time complexity: O(1)

        public bool Empty()
        {
            return count == 0;
        }

        // Size
        // Returns the number of items in the list
        // Time complexity: O(1)

        public int Size()
        {
            return count;
        }

        // DoubleCapacity
        // Doubles the capacity of the list
        // Time complexity:  O(n)

        private void DoubleCapacity()
        {
            T[] oldA = A;

            capacity = 2 * capacity;
            A = new T[capacity];

            for (int i = 0; i < count; i++)
            {
                A[i] = oldA[i];
            }
        }

        // Add
        // Adds item at the end of the list
        // Doubles the capacity of the list if the list is full
        // Amortized time complexity:  O(1)

        public void Add(T item)
        {
            if (count == capacity)
            {
                DoubleCapacity();
            }
            A[count] = item;
            count++;

            //Insert(item, count + 1);
        }

        // Insert
        // Inserts item at position p in the list
        // Doubles the capacity of the list if the list is full
        // Throws an InvalidOperationException if position p is out of range
        // Time complexity:  O(n)

        public void Insert(T item, int p)
        {
            if (p < 1 || p > count + 1)
            {
                throw new InvalidOperationException("Position is out of range");
            }
            else
            {
                int i;

                if (count == capacity)
                {
                    DoubleCapacity();              }

                // Shift items A[count-1] down to A[p-1] up one position
                for (i = count; i >= p; i--)
                {
                    A[i] = A[i-1];
                }
                A[i] = item;
                count++;
            }
        }

        // RemoveAt
        // Removes the item at position p in the list
        // Throws an InvalidExceptionOperation if the list is empty or position p is out of range
        // Time complexity: O(n)

        public void RemoveAt(int p)
        {
            if (Empty())
            {
                throw new InvalidOperationException("List is empty");
            }
            else
            {
                if (p < 1 || p > count)
                {
                    throw new InvalidOperationException("Position is out of range");
                }
                else
                {
                    // Shift items A[p] up to A[count-1] down one position
                    for (int i = p; i <= count - 1; i++)
                    {
                        A[i - 1] = A[i];
                    }
                    count--;
                }
            }
        }

        // Remove
        // Returns true if item is removed from the list; false otherwise
        // Time complexity:  O(n)

        public bool Remove(T item)
        {
            if (Empty())
            {
                throw new InvalidOperationException("List is empty");
            }
            else
            {
                bool found = false;
                int i = 0;

                while (i < count && !found)
                {
                    if (A[i].Equals(item))
                    {
                        RemoveAt(i);
                        found = true;
                    }
                    else
                    {
                        i++;
                    }
                }
                return found;
            }
        }

        // Retrieve
        // Retrieves the item at position p
        // Throws an InvalidOperationException if the list is empty or position p is out of range
        // Time complexity: O(1)

        public T Retrieve(int p)
        {
            if (Empty())
            {
                throw new InvalidOperationException("List is empty");
            }
            else
            {
                if (p < 1 || p > count)
                {
                    throw new InvalidOperationException("Position is out of range");
                }
                else
                {
                    return A[p - 1];
                }
            }
        }

        // Contains
        // Returns true if item is in the list; false otherwise
        // Time complexity:  O(n)

        public bool Contains(T item)
        {
            int i = 0;
            bool found = false;

            while (i < count && !found)
            {
                if (A[i].Equals(item))
                {
                    found = true;
                }
                else
                {
                    i++;
                }
            }
            return found;
        }

        // Print
        // Outputs the list to console
        // Time complexity: O(n)

        public void Print()
        {
            for (int i = 0; i < count; i++)
                Console.Write(A[i] + " ");

            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<int> L;
            L = new List<int>();

            Console.WriteLine("Executing Linear Array Implementation of List");

            L.Insert(5, 1);      // 5
            L.Insert(3, 2);      // 5 3
            L.Insert(9, 1);      // 9 5 3
            L.Insert(2, 2);      // 9 2 5 3
            L.Add(7);            // 9 2 5 3 7

            L.Print();

            Console.WriteLine(L.Retrieve(1));   // 9
            Console.WriteLine(L.Retrieve(3));   // 5

            L.Remove(2);         // 9 5 3 7
            L.RemoveAt(1);       // 5 3 7
            L.Remove(1);         // 5 3 7
            L.Remove(3);         // 5 7

            L.Print();           // 5 7

            Console.WriteLine("Size of the list: " + L.Size());

            Console.ReadKey();
        }
    }
}
