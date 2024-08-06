using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonAndBunnyGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] sizeOfMatrix = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int rows = sizeOfMatrix[0];
            int cols = sizeOfMatrix[1];

            char[,] matrix = new char[rows, cols];

            int playerCol = 0;
            int playerRow = 0;

            for(int row = 0; row < matrix.GetLength(0); row++)
            {
                string input = Console.ReadLine();

                for(int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = input[col];

                    if (matrix[row, col] == 'P')
                    {
                        playerRow = row;
                        playerCol = col;

                    }
                }
            }
            string directions = Console.ReadLine().ToLower();

            foreach(var direction in directions)
            {
                int nextRow = 0;
                int nextCol = 0;
                // r l u d
                switch (direction)
                {
                    case 'l':
                        nextCol = -1;
                        break;
                    case 'r':
                        nextCol = 1;
                        break;
                    case 'u':
                        nextRow = -1;
                        break;
                    case 'd':
                        nextRow = 1;
                        break;
                    default:
                        break;

                }

                bool hasWon = false;
                bool isDead = false;

                matrix[playerRow, playerCol] = '.';
                

                if (!IsInside(matrix, playerRow+nextRow, playerCol+nextCol))
                {
                    hasWon = true;
                }
                else
                {
                    playerRow += nextRow;
                    playerCol += nextCol;

                    if (matrix[playerRow, playerCol] == 'B')
                    {
                        isDead = true;
                    }
                    else
                    {
                        matrix[playerRow, playerCol] = 'P';
                    }
                }
               

                List<int[]> bunniesCoordinates = new List<int[]>();
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    for(int col = 0; col < matrix.GetLength(1); col++)
                    {
                        if (matrix[row, col] == 'B')
                        {
                            bunniesCoordinates.Add(new int[] { row, col });
                        }
                    }
                }

                foreach(var bunniesC in bunniesCoordinates)
                {
                    int bunnyRow = bunniesC[0];
                    int bunnyCol = bunniesC[1];
                    if (IsInside(matrix, bunnyRow + 1, bunnyCol))
                    {
                        if (matrix[bunnyRow + 1, bunnyCol] == 'P')
                        {
                            isDead = true;
                        }
                        matrix[bunnyRow + 1, bunnyCol]='B';
                    }
                    if (IsInside(matrix, bunnyRow - 1, bunnyCol))
                    {
                        if (matrix[bunnyRow - 1, bunnyCol] == 'P')
                        {
                            isDead = true;
                        }
                        matrix[bunnyRow - 1, bunnyCol] = 'B';
                    }
                    if (IsInside(matrix, bunnyRow, bunnyCol-1))
                    {
                        if (matrix[bunnyRow, bunnyCol-1] == 'P')
                        {
                            isDead = true;
                        }
                        matrix[bunnyRow, bunnyCol-1] = 'B';
                    }
                    if (IsInside(matrix, bunnyRow, bunnyCol+1))
                    {
                        if (matrix[bunnyRow, bunnyCol+1] == 'P')
                        {
                            isDead = true;
                        }
                        matrix[bunnyRow, bunnyCol+1] = 'B';
                    }
                }

                if (hasWon)
                {
                    Print(matrix);
                    Console.WriteLine($"won: {playerRow} {playerCol}");
                    Environment.Exit(0);
                }
                else if (isDead)
                {
                    Print(matrix);
                    Console.WriteLine($"dead: {playerRow} {playerCol}");
                    Environment.Exit(0);
                }
            }

        }
        private static void Print(char[,] matrix)
        {
            for(int row = 0; row < matrix.GetLength(0); row++)
            {
                for(int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col]);
                }
                Console.WriteLine();
            }
        }
        private static bool IsInside(char[,]board,int row,int col)
        {
            return row >= 0 && row < board.GetLength(0) &&
                col >= 0 && col < board.GetLength(1);
        }
    }
}
