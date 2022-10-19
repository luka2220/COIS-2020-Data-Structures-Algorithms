/*
COIS-2020 
Professor Brian Patrick
Assignment 1 - Part A - Crossword Puzzle

Contributors:
    Dylan Brosseau - 0724989
    Luka Piplica - 0748533
    Opeyemi Okunmuyide - 0736535
*/

using System;
public enum TColor { WHITE, BLACK }; // Value type for square color

// Sqaure class - Objects for individual squares in the grid
// 1 Constrcutor : Squar()
public class Square
{
    // Public properties 
    public TColor Color { get; set; }   // Square color (white or black)
    public int Number { get; set; }    // Sqaure clue number


    //Constructor that sets color to white and number to -1
    public Square()
    {
        Color = TColor.WHITE;
        Number = -1;
    }
}

//  Puzzle Class
//  Public methods: Initialize(M), Number(), PrintClues(), PrintGrid(), Symmetric()
//  1 Constuctor: Puzzle(N)

public class Puzzle
{
    //private data members
    private Square[,] grid;  // The 2D array of type Square representing the crossword grid
    private int N;  //The length or height of the grid


    //Creates NxN grid of WHITE squares -- Puzzle constructor
    public Puzzle(int N)
    {
        grid = new Square[N, N];

        // loops through the grid and assigns each index a new instance of the Square class
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                grid[i, j] = new Square();
            }
        }
    }

    // Randomly initialize the crossword grid with M black squares
    public void Initialize(int M)

    {

        int N = grid.GetLength(0);

        // If M > # of black squares, an error message is printed and the program is exited
        if (M > N * N) { Console.WriteLine("The number of black squares exceeds the number of squares on the grid."); Environment.Exit(1); }



        Random random = new Random(); // creating instance of the Random class

        int counter = 0; // variable to track number of succesfull black squares

        while (counter < M) // while there are not M black sqaures
        {
            int num1 = random.Next(0, N);
            int num2 = random.Next(0, N);
            if (grid[num1, num2].Color == TColor.WHITE) // checking if the random sqaure is white. 
            {
                grid[num1, num2].Color = TColor.BLACK; // convert color from white to black
                counter++; // increase counter by one
            }
        }



    }

    // loops through the grid, checking if each square can have a clue number
    // checks whether there is a border/black square above and a white square below or border/black square on the left and white square to the right
    // this method checks the outer rows/columns seperately to avoid IndexOutOfRange exceptions when checking for black squares
    public void Number()
    {
        int N = grid.GetLength(0); // grid dimension
        int currentNumber = 1; // current number

        //loop through grid   
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                // if this square is white (ignore all black squares)
                if (grid[i, j].Color == TColor.WHITE)
                {

                    if (i == N - 1 && j == N - 1) { continue; } // The bottom right square cannot have a clue and may cause exceptions if it checks further conditions


                    // if current square is in the right-most column
                    else if (j == N - 1 && i != N - 1)
                    {


                        // If in the top right corner or the square above is black 
                        if (i == 0 || grid[i - 1, j].Color == TColor.BLACK)
                        {
                            // If the square below is white
                            if (grid[i + 1, j].Color == TColor.WHITE)

                            {
                                grid[i, j].Number = currentNumber;
                                currentNumber++;
                            }
                        }
                    }



                    // if current square is in the bottom row
                    else if (i == N - 1 && j != N - 1)
                    {

                        // If in bottom left square or a black square is to the left
                        if (j == 0 || grid[i, j - 1].Color == TColor.BLACK)
                        {
                            //If there is a white square to the right
                            if (grid[i, j + 1].Color == TColor.WHITE)
                            {
                                grid[i, j].Number = currentNumber;
                                currentNumber++;
                            }

                        }
                    }


                    //if the current square is not in either bottom row or right most column
                    else
                    {
                        // if there is either a white square below or to the right
                        if (grid[i + 1, j].Color == TColor.WHITE || grid[i, j + 1].Color == TColor.WHITE)
                        {

                            //if the square is in either left-most or top column/row
                            if (j == 0 || i == 0)

                            {

                                if ((j == 0 && grid[i, j + 1].Color == TColor.WHITE) || i == 0 && grid[i + 1, j].Color == TColor.WHITE)
                                {
                                    grid[i, j].Number = currentNumber;
                                    currentNumber++;
                                }
                                else if (j == 0 && i != 0 && grid[i - 1, j].Color == TColor.BLACK && grid[i + 1, j].Color == TColor.WHITE)
                                {

                                    grid[i, j].Number = currentNumber;
                                    currentNumber++;
                                }
                                else if (i == 0 && j != 0 && grid[i, j - 1].Color == TColor.BLACK && grid[i, j + 1].Color == TColor.WHITE)
                                {
                                    grid[i, j].Number = currentNumber;
                                    currentNumber++;
                                }
                            }

                            //for squares not in an outer row/column
                            else if ((grid[i - 1, j].Color == TColor.BLACK && grid[i + 1, j].Color == TColor.WHITE) || (grid[i, j - 1].Color == TColor.BLACK && grid[i, j + 1].Color == TColor.WHITE))
                            {

                                grid[i, j].Number = currentNumber;
                                currentNumber++;
                            }

                        }
                    }



                }
            }
        }
    }

    // This method loops through grid first checking if the square is eligible to be both a down clue and an across clue
    // Creates two lists and assigns each clue to its corresponding list and finally prints them
    public void PrintClues()
    {
        int N = grid.GetLength(0);

        //creating arrays to hold clue numbers
        int[] across = new int[N * N];
        int[] down = new int[N * N];

        // variables to store current index of the two arrays while looping through the grid
        int indexAcross = 0;
        int indexDown = 0;

        bool is2Way; // boolean to know if the current square is eligible to be a 2 way clue


        //loop through grid
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                is2Way = false; //resets bool to false 


                if (grid[i, j].Number != -1) // if this sqaure has a clue
                {


                    //determining if the current square is a 2way

                    // if the square is in the top left, it is automatically eligible   
                    if (j == 0 && i == 0) { is2Way = true; }

                    // if in first row and square to the left is black
                    else if (i == 0)
                    {
                        if (grid[i, j - 1].Color == TColor.BLACK) { is2Way = true; }
                    }

                    // if in first column and square above is black
                    else if (j == 0)
                    {
                        if (grid[i - 1, j].Color == TColor.BLACK) { is2Way = true; }
                    }

                    // if not in outer row/column and is a 2way
                    else if (grid[i - 1, j].Color == TColor.BLACK && grid[i, j - 1].Color == TColor.BLACK) { is2Way = true; } // if both above and to the left is black



                    //now adding numbers to the arrays

                    // if square is in right most column, assign to down
                    if (j == N - 1)
                    {
                        down[indexDown] = grid[i, j].Number;
                        indexDown++;
                    }

                    // if square is in bottom row, assign to across
                    else if (i == N - 1)
                    {
                        across[indexAcross] = grid[i, j].Number;
                        indexAcross++;
                    }

                    //all other squares
                    else
                    {
                        // if the square goes both across and down, add to both lists
                        if (grid[i + 1, j].Color == TColor.WHITE && grid[i, j + 1].Color == TColor.WHITE && is2Way == true)
                        {

                            down[indexDown] = grid[i, j].Number;
                            indexDown++;

                            across[indexAcross] = grid[i, j].Number;
                            indexAcross++;
                        }
                        //if the square has a white square below and either a border or black square above
                        else if (grid[i + 1, j].Color == TColor.WHITE && (i == 0 || grid[i - 1, j].Color == TColor.BLACK))//if the square has a white square below and either a border or black square above
                        {
                            down[indexDown] = grid[i, j].Number;
                            indexDown++;
                        }
                        else // if the square has a number but is neither a 2way nor down, which just leaves across
                        {
                            across[indexAcross] = grid[i, j].Number;
                            indexAcross++;
                        }
                    }
                }
            }
        }


        //printing the lists (only indexes that have been filled)
        Console.WriteLine("Across: ");
        int b = 0;
        while (across[b] != 0)
        {
            Console.WriteLine(across[b].ToString());
            b++;
        }

        Console.WriteLine("Down: ");
        int c = 0;
        while (down[c] != 0)
        {
            Console.WriteLine(down[c].ToString());
            c++;
        }
        Console.WriteLine();
    }

    // Prints the puzzle grid with X's representing black sqaures, _'s representing white sqaures and numbers representing white squares with clues.
    public void PrintGrid()
    {
        int N = grid.GetLength(0);

        //loops through the grid
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                // if the square is numbered
                if (grid[i, j].Number > 0) { Console.Write(String.Format("{0, 5}", " " + grid[i, j].Number.ToString() + " |")); }


                // if square is black
                else if (grid[i, j].Color == TColor.BLACK) { Console.Write(String.Format("{0, 5}", " X |")); }


                // if sqaure is white and not numbered
                else { Console.Write(String.Format("{0, 5}", "   |")); }

            }
            Console.WriteLine();
            for (int b = 0; b < N * 5; b++) { Console.Write("-"); }
            Console.WriteLine();
        }
    }

    // loops through half the grid row by row, comparing with the other half to check for symmetry
    // if a discrepency is found, the function returns false, otherwise it returns true
    public bool Symmetric()
    {
        int N = grid.GetLength(0);
        int end = N - 1; //int of the last index (to simplify code)

        int i = 0;
        int j = 0;
        while (i <= N / 2)
        {
            while (j <= end)
            {
                // if the opposite square is not the same color return false
                if (grid[i, j].Color != grid[end - i, end - j].Color) { return false; }
                j++;
            }
            i++;
            j = 0;
        }

        return true;
    }

}

//testing class
public class Test
{
    public static void Main()
    {
        Puzzle puz = new Puzzle(5);
        puz.Initialize(5);
        puz.Number();
        puz.PrintClues();
        puz.PrintGrid();
        bool s = puz.Symmetric();
        if (s == true)
            Console.WriteLine("\n Symmetric: true");
        else
            Console.WriteLine("\n Symmetric: false");



        Console.ReadLine();

    }
}

