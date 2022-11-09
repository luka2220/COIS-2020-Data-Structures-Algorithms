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
        T Retrieve(int p);               // Retrieve the item at position p in the list
        bool Contains(T item);           // Return true if item is found in the list; false otherwise
    }

    //-----------------------------------------------------------------------------

    // List
    // Implementation:  Singly linked list

    public class List<T> : IList<T>
    {

        // Common generic Node class for all singly linked lists

        private class Node
        {
            public T Item    { get; set; }     // Read/write property
            public Node Next { get; set; }     // Read/write property

            // Parameterless constructor

            public Node()
            {
                Next = null;
            }

            // Constructor
            // Creates a single node set to the parameters

            public Node(T item, Node next = null)
            {
                Item = item;
                Next = next;
            }
        }

        // Data members

        private Node front;         // Reference to the first node of the list
        private Node back;          // Reference to the last node of the list
        private int count;          // Number of items in the list

        // Constructor
        // Creates an empty list with a header node
        // Time complexity: O(1)

        public List()
        {
            MakeEmpty();
        }

        // MakeEmpty
        // Resets list to empty
        // Time complexity: O(1)

        public void MakeEmpty()
        {
            back = front = new Node();
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

        // Add
        // Adds item at the end of the list
        // Time complexity:  O(1)

        public void Add(T item)
        {
            back = back.Next = new Node(item);
            count++;
        }

        // Insert
        // Inserts item at position p in the list
        // Throws an InvalidOperationException if position p is out of range
        // (Worst case) time complexity: O(n)

        public void Insert(T item, int p)
        {
            Node curr = front;

            if (p < 1 || p > count + 1)
            {
                throw new InvalidOperationException("Position is out of range");
            }
            else
            {
                if (p < count + 1)
                { 
                    for (int i = 1; i <= p - 1; i++)      // Move to the node at position p-1
                    {
                        curr = curr.Next;
                    }

                    curr.Next = new Node(item, curr.Next);
                    count++;
                }
                else
                {
                    Add(item);
                }
            }
        }

        // RemoveAt
        // Removes the item at position p in the list
        // Throws an InvalidExceptionOperation if the list is empty or position p is out of range
        // Time complexity: O(n)

        public void RemoveAt(int p)
        {
            Node curr = front;

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
                    for (int i = 1; i <= p - 1; i++)      // Move to the node at position p-1
                        curr = curr.Next;

                    curr.Next = curr.Next.Next;
                    if (curr.Next == null)
                    {
                        back = curr;
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
            bool found = false;
            Node curr = front;

            while (curr.Next != null && !found)
            {
                if (curr.Next.Item.Equals(item))
                {
                    curr.Next = curr.Next.Next;
                    if (curr.Next == null)
                        back = curr;
                    found = true;
                    count--;
                }
                else
                {
                    curr = curr.Next;
                }
            }
            return found;
        }

        // Retrieve
        // Retrieves the item at position p
        // Throws an InvalidOperationException if the list is empty or position p is out of range
        // Time complexity: O(n)

        public T Retrieve(int p)
        {
            Node curr = front;

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
                    for (int i = 1; i <= p; i++)      // Move to the node at position p
                        curr = curr.Next;

                    return curr.Item;
                }
            }
        }

        // Contains
        // Returns true if item is in the list; false otherwise
        // Time complexity:  O(n)

        public bool Contains(T item)
        {
            Node curr = front.Next;
            bool found = false;

            while (curr != null && !found)
            {
                if (curr.Item.Equals(item))
                {
                    found = true;
                }
                else
                {
                    curr = curr.Next;
                }
            }

            return found;
        }

        // Print
        // Outputs the list to console
        // Time complexity: O(n)

        public void Print()
        {
            Node curr = front;
            while (curr.Next != null)
            {
                curr = curr.Next;
                Console.Write(curr.Item + " ");
            }
            Console.WriteLine();
        }
    }

    //-----------------------------------------------------------------------------

    class Program
    {
        static void Main(string[] args)
        {
            List<int> L;
            L = new List<int>();

            Console.WriteLine("Executing Singly Linked List Implementation of List");

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

            L.Print();

            Console.WriteLine("Size of the list: " + L.Size());

            Console.ReadKey();
        }
    }
}
