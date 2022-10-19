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
        bool Empty();      // Test if an instance is empty 
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
    // Implementation:  Circular Array

    public class Queue<T> : IQueue<T>
    {
        private T[] A;            // Linear array of items (Generic)
        private int front, back;  // Indices of the front and back items
        private int capacity;     // Maximum capacity of the queue
        private int count;        // Actual number of items in the queue
  
        // Time complexity of all methods (except DoubleCapacity and Enqueue):  O(1)

        // Constructor
        // Creates an empty queue

        public Queue()
        {
            A = new T[8];
            capacity = 8;
            MakeEmpty();
        }

        // MakeEmpty
        // Resets the queue to empty

        public void MakeEmpty()
        {
            count = 0;
            front = 0;
            back = capacity - 1;            // of -1
        }

        // Empty
        // Returns true if the queue is empty; false otherwise

        public bool Empty()
        {
            return count == 0;
        }

        // Size
        // Returns the number of items in the queue

        public int Size()
        {
            return count;
        }

        // DoubleCapacity
        // Doubles the capacity of the current queue
        // Time complexity:  O(n)

        private void DoubleCapacity()
        {
            int i, j;
            T[] oldA = A;

            A = new T[2 * capacity];
            for (i = 0, j = front; i < count; i++, j = (j + 1) % capacity)
            {
                A[i] = oldA[j];
            }
            front = 0;
            back = count - 1;
            capacity = 2 * capacity;
        }

        // Enqueue
        // Inserts an item at the back of the queue
        // Doubles the capacity of the queue if the queue is full
        // Amortized time complexity:  O(1)

        public void Enqueue(T item)
        {
            if (count == capacity)
            {
                DoubleCapacity();
            }
            back = (back + 1) % capacity;    // Increment back with wraparound 
            A[back] = item;
            count++;
        }

        // Dequeue
        // Removes the front item from the queue
        // Throws an InvalidOperationException if the queue is empty

        public void Dequeue()
        {
            if (Empty())
            {
                throw new InvalidOperationException("Queue is empty");
            }
            else
            {
                front = (front + 1) % capacity;  // Increment front with wraparound
                count--;
            }
        }

        // Front
        // Retrieves the item the front item of the queue
        // Throws an InvalidOperationException if the queue is empty

        public T Front()
        {
            if (Empty())
            {
                throw new InvalidOperationException("Queue is empty");
            }
            else
            {
                return A[front];
            }
        }

    //-----------------------------------------------------------------------------

    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> Q;
            Q = new Queue<int>();

            Console.WriteLine("Executing Circular Array Implementation of Queue");

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
