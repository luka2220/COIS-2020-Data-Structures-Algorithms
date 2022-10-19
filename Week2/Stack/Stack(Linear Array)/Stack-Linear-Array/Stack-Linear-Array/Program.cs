﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    // Common interface for ALL classes

    // Interface for supporting methods
    public interface IContainer<T>
    {
        void MakeEmpty();  // Reset an instance to empty
        bool Empty();      // Test if an instance is empty 
        int Size();        // Return the number of items in an instance
    }

    //-------------------------------------------------------------------------

    // interface for primary methods

    public interface IStack<T> : IContainer<T>
    {
        void Push(T item); // Place an item on the top of a stack
        void Pop();        // Remove the top item of a stack
        T Top();           // Return the top item of a stack
    }

    //-------------------------------------------------------------------------

    // Stack 
    // Behavior: Last-In, First-Out (LIFO)
    // Implementation:  Linear Array

    class Stack<T> : IStack<T>
    {
        private T[] A;         // Linear array of items (Generic)
        private int top;       // Index of the top item in the stack
        private int capacity;  // Maximum capacity of the stack

        // Constructor
        // Creates an empty stack
        // Time complexity:  O(1)

        public Stack()
        {
            A = new T[10];
            capacity = 10;
            MakeEmpty();
        }

        // MakeEmpty
        // Resets the stack to empty
        // Time complexity:  O(1)

        public void MakeEmpty()
        {
            top = -1;
        }

        // Empty
        // Returns true if the stack is empty; false otherwise
        // Time complexity:  O(1)

        public bool Empty()
        {
            return top == -1;
        }

        // Size
        // Returns the number of items in the stack
        // Time complexity:  O(1)

        public int Size()
        {
            return top + 1;
        }

        // DoubleCapacity
        // Doubles the capacity of the current stack
        // Time complexity:  O(n)

        private void DoubleCapacity()
        {
            int i;
            T[] oldA = A;

            capacity = 2 * capacity;
            A = new T[capacity];

            for (i = 0; i <= top; i++)
            {
                A[i] = oldA[i];
            }
        }

        // Push
        // Inserts an item at the top of the stack
        // Doubles the capacity of the stack if the stack is full
        // Amortized time complexity:  O(1)

        public void Push(T item)
        {
            if (top + 1 == capacity)
            {
                DoubleCapacity();
            }
            A[++top] = item;
        }

        // Pop
        // Removes the top item from the stack
        // Throws an InvalidOperationException if the stack is empty
        // Time complexity:  O(1)

        public void Pop()
        {
            if (Empty())
            {
                throw new InvalidOperationException("Stack is empty");
            }
            else
            {
                top--;
            }
        }

        // Top
        // Retrieves the item from the top of the stack
        // Throws an InvalidOperationException if the stack is empty
        // Time complexity:  O(1)

        public T Top()
        {
            if (Empty())
            {
                throw new InvalidOperationException("Stack is empty");
            }
            else
            {
                return A[top];
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

            Console.WriteLine("Executing Linear Array Implementation of Stack");

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
