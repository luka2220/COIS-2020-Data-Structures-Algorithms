using Queue;

namespace Queue
{
    // Supporting methods
    public interface IContainer<T> 
    {
        // Resets the queue to empty
        void MakeEmpty();
        // Return true is queue is empty; false otherwise
        bool Empty();
        // Returns the number of items in the queue
        int Size();
    }

    public interface IQueue<T> : IContainer<T>
    {
        // Add an item to the queue
        void Enqueue(T item);
        // Remove the first item of the queue
        void Dequeue();
        // Return the front item of the queue
        T Front();
    }

    public class QueueCircularArray<T> : IQueue<T>
    {
        private T[] A; // linear array of items
        private int front, back; // indecies of front and back items
        private int capacity; // capacity of the queue
        private int count; // number of items in the queue

        // Base Constructor for class
        public QueueCircularArray()
        {
            A = new T[8]; // creating a new linear array with a capacity of 8
            capacity = 8; // setting the capacity of the array to 8
            MakeEmpty(); // resetting the queue
        }

        // Method making the queue empty
        public void MakeEmpty()
        {
            front = 0; // resetting the front index
            back = capacity - 1; ; // resetting the back index
            count = 0; // resetting the count to 0
        }

        // Returns true if the queue is empty; false otherwise
        // Time Complexity: O(1)
        public bool Empty()
        {
            if (count == 0) return true;
            return false;
        }

        // Return the number of items in the queue
        // Time Complexity: O(1)
        public int Size()
        {
            return count;
        }

        // Method for adding a new item to the queue
        public void Enqueue(T item)
        {
            if (count == capacity) DoubleCapacity();

            back = (back + 1) % capacity;
            A[back] = item;
            count++;
        }

        // Doubles the capacity of the queue by 2
        // Time Complexity: O(n)
        private void DoubleCapacity() 
        {
            // current array
            T[] oldA = A; 
            // new array with updated capacity
            A = new T[capacity * 2];
            // old array index
            int index = front;

            for (int i = 0; i < count; i++)
            {
                A[i] = oldA[index];

                index = (index + 1) % capacity;
            }

            front = 0;
            back = count - 1;
            capacity *= 2;
        }

        // Remove the first item in the Queue
        // Time Complexity: O(1)
        public void Dequeue()
        {
            if (count == 0) throw new InvalidOperationException("Queue is empty");
            else 
            {
                // Increasing the index by one with wraparound
                front = (front + 1) % capacity;
                count--;
            }
        }

        // Return the first item in the queue
        // Time Complexity: O(1)
        public T Front()
        {
            if (count == 0) throw new InvalidOperationException("Queue is empty");
            else
            {
                return A[front];
            }
        }

        // Prints out all the items in the queue
        // Time Complexity: O(n)
        public void Print()
        {
            for (int i = front; i < count; i = (i + 1) % capacity)
            {
                Console.WriteLine(A[i]);
            }
        }
    }

    public class QueueLinkedList<T> : IQueue<T>
    {
        // Basic Node representstion object
        private class Node
        { 
            public T item { get; set; }
            public Node next { get; set; }

            public Node(T item, Node next = null)
            {
                this.item = item;
                this.next = next;
            }
        }

        private Node front;     // Front node of queue
        private Node back;      // Back node of queue
        private int count;      // Number of items in queue

        public QueueLinkedList()
        {
            MakeEmpty();
        }

        // MakeEmpty
        // Sets front and back nodes to null; count set to 0
        // Time Complexity: O(1)
        public void MakeEmpty()
        {
            front = back = null;
            count = 0;
        }

        // Empty
        // Returns true if queue is empty; false otherwise
        // Time Complexity: O(1)
        public bool Empty()
        {
            return count == 0;
        }

        // Size
        // Return the amount of items in the queue
        // Time Complexity: O(1)
        public int Size()
        {
            return count;
        }

        // Enqueue
        // Adds a new item to the back of the queue
        // Time Complexity: O(1)
        public void Enqueue(T item)
        {
            Node newNode = new Node(item);

            if (Empty())
            {
                front = back = newNode;
            }
            else {
                back.next = back = newNode;
            }

            count++;
        }

        // Dequeue
        // Removes the front item of the queue; throws exception is queue is empty
        // Time Complexity: O(1)
        public void Dequeue()
        {
            if (Empty()) throw new InvalidOperationException("Queue is empty");
            else {
                front = front.next;
                count--;
            }
        }

        // Front
        // Returns the front element of the queue; throws exception if queue is empty
        // Time Complexity: O(1)
        public T Front()
        {
            return front.item;
        }

        // Print
        // Outputs the items in the queue; throws exception if queue is empty
        // Time Complexity: O(n)
        public void Print()
        {
            Node currentNode = front;

            if (Empty()) throw new InvalidOperationException("Queue is empty");
            else {
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine(currentNode.item);

                    currentNode = currentNode.next;
                }
            }
        }

    }
}


// Class for testing queue data structure
class Test
{ 
    public static void Main(String[] args)
    {
        Console.WriteLine("Testing circular array queue data structure");

        Queue.QueueCircularArray<int> QueueArray = new Queue.QueueCircularArray<int>();

        for (int i = 1; i <= 15; i++) 
        {
            QueueArray.Enqueue(i);
        }

        QueueArray.Print();

        Console.WriteLine("Testing linked list queue data structure");

        Queue.QueueLinkedList<int> QueueList = new Queue.QueueLinkedList<int>();

        for (int i = 1; i <= 30; i++)
        { 
            QueueList.Enqueue(i);
        }

        QueueList.Print();
        
        Console.ReadLine();
    }
}
