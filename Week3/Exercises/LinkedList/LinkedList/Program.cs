using System;
using System.Diagnostics;
namespace List
{
    public interface IContainer<T>
    {
        void MakeEmpty();   // Resets the list to empty
        bool Empty();       // Returns true if the list is empty; false otherwise
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

        // Basic constructor for lit object
        public List()
        {
            MakeEmpty();
        }

        // MakeEmpty
        // Resets the current list to an empty one
        // Time Complexity: O(1)
        public void MakeEmpty()
        {
            back = front = new Node();
            count = 0;
        }

        // Empty
        // Returns true is the list is empty; false otherwise
        // Time Complexity: O(1)
        public bool Empty()
        {
            return count == 0;
        }

        // Size
        // Returns the number of items in the list
        // Time Complexity: O(1)
        public int Size()
        {
            return count;
        }

        // Print
        // Outputs all the items in the list
        // Time Complexity: O(n)
        public void Print()
        {
            Node curr = front.Next;

            for (int i = 1; i <= count; i++)
            {
                Console.Write("{0} ", curr.Item);
                curr = curr.Next;
            }
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

            if (p < 1 || p > count + 1)
            {
                throw new InvalidOperationException("Index is out of range");
            }
            if (p == count)
            {
                back = back.Next = new Node(item);
                count++;
            }
            else {
                for (int i = 1; i < p - 1; i++)
                {
                    curr = curr.Next;
                }

                Node newNode = new Node(item, curr.Next);
                curr.Next = newNode;
                count++;
            } 
        }

        // RemoveAt(int p)
        // Removes and item at a specific index in the list
        // Time Complexity: O(n) - worst case
        public void RemoveAt(int p)
        {
            if (Empty() || p < 0 || p > count)
            {
                throw new InvalidOperationException("Index is out of range or list is empty");
            }
            else
            {
                Node curr = front.Next;

                for (int i = 1; i < p - 1; i++)
                {
                    curr = curr.Next;
                }
                if (p == count)
                {
                    back = curr;
                    count--;
                }
                else
                {
                    Node toRemove = curr.Next;
                    curr.Next = toRemove.Next;
                    count--;
                }
            }
        }

        // Remove(T item)
        // Returns true if item exists in list and is removed; false otherwise
        // Time Complexity: O(n) - worst case
        public bool Remove(T item)
        {
            Node curr = front.Next;
            bool found = false;

            if (Empty()) throw new InvalidOperationException("List is empty");
            else {
                for (int i = 1; i <= count; i++)
                {
                    if (curr.Next.Item.Equals(item))
                    {
                        Node toRemove = curr.Next;
                        curr.Next = toRemove.Next;

                        count--;
                        found = true;

                        break;
                    }

                    curr = curr.Next;
                }
            }

            return found;
        }

        // Retrieve(int p)
        // Returns the item at a given index; throws an exception if the item does not exist
        // Time Complexity: O(n) - worst case
        public T Retrieve(int p)
        {
            Node curr = front.Next;

            if (Empty() || p < 1 || p > count)
            {
                throw new InvalidOperationException("List is empty or index is out of range");
            }
            else {
                for (int i = 1; i <= count; i++)
                {
                    if (i == p)
                    {
                        break;
                    }

                    curr = curr.Next;
                }
            }

            return curr.Item;
        }

        // Contains(T item)
        // Returns true if the item is in the list; false if not; throws exception if list is empty
        // Time Complexity: O(n) - worst case
        public bool Contains(T item)
        {
            Node curr = front.Next;
            bool found = false;

            if (Empty()) throw new InvalidOperationException("List is empty");
            else
            {
                for (int i = 0; i <= count; i++)
                {
                    if (curr.Item.Equals(item))
                    {
                        found = true;
                        break;
                    }

                    curr = curr.Next;
                }
            }

            return found;
        }
    }

    // Main class for executing List object
    class Program
    {
        public static void Main(String[] args)
        {
            List<int> list = new List<int>();

            for (int i = 1; i <= 20; i++)
            {
                list.Add(i);
            }

            Console.WriteLine("Linked list implementation of list");

            list.Print(); // displaying current list

            list.Insert(50, 10);
            list.RemoveAt(15);
            Console.WriteLine("\nRemove item 17? {0}", list.Remove(17));

            Console.WriteLine();
            list.Print();

            Console.ReadLine(); 
        }
    }
}