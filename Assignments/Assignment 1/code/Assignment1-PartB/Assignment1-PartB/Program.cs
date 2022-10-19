using System;
using System.Collections.Generic;

public class MyString
{
    private class Node
    {
        public char item { get; set; }
        public Node next { get; set; }

        // Constructor (2 marks)

        public Node(char item, Node next = null)
        {
            this.item = item;
            this.next = next;
        }
    }

    private Node front; // Reference to the first (header) node
    private int length; // Number of characters in MyString

    // Initialize with a header node an instance of MyString to the given character array A (4 marks)
    public MyString(char[] A)
    {

        length = A.Length;
        if (A.Length == 0) { this.front = null; }
        else
        { // setting the length of the linkedlist to the number of elements in A
            front = new Node(A[length - 1]); // setting head node to last index in A

            // Looping through all elements in A to create nodes for each element 
            for (int i = length - 2; i >= 0; i--)
            {
                Node nextNode = front;  // setting previous front to next for current node creation
                front = new Node(A[i], nextNode); // setting front to the new node, and nextNode to previous node in list
            }
        }
    }

    // Using a stack, reverse this instance of MyString (6 marks)
    public void Reverse()
    {
        Stack<char> S = new Stack<char>();

        // Setting front node to be the first node item on the bottom of the stack
        Node currentNode = front;

        // Looping through every node in list to add to S stack
        for (int i = 0; i < length; i++)
        {
            // Pushing the current node item onto the stack
            S.Push(currentNode.item);

            // Setting currentNode to the current nodes next node
            currentNode = currentNode.next;
        }

        // Displaying reversed items
        Console.WriteLine("Printing reversed items");

        // Looping through each item in reversed stack
        foreach (var item in S)
        {
            Console.Write("{0}", item);
        }

        Console.WriteLine();
    }

    // Return the index of the first occurrence of c in this instance; otherwise -1 (4 marks)
    public int IndexOf(char c)
    {
        // Setting current node to the first node in list
        Node currentNode = front;

        // Looping through all the nodes in the list
        for (int i = 0; i < length; i++)
        {
            // Return the index number if the current node item is c
            if (currentNode.item == c)
            {
                return i;
            }

            // Setting the current node to the next node in the list
            currentNode = currentNode.next;
        }

        // Retturning -1 if c is not in the list
        return -1;
    }

    // Remove all occurrences of c from this instance (4 marks)
    public void Remove(char c)
    {
        // Current and prev node for algorithm
        Node currentNode = front;
        Node prevNode = null;

        // Looping through to check front node in list 
        while (currentNode != null && currentNode.item == c)
        {
            front = currentNode.next;
            currentNode = front;

            length--;
        }

        // Checking all other nodes besides front node
        while (currentNode != null)
        {
            while (currentNode != null && currentNode.item != c)
            {
                prevNode = currentNode;
                currentNode = currentNode.next;
            }

            // Exits loop if there are no occurances of c in list  
            if (currentNode == null)
            {
                break;
            }

            prevNode.next = currentNode.next;

            currentNode = prevNode.next;

            length--;
        }
    }

    // Return true if obj is both of type MyString and the same as this instance;
    // otherwise false (6 marks)
    public override bool Equals(object obj)
    
    {
        if (obj == null || !(obj is MyString)) { return false; }
        MyString copy = (MyString)obj;
        if (this.length != copy.length) { return false; }
        if (this.front == null && copy.front == null) { return true; }
        if (this.front== null || copy.front == null) { return false; }
        Node thisNode = this.front;
        Node copyNode = copy.front;
        while (thisNode != null && copyNode != null) 
        {
            if (thisNode.item != copyNode.item) { return false; }
            thisNode = thisNode.next; 
            copyNode = copyNode.next;
        }
        return true;
    }

    // Print out this instance of MyString (3 marks)
    public void Print()
    {
        Node currentNode = front;

        for (int i = 1; i <= length; i++)
        {
            Console.Write("{0}", currentNode.item);

            currentNode = currentNode.next;
        }

        Console.WriteLine();
    }
}

class Program
{
    static void Main(String[] args)
    {
        // Variable to store the operation code
        char menuOperationCode;
        // Reference to MyString
        MyString stringList;

        MyString[] instanceList = new MyString[10];

        // Menu title
        Console.WriteLine("Assignment 1 Part B Menu");

        // Prompting user to input characters for MyString
        Console.Write("Enter a series of characters (i.e abcd) to continue: ");
        string inputSting = Console.ReadLine();

        // Variable for list of characters to pass to MyString
        char[] listOfInputCharacters;

        // Converting string input to char array for MyString instance
        listOfInputCharacters = inputSting.ToCharArray();

        // Creating an instance of MyString
        stringList = new MyString(listOfInputCharacters);
        instanceList[0] = stringList;

        // Prompting user to input code for MyString operation
        Console.WriteLine("Enter a code for MyString operation:\n 'p' or 'P' to print out the current instance of MyString \n 'r' or 'R' to reverse the current instance of MyString \n 'i' or 'I' to get the index of the first occurance of a character \n 'd' or 'D' to remove all occurances of a character \n 'e' or 'E' to check if an object is of type MyString and the same as the current instance \n 'q', or 'Q' to quit the program \n 'n' or 'N' to create a new instance \n 'l' or 'L' to show the current list of instances and choose a different instance ");

        // Reading and storing the operation code
        menuOperationCode = Convert.ToChar(Console.ReadLine());

        // Loop will run as along a 'q' or 'Q' was not inputted 
        while (char.ToUpper(menuOperationCode) != 'Q')
        {
            // Will execute MyString.Print() method 
            if (char.ToUpper(menuOperationCode) == 'P')
            {
                stringList.Print();
            }

            // Will execute MyString.Reverse() method
            if (char.ToUpper(menuOperationCode) == 'R')
            {
                stringList.Reverse();
            }

            // Will execute MyString.IndexOf(char c) and prompt user for a character to pass to the method
            if (char.ToUpper(menuOperationCode) == 'I')
            {
                Console.WriteLine("Enter a char to find index in list");
                char inputChar = Convert.ToChar(Console.ReadLine());

                Console.WriteLine("Result: {0}", stringList.IndexOf(inputChar));
            }

            // Will execute MyString.Remove(char c) and prompt user for a character to pass to the method
            if (char.ToUpper(menuOperationCode) == 'D')
            {
                Console.WriteLine("Enter a character to remove all instances in the list");
                char inputChar = Convert.ToChar(Console.ReadLine());

                stringList.Remove(inputChar);
            }

            // Will execute MyString.Equals(object obj) and promt user for an object to pass to the method
            if (char.ToUpper(menuOperationCode) == 'E')
            {
               Console.WriteLine("Enter another series of characters (i.e abcd)"); 
                
               
                string newInputString = Console.ReadLine();

                char[] newCharArray = newInputString.ToCharArray();

                MyString newString = new MyString(newCharArray);

                if (stringList.Equals(newString) == true) { Console.WriteLine("true"); }
                else { Console.WriteLine("false"); }
                

            }

            if(char.ToUpper(menuOperationCode) == 'N')
            {
                
                Console.WriteLine("Enter another series of characters (i.e abcd)");
                string newInputString = Console.ReadLine();

                char[] newCharArray = newInputString.ToCharArray();

                MyString newString = new MyString(newCharArray);
                int i = 0;
                while (instanceList[i] != null ) { i++; }
                instanceList[i] = newString;
            }
            if (char.ToUpper(menuOperationCode) == 'L')
            {
                int i = 0;
                while (instanceList[i] != null) { instanceList[i].Print(); i++; }
                Console.WriteLine();
                Console.Write("Enter the index of the instance you would like to choose.");
                int index = Convert.ToInt32(Console.ReadLine()) - 1;
                stringList = instanceList[index];
            }


            // Prompting user to input code for MyString operation
            Console.Write("Enter another code: ");

            menuOperationCode = Convert.ToChar(Console.ReadLine()); // Reading and storing the next menu code
        }

        Environment.Exit(0);
    }
}
