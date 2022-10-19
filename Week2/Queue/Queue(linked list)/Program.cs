using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    // Common interface for ALL linear data structures

    public interface IContainer<T>
    {
        void MakeEmpty();  // Reset an instance to empty
        bool Empty();      // Return true if the queue is empty; false otherwise 
        int Size();        // Return the number of items in an instance
    }

    //-------------------------------------------------------------------------

    public interface IQueue<T> : IContainer<T>
    {
        void Enqueue(T item);  // Add an item to the back of a queue
        void Dequeue();        // Remove an item from the front of a queue
        T Front();             // Return the front item of a queue
    }

    //-------------------------------------------------------------------------

    // Queue
    // Behavior:  First-In, First-Out (FIFO)
    // Implementation:  Singly linked list

    public class Queue<T> : IQueue<T>
    {

        // Common generic Node class for all singly linked lists

        private class Node
        {
            public T Item { get; set; }        // Read/write property
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

        private Node front;     // Reference to the front item
        private Node back;      // Reference to the back item
        private int count;      // Number of items in the queue

        // Time complexity of all methods: O(1)

        // Constructor
        // Creates an empty queue

        public Queue()
        {
            MakeEmpty();
        }

        // MakeEmpty
        // Resets the queue to empty

        public void MakeEmpty()
        {
            front = back = null;
            count = 0;
        }

        // Empty
        // Returns true if the queue is empty; false otherwise

        public bool Empty()
        {
            return count == 0;   // front == null
        }

        // Size
        // Returns the number of items in the queue

        public int Size()
        {
            return count;
        }

        // Enqueue
        // Inserts an item at the back of the queue

        public void Enqueue(T item)
        {
            Node newNode = new Node(item);
            if (count == 0)
            {
                front = back = newNode;
            }
            else
            {
                back = back.Next = newNode;
            }
            count++;
        }

        // Dequeue
        // Removes the front item of the queue
        // Throws an InvalidOperationException if the queue is empty

        public void Dequeue()
        {
            if (Empty())
            {
                throw new InvalidOperationException("Queue is empty");
            }
            else
            {
                front = front.Next;
                count--;
            }
        }

        // Front
        // Retrieves the front item of the queue
        // Throws an InvalidOperationException is the queue is empty

        public T Front()
        {
            if (Empty())
            {
                throw new InvalidOperationException("Queue is empty");
            }
            else
            {
                return front.Item;
            }
        }
    }

    //-----------------------------------------------------------------------------

    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> Q;
            Q = new Queue<int>();

            Console.WriteLine("Executing Singly Linked List Implementation of Queue");

            for (int i = 0; i < 15; i++)
                Q.Enqueue(i);

            Console.WriteLine();
            Console.WriteLine("Size of the queue: " + Q.Size());

            while (!Q.Empty())
            {
                Console.Write(Q.Front() + " ");
                Q.Dequeue();
            }

            Console.WriteLine();
            Console.WriteLine("Size of the queue: " + Q.Size());

            Console.ReadKey();
        }
    }
}
