using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8_Puzzle_From_Java
{
    class BFS_solution
    {

        static void Print(string s)
        {
            for (int i = 0; i < 9; i += 3) Console.WriteLine(s[i] + " " + s[i + 1] + " " + s[i + 2]); Console.WriteLine();
        }

        static void swap(ref string str, int indexOneToSwap, int indexTwoToSwap)
        {
            //Console.WriteLine("Passed string = " + str);
            char[] tempState = str.ToCharArray();
            char temp = tempState[indexOneToSwap];
            tempState[indexOneToSwap] = tempState[indexTwoToSwap];
            tempState[indexTwoToSwap] = temp;
            str = new string(tempState);
            Debug.Assert(str != null);
            //Console.WriteLine("New string = " + str);
        }

        static bool isGoalState(string s)
        {
            if (s == "123456780") return true;
            return false;
        }

        static bool checkIfStringExistsInList(ref string str, ref List<string> uniqueStateStracker)
        {
            foreach (string s in uniqueStateStracker) if (str == s)
                { //Console.WriteLine("Found equal");
                  //Console.WriteLine("looking for = "+ str);
                  //Console.WriteLine("Found = "+s);
                  //Console.WriteLine("Found match");

                    //Console.WriteLine("Contained unique states");
                    //foreach (string ss in uniqueStateStracker) Print(ss);

                    return true;


                }
            return false;
        }

        static string moveUP(string mainState)
        {
            string temp = mainState;
            int spaceIndex = temp.IndexOf("0");
            if (spaceIndex > 2)
            {
                swap(ref temp, spaceIndex, spaceIndex - 3);
                //Console.WriteLine("0 going up");
                //Console.WriteLine("new mainstate = ");
                return temp;
            }
            return null;
        }

        static string moveDOWN(string mainState)
        {
            string temp = mainState;
            int spaceIndex = temp.IndexOf("0");
            if (spaceIndex < 6)
            {
                swap(ref temp, spaceIndex, spaceIndex + 3);
                //Console.WriteLine("0 going Down");
                //Console.WriteLine("new mainstate = ");
                return temp;
            }
            return null;
        }

        static string moveLEFT(string mainState)
        {
            string temp = mainState;
            int spaceIndex = temp.IndexOf("0");
            if (spaceIndex % 3 > 0)
            {
                swap(ref temp, spaceIndex, spaceIndex - 1);
                //Console.WriteLine("0 going Left");
                //Console.WriteLine("new mainstate = ");
                return temp;
            }
            return null;
        }

        static string moveRIGHT(string mainState)
        {
            string temp = mainState;
            int spaceIndex = temp.IndexOf("0");
            if (spaceIndex % 3 < 2)
            {
                swap(ref temp, spaceIndex, spaceIndex + 1);
                //Console.WriteLine("0 going Left");
                //Console.WriteLine("new mainstate = ");
                return temp;
            }
            return null;
        }



        public static bool DFS(ref string mainState, ref Queue<string> solution)
        {
            Debug.Assert(mainState != null);
            //Console.WriteLine("new mainstate : "  );
            //Print(mainState);
            int spaceIndex = mainState.IndexOf("0");
            //Console.WriteLine("0 at  " + spaceIndex);



            if (isGoalState(mainState))
            {
                solution.Enqueue(mainState);
                return true;
            }

            Queue<string> executionStack = new Queue<string>();
            List<string> uniqueStateTracker = new List<string>();

            executionStack.Enqueue(mainState);

            while (!isGoalState(mainState))
            {
                string poppedMainState = executionStack.Dequeue();

                if (isGoalState(poppedMainState))
                {
                    solution.Enqueue(poppedMainState);
                    return true;
                }
                //creating child states
                string up = moveUP(poppedMainState);
                string down = moveDOWN(poppedMainState);
                string left = moveLEFT(poppedMainState);
                string right = moveRIGHT(poppedMainState);

                //Pushing unique child states into stack
                if (up != null || down != null || left != null || right != null)
                {
                    solution.Enqueue(poppedMainState);
                    uniqueStateTracker.Add(poppedMainState);
                    Print(poppedMainState);
                    if (up != null && (!(checkIfStringExistsInList(ref up, ref uniqueStateTracker)))) executionStack.Enqueue(up);
                    if (down != null && (!(checkIfStringExistsInList(ref down, ref uniqueStateTracker)))) executionStack.Enqueue(down);
                    if (left != null && (!(checkIfStringExistsInList(ref left, ref uniqueStateTracker)))) executionStack.Enqueue(left);
                    if (right != null && (!(checkIfStringExistsInList(ref right, ref uniqueStateTracker)))) executionStack.Enqueue(right);
                }

            }
            return false;
        }

        static public void solveByIterativeBFSImplementation(byte[] array)
        {

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

            //string s = "368124057";
            Print(s);

            Queue<string> solution = new Queue<string>();


            //Preconditions testing for DFS
            Debug.Assert(s != null);

            //Start DFS solution
            DFS(ref s, ref solution);

            //Postconditions for DFS
            Debug.Assert(solution != null);

            //Printing solution
            Console.WriteLine("Solution being printed : ::::::::::::::::::::::::::::::");
            foreach (string str in solution)
            {
                Print(str);
            }
        }

    }
}

