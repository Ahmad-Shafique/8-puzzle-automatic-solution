using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace _8_Puzzle_From_Java
{
    class DFS_solution
    {
        
        public static void swap(byte[,] arr, int iIndex, int jIndex, int newIIndex , int newJIndex)
        {
            byte temp = arr[iIndex, jIndex];
            arr[iIndex, jIndex] = arr[newIIndex, newJIndex];
            arr[newIIndex, newJIndex] = temp;
            Debug.Assert(arr[iIndex, jIndex] != 0);
        }

        static bool isGoalState(byte[,] array)
        {
            Debug.Assert(array != null);
            byte[] g = { 1,2,3,4,5,6,7,8,0};
            int index = 0;
            for(int i=0; i<3; i++)
            {
                for(int j=0; j<3; j++)
                {
                    if (array[i, j] != g[index++]) return false;
                }
            }
            return true;
        }

        public static bool DFS(byte[,] array,int spaceI,int spaceJ, Queue<byte[,]> history, Queue<byte[,]> uniqueStateStracker)
        {
            Console.WriteLine("Checking unique state tracker");
            foreach(byte[,] arr in uniqueStateStracker)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Console.Write(arr[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine("Ending Unique state tracker here\n");

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine();
            }


            uniqueStateStracker.Enqueue(array);

            if (isGoalState(array))
            {               
                history.Enqueue(array);
                return true;
            }
            bool flag = false;
            //Attempting down movement
            if (spaceI + 1 <= 2)
            {
                Console.WriteLine("Entering node 1");
                byte[,] temp = array.Clone() as byte[,];
                swap(temp,spaceI,spaceJ,spaceI+1,spaceJ);
                if (!uniqueStateStracker.Contains(temp))
                {
                    flag = DFS(temp, spaceI + 1, spaceJ, history,uniqueStateStracker);
                    if (flag) { history.Enqueue(array); return true; }
                }
            }
            //Attempting Left movement
            if (spaceJ - 1 >= 0)
            {
                Console.WriteLine("Entering node 2");

                byte[,] temp = array.Clone() as byte[,];
                swap(temp, spaceI, spaceJ, spaceI, spaceJ-1);
                if (!uniqueStateStracker.Contains(temp))
                {
                    flag = DFS(temp, spaceI, spaceJ-1, history,uniqueStateStracker);
                    if (flag) { history.Enqueue(array); return true; }
                }
            }

            //Attempting Right movement
            if (spaceJ + 1 <= 2)
            {
                Console.WriteLine("Entering node 3");

                byte[,] temp = array.Clone() as byte[,];
                swap(temp, spaceI, spaceJ, spaceI, spaceJ+1);
                if (!uniqueStateStracker.Contains(temp))
                {
                    flag = DFS(temp, spaceI, spaceJ+1, history,uniqueStateStracker);
                    if (flag) { history.Enqueue(array); return true; }
                }
            }

            //Attempting Up movement
            if (spaceI -1 >= 0)
            {
                Console.WriteLine("Entering node 4");

                byte[,] temp = array.Clone() as byte[,];
                swap(temp, spaceI, spaceJ, spaceI - 1, spaceJ);
                if (!uniqueStateStracker.Contains(temp))
                {
                    flag = DFS(temp, spaceI - 1, spaceJ, history,uniqueStateStracker);
                    if (flag) { history.Enqueue(array); return true; }
                }
            }

            return false;
        }


        static public void solveByRecursiveDFSImplementation(byte[] array)
        {
            //Shuffling to return random array
            new Random().Shuffle(array);
            int inv_count = inversionCountingClass.getInversionCount(array);
            //Looking for solvable array
            while (inv_count % 2 != 0)
            {
                new Random().Shuffle(array);

                inv_count = inversionCountingClass.getInversionCount(array);

            }
            Console.WriteLine("NumberOfInversions = "+inv_count);

            //Casting the 1D array to a 2D array
            int array_index = 0;
            byte[,] mainState = new byte[3,3];
            int spaceI=0, spaceJ=0;
            for(int i=0; i<3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mainState[i,j] = array[array_index++];
                    if(mainState[i,j] == 0)
                    {
                        spaceI = i;
                        spaceJ = j;
                    }
                }
            }

            //Printing the new 2D array
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(mainState[i,j] + " ");
                }
                Console.WriteLine();
            }


            Queue<byte[,]> history = new Queue<byte[,]>();
            Queue<byte[,]> uniqueStateStracker = new Queue<byte[,]>();


            DFS(mainState, spaceI, spaceJ, history, uniqueStateStracker);

            history.Reverse();

            Console.WriteLine("Number of steps = " + history.Count);
            while (history.Count > 0)
            {
                byte[,] tempArr = history.Dequeue();
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Console.Write(tempArr[i,j] + " ");
                    }
                    Console.WriteLine();
                }
            }


        }
    }
}
