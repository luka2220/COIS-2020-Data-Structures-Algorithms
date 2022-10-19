using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    // Common interface for ALL data structures

    public interface IContainer<T>
    {
        void MakeEmpty();  // Reset an instance to empty
        bool Empty();      // Test if an instance is empty 
        int Size();        // Return the number of items in an instance
    }

    //-------------------------------------------------------------------------

    public interface IStack<T> : IContainer<T>
    {
        void Push(T item); // Place an item on the top of a stack
        void Pop();        // Remove the top item of a stack
        T Top();           // Return the top item of a stack
    }

    //-----------------------------------------------------------------------------

    // Stack
    // Behavior: Last-In, First-Out (LIFO)
    // Implementation: Singly linked list

    public class Stack<T> : IStack<T>
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

            public Node(T item, Node next)
            {
                Item = item;
                Next = next;
            }
        }

        private Node top;       // Reference to the top item of the stack
        private int count;      // Number of items in the stack

        // Time complexity of all methods: O(1)

        // Constructor
        // Create an empty stack

        public Stack()
        {
            MakeEmpty();
        }

        // MakeEmpty
        // Resets the stack to empty

        public void MakeEmpty()
        {
            top = null;
            count = 0;
        }

        // Empty
        // Returns true if the stack is empty; false otherwise

        public bool Empty()
        {
            return count == 0;
        }

        // Size
        // Returns the number of items in the stack

        public int Size()
        {
            return count;
        }

        // Push
        // Inserts an item at the top of the stack

        public void Push(T item)
        {
            top = new Node(item, top);
            count++;
        }

        // Pop
        // Removes the top item from the stack
        // Throws an InvalidOperationException if the stack is empty 

        public void Pop()
        {
            if (Empty())
            {
                throw new InvalidOperationException("Stack is empty");
            }
            else
            {
                top = top.Next;
                count--;
            }
        }

        // Top
        // Retrieves the item from the top of the stack
        // Throws an InvalidOperationException if the stack is empty

        public T Top()
        {
            if (Empty())
            {
                throw new InvalidOperationException("Stack is empty");
            }
            else
            {
                return top.Item;
            }
        }
    }

    //-----------------------------------------------------------------------------

    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> S;
            S = new Stack<int>();

            Console.WriteLine("Executing Singly Linked List Implementation of Stack");

            for (int i = 0; i < 15; i++)
                S.Push(i);

            Console.WriteLine();
            Console.WriteLine("Size of the stack: " + S.Size());

            while (!S.Empty())
            {
                Console.Write(S.Top() + " ");
                S.Pop();
            }

            Console.WriteLine();
            Console.WriteLine("Size of the stack: " + S.Size());

            Console.ReadKey();
        }
    }
}
