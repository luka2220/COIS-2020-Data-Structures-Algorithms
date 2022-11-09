using System;
using System.Diagnostics;

namespace List
{
    public interface IContainer<T>
    {
        void MakeEmpty();   // Resets the list to empty
        void Empty();       // Returns true if the list is empty; false otherwise
        int Size();         // Returns the number of items in the list 
        void Print();       // Prints all items in the list to the console
    }

    public interface IList<T> : IContainer<T>
    {
        void Add(T item);           // Adds an item to the end of the list
        void Insert(T item, int p); // Insert an item at specific position in list
        void RemoveAt(int p);       // Removes an item at a specific position
        bool Remove(T item);        // Returns true if item was removed from list; false otherwise
        T Retrieve(int p);          // Returns an item at a specific position in list
        bool Contains(T item);      // Returns true if an item exists an the list; false otherwise
    }

    public class List<T> : IList<T>
    {
        // Generic node class for singly linked list
        private class Node
        {
            public T Item { get; set;  }
            public Node Next { get; set; }

            public Node()
            {
                Next = null;
            }

            public Node(T item, Node next = null)
            {
                Item = item;
                Next = next;
            }
        }

        // Data Members
        Node front; // Reference to head node
        Node back;  // Reference to last node
        int count;  // Count of all items in list

        public List()
        {
            MakeEmpty();
        }

        public void MakeEmpty()
        {
            back = front = new Node();
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

        // Add(T item)
        // Adds an item to the emd of the linked list
        // Time Complexty: O(1)
        public void Add(T item)
        {
            back = back.Next = new Node(item);
            count++;
        }

        // Insert(T item, int p)
        // Inserts a new item at a specific index in the list
        // Time Complexity: O(n) - worst case
        public void Insert(T item, int p)
        {
            Node curr = front.Next;

            for (int i = 0; i < count; i++)
            {
                if (i == p)
                {
                    Node newNode = new Node(item, curr.Next);
                    curr.Next = newNode;
                    count++;
                    break;
                }
            }
        }

        // RemoveAt(int p)
        // Removes and item at a specific index in the list
        // Time Complexity: O(n) - worst case

    }

    // Main class for executing List object
    class Program
    {
        public static void Main(String[] args)
        { 
            
        }
    }
}