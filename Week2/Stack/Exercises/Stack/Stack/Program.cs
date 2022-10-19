using System;

namespace Stack
{
    // Interface for supporting methods
    public interface IContainer<T> 
    {
        void MakeEmpty(); // resets the stack
        bool Empty(); // returns true if the stack is empty; false otherwise
        int Size(); // return the number of items in the stack
    }

    // Interface for primary methods
    public interface IStack<T> : IContainer<T>
    {
        void Push(T item);  // push a item on top of the stack
        void Pop(); // remove the top item of the stack
        T Top();  // return the top item of the stack
    }

    // Object for representing Stack Array data structure
    class StackArray<T> : IStack<T>
    {
        // Capacity of the stack
        private int capacity;
        // Index of the to element of stack
        private int top;
        // Linear array of items
        private T[] A;

        // Constructor
        // Creates an empty stack of size 10
        // Time Complexity O(1)
        public StackArray()
        {
            A = new T[10];
            capacity = 10;
            MakeEmpty();
        }

        // MakeEmpty()
        // Resets the stack to empty
        // Time Complexity O(1)
        public void MakeEmpty()
        {
            top = -1;
        }

        // Push(T item)
        // Places an item onto the top of the stack
        public void Push(T item)
        {
            if (top + 1 == capacity)
            {
                DoubleCapacity();
            }
            
            top++; // increase top item by 1
            A[top] = item;
        }

        // Supporintg method to double the capacity of the current stack
        private void DoubleCapacity()
        {
            T[] newStack = new T[capacity * 2];

            for (int i = 0; i < capacity; i++)
            {
                newStack[i] = A[i];
            }

            // Update the stack
            A = newStack;
            // Update the capacity
            capacity *= 2;
        }

        // Pop()
        // Pop an item off the current stack
        // Time Complexity O(1)
        public void Pop()
        {
            if (Empty())
            {
                throw new InvalidOperationException("Stack is empty");
            }
            else {
                top--;
            }
        }

        // Top()
        // Retrieve the top item of the stack
        // Time Complexity O(1)
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

        // Size()
        // Return the size of the stack
        // Time Complexity O(1)
        public int Size()
        {
            return top + 1;
        }
    
        public bool Empty()
        {
            if (top == -1)
            {
                return true;
            }

            return false;
        }

        public void Print()
        {
            for (int i = top; top >= 0; i--)
            {
                if (i == top)
                {
                    Console.WriteLine("{0} - top of stack", A[i]);
                    continue;
                }
                else if (i == 0)
                {
                    Console.WriteLine("{0} - bottom of stack", A[i]);
                    break;
                }

                Console.WriteLine(A[i]);   
            }

            Console.WriteLine("Total number of items in the stack: {0}", top + 1);
            Console.WriteLine("Current capacity of the stack: {0}", capacity);
        }
    }

    // Object for representing Stack Linked List Data Structure
    class StackLinkedList<T> : IStack<T>
    {
        private class Node
        { 
            public T item { get; set;  }
            public Node next { get; set; }

            public Node()
            {
                next = null;
            }

            public Node(T item, Node next)
            {
                this.item = item;
                this.next = next;
            }
        }

        private Node top;
        private int count;

        public StackLinkedList()
        {
            MakeEmpty();
        }

        public void MakeEmpty()
        {
            top = null;
            count = 0;
        }

        public bool Empty()
        {
            if (count == 0)
            {
                return true;
            }

            return false;
        }

        public int Size()
        {
            return count;
        }

        public void Push(T item)
        {
            top = new Node(item, top);

            count++;
        }

        public void Pop()
        {
            if (Empty())
            {
                throw new InvalidOperationException("Cannot pop, stack is empty");
            }
            else {
                top = top.next;

                count--;
            }
        }

        public T Top()
        {
            if (Empty())
            {
                throw new InvalidOperationException("Cannot retrieve top, stack is empty");
            } 
            else {
                return top.item;
            }  
        }

        public void ReverseStack()
        {
            Node newNode = new Node(top.item, null);
            Node prevList = top;

            for (int i = 0; i < count; i++)
            {
                if (i == 0)
                {
                    prevList = top.next; 
                    top = newNode;  

                    continue;
                }

                top = new Node(prevList.item, top);

                prevList = prevList.next;
            }
        }

        public void Print()
        {
            Node currentNode = top;

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("{0}", currentNode.item);
                currentNode = currentNode.next;
            }
        }
    }
}


// Tests - Main program class
class Tests
{
    public static void Main(String[] args)
    {
        Stack.StackLinkedList<char> name = new Stack.StackLinkedList<char>();

        name.Push('l');
        name.Push('u');
        name.Push('k');
        name.Push('a');

        name.Print();

        name.ReverseStack();

        Console.WriteLine("\nReversed Stack");

        name.Print();

    }
}