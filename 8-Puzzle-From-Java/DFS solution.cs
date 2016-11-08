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
            for(int i=0; i<9; i += 3)Console.WriteLine(s[i] + " " + s[i + 1] + " " + s[i + 2]); Console.WriteLine();
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
            if (s == "628354107") return true;
            return false;
        }

        static bool checkIfStringExistsInList(ref string str,ref List<string> uniqueStateStracker)
        {
            foreach (string s in uniqueStateStracker) if (str == s) { //Console.WriteLine("Found equal");
                    //Console.WriteLine("looking for = "+ str);
                    //Console.WriteLine("Found = "+s);
                    //Console.WriteLine("Found match");

                    //Console.WriteLine("Contained unique states");
                    //foreach (string ss in uniqueStateStracker) Print(ss);

                        return true;

                
                        }
            return false; 
        }

        public static bool DFS(ref string mainState, ref Queue<string> solution, ref List<string> history)
        {
            Debug.Assert(mainState != null);
            //Console.WriteLine("new mainstate : "  );
            //Print(mainState);
            int spaceIndex = mainState.IndexOf("0");
            //Console.WriteLine("0 at  " + spaceIndex);

            //Console.WriteLine(mainState);
            history.Add(mainState);

            //Printing uniqueStateTracker's contained strings
            //Console.WriteLine("Unique states visisted starts here");
            //foreach (string ss in uniqueStateStracker) Print(ss);
            //Console.WriteLine("Unique states visisted Ends here");

            if (isGoalState(mainState))
            {
                solution.Enqueue(mainState);
                return true;
            }

            bool flag = false;
            
            //Attempting UP movement
            if (spaceIndex > 2 && !flag)
            {
                
                string temp = mainState;
                swap(ref temp, spaceIndex, spaceIndex - 3);
                //Console.WriteLine("new mainstate = " + temp);
                
                //bool status = uniqueStateStracker.Contains(temp);
                //Console.WriteLine(status.ToString());

                if (!checkIfStringExistsInList(ref temp,ref history))
                {
                    Console.WriteLine("0 going up");
                    Console.WriteLine("new mainstate = ");
                    Print(temp);
                    flag = DFS(ref temp, ref solution, ref history);
                    if (flag)
                    { solution.Enqueue(temp); return true; }
                }
            }
            //Attempting DOWN movement
            if (spaceIndex < 6 && !flag)
            {
                
                string temp = mainState;
                swap(ref temp, spaceIndex, spaceIndex +3);
                //Console.WriteLine("new mainstate = " + temp);
                
                //bool status = uniqueStateStracker.Contains(temp);
                //Console.WriteLine(status.ToString());

                if (!checkIfStringExistsInList(ref temp,ref history))
                {
                    Console.WriteLine("0 going down");
                    Console.WriteLine("new mainstate = ");
                    Print(temp);
                    flag = DFS(ref temp,ref solution, ref history);
                    if (flag)
                    { solution.Enqueue(temp); return true; }
                }
            }

            //Attempting LEFT movement
            if (spaceIndex % 3 > 0 && !flag)
            {
                
                string temp = mainState;
                swap(ref temp, spaceIndex, spaceIndex - 1);

                //bool status = uniqueStateStracker.Contains(temp);
                //Console.WriteLine(status.ToString());
                //Console.WriteLine("new mainstate = " + temp);
                
                if (!checkIfStringExistsInList(ref temp,ref  history))
                {
                    Console.WriteLine("0 going left");
                    Console.WriteLine("new mainstate = ");
                    Print(temp);
                    flag = DFS(ref temp,ref solution, ref history);
                    if (flag)
                    {
                        solution.Enqueue(temp); return true; }
                }
            }

            //Attempting RIGHT movement
            if (spaceIndex % 3 < 2 && !flag)
            {
               
                string temp = mainState;
                swap(ref temp, spaceIndex, spaceIndex +1);
                //Console.WriteLine("new mainstate = " + temp);
                
                //bool status = uniqueStateStracker.Contains(temp);
                //Console.WriteLine(status.ToString());

                if (!checkIfStringExistsInList(ref temp,ref history))
                {
                    Console.WriteLine("0 going right");
                    Console.WriteLine("new mainstate = ");
                    Print(temp);
                    flag = DFS(ref temp,ref solution,ref history);
                    if (flag)
                    {
                        solution.Enqueue(temp); return true;
                    }
                }
            }

            if (flag)
            {
                solution.Enqueue(mainState);
                return true;
            }
            return false;
        }

        static public void solveByRecursiveDFSImplementation(byte[] array)
        {
            /*
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
            */
            string s = "368124057";
            //Console.WriteLine(s);
            Print(s);

            Queue<string> solution = new Queue<string>();
            List<string> history = new List<string>() ;


            //Preconditions testing for DFS
            Debug.Assert(s != null);

            //Start DFS solution
            DFS(ref s,  ref solution,ref history);

            //Postconditions for DFS
            Debug.Assert(solution != null);

            //Reversing recursive entries
            solution.Reverse();

            //Printing solution
            Console.WriteLine("Solution being printed : ::::::::::::::::::::::::::::::");
            foreach(string str in solution)
            {
                Print(str);
            }
        }

    }
}
