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
        static void Print(string s)
        {
            for(int i=0; i<9; i += 3)Console.WriteLine(s[i] + " " + s[i + 1] + " " + s[i + 2]);
        }

        static void swap(string str, int indexOneToSwap, int indexTwoToSwap)
        {
            Console.WriteLine("Passed string = " + str);
            char[] tempState = str.ToCharArray();
            char temp = tempState[indexOneToSwap];
            tempState[indexOneToSwap] = tempState[indexTwoToSwap];
            tempState[indexTwoToSwap] = temp;
            str = new string(tempState);
            Debug.Assert(str != null);
            Console.WriteLine("New string = " + str);
        }

        static bool isGoalState(string s)
        {
            if (s == "123456780") return true;
            return false;
        }


        public static bool DFS(string mainState, int spaceIndex, Queue<string> history, Queue<string> uniqueStateStracker)
        {
            


            //Console.WriteLine(mainState);
            uniqueStateStracker.Enqueue(mainState);

            //Printing uniqueStateTracker's contained strings
            Console.WriteLine("Unique states visisted starts here");
            foreach (string ss in uniqueStateStracker) Print(ss);
            Console.WriteLine("Unique states visisted Ends here");

            if (isGoalState(mainState))
            {
                history.Enqueue(mainState);
                return true;
            }
            bool flag = false;

            //Attempting UP movement
            if (spaceIndex > 2)
            {
                Console.WriteLine("Entering node 1");
                string temp = mainState;
                swap(temp, spaceIndex, spaceIndex - 3);
                if (!uniqueStateStracker.Contains(temp))
                {
                    flag = DFS(temp, spaceIndex,history, uniqueStateStracker);
                    if (flag) { history.Enqueue(temp); return true; }
                }
            }
            //Attempting DOWN movement
            if (spaceIndex < 6)
            {
                Console.WriteLine("Entering node 2");
                string temp = mainState;
                swap(temp, spaceIndex, spaceIndex +3);
                if (!uniqueStateStracker.Contains(temp))
                {
                    flag = DFS(temp, spaceIndex, history, uniqueStateStracker);
                    if (flag) { history.Enqueue(temp); return true; }
                }
            }

            //Attempting LEFT movement
            if (spaceIndex % 3 > 0)
            {
                Console.WriteLine("Entering node 3");
                string temp = mainState;
                swap(temp, spaceIndex, spaceIndex - 1);
                if (!uniqueStateStracker.Contains(temp))
                {
                    flag = DFS(temp, spaceIndex, history, uniqueStateStracker);
                    if (flag) { history.Enqueue(temp); return true; }
                }
            }

            //Attempting RIGHT movement
            if (spaceIndex % 3 < 2)
            {
                Console.WriteLine("Entering node 4");
                string temp = mainState;
                swap(temp, spaceIndex, spaceIndex +1);
                if (!uniqueStateStracker.Contains(temp))
                {
                    flag = DFS(temp, spaceIndex, history, uniqueStateStracker);
                    if (flag) { history.Enqueue(temp); return true; }
                }
            }

            return false;
        }

        static public void solveByRecursiveDFSImplementation(byte[] array)
        {
            int spaceIndex=0;

            new Random().Shuffle(array);
            int inv_count = inversionCountingClass.getInversionCount(array);

            while (inv_count % 2 != 0)
            {
                new Random().Shuffle(array);
                inv_count = inversionCountingClass.getInversionCount(array);
            }

            Console.WriteLine("Number of inversions = " + inv_count);

            //Converting array to string
            string s = "";
            for (int i = 0; i < 9; i++) s += array[i];
            Console.WriteLine(s);
            Print(s);

            //Finding spaceIndex
            spaceIndex = s.IndexOf("0");

            Queue<string> solution = new Queue<string>();
            Queue<string> uniqueStateStracker = new Queue<string>();


            //Preconditions testing for DFS
            Debug.Assert(s != null);

            //Start DFS solution
            DFS(s, spaceIndex, solution,uniqueStateStracker);

            //Postconditions for DFS
            Debug.Assert(solution != null);

            //Reversing recursive entries
            solution.Reverse();

            //Printing solution
            foreach(string str in solution)
            {
                Print(str);
            }
        }

    }
}
