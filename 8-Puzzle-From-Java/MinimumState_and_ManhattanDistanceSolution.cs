using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8_Puzzle_From_Java
{
    class MinimumState_and_Manhattan_OR_EuclideanDistanceSolution
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
            if (s == "012345678") return true;
            return false;
        }

        static bool checkIfStringExistsInList(ref string str, List<string> uniqueStateStracker)
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

        static int EuclideanDistance(int originalIndex, int currentIndex)
        {
            int x1 = originalIndex % 3;
            int y1 = originalIndex / 3;
            int x2 = currentIndex % 3;
            int y2 = currentIndex / 3;
            double sqrDiff1 = Math.Abs(x1 - x2) * Math.Abs(x1 - x2);
            double sqrDiff2 = Math.Abs(y1 - y2) * Math.Abs(y1 - y2);


            return (int)Math.Sqrt((int)sqrDiff1 + (int)sqrDiff2);
        }

        static int ManhattanDistance(int originalIndex, int currentIndex)
        {
            int x1 = originalIndex % 3;
            int y1 = originalIndex / 3;
            int x2 = currentIndex % 3;
            int y2 = currentIndex / 3;
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        static int CalculateMisplacementValues(string str,int mode)
        {
            int totalValue = 0;
            for(int i=0; i<9; i++)
            {
                if(str[i] != i)
                {
                    char temp =(char) i;
                    //Take current index
                    int index = str.IndexOf(temp);
                    if (mode == 1)
                    {
                        totalValue += ManhattanDistance(i, index);
                    }
                    else totalValue += EuclideanDistance(i, index);
                }
            }
            return totalValue;
        }



        public static bool MinStateSolution(string mainState, int choice)
        {
            List<string> history = new List<string>();


            Debug.Assert(mainState != null);
            //Console.WriteLine("new mainstate : "  );
            //Print(mainState);
            int spaceIndex = mainState.IndexOf("0");
            //Console.WriteLine("0 at  " + spaceIndex);



            if (isGoalState(mainState))
            {
                Console.WriteLine("No other steps needed. Already in goal state");
                Print(mainState);
                return true;
            }

            history
                .Add(mainState);


            int iterationNumber = 0;
            while (!isGoalState(mainState))
            {
                Console.WriteLine("iteration : " + iterationNumber++);
               // Console.WriteLine("Main State : ");
                //Print(mainState);
                //Console.WriteLine("Children creation starts");

                //creating child states
                string up = moveUP(mainState);
                string down = moveDOWN(mainState);
                string left = moveLEFT(mainState);
                string right = moveRIGHT(mainState);

                //Calculating minimum number of misplacements among child states
                int upValue=-1, downValue=-1, leftValue=-1, rightValue=-1,min=99999;

                if (up != null && !checkIfStringExistsInList(ref up,history)) { upValue = CalculateMisplacementValues(up, choice); if (upValue < min) min = upValue;
                    //Console.WriteLine("up");Print(up);
                }
                if (down != null && !checkIfStringExistsInList(ref down,history)) { downValue = CalculateMisplacementValues(down, choice); if (downValue < min) min = downValue;
                    //Console.WriteLine("down"); Print(down); 
                }
                if (left != null && !checkIfStringExistsInList(ref left,history)) { leftValue = CalculateMisplacementValues(left, choice); if (leftValue < min) min = leftValue;
                    //Console.WriteLine("left"); Print(left);
                }
                if (right != null && !checkIfStringExistsInList(ref right,history)) { rightValue = CalculateMisplacementValues(right, choice); if (rightValue < min) min = rightValue;
                    //Console.WriteLine("right"); Print(right);
                }
                //Calculation of minimum misplaced state end here

                //Console.WriteLine("children ready");
                //Console.WriteLine("recheck main state : ");
                // Print(mainState);

                //Selecting the successor child 
                if (upValue == min && upValue != -1) { mainState = up; Console.WriteLine("up chosen"); }
                else if (downValue == min && downValue != -1) { mainState = down; Console.WriteLine("down chosen"); }
                else if (leftValue == min && leftValue != -1) { mainState = left; Console.WriteLine("left chosen"); }
                else if (rightValue == min && rightValue != -1) { mainState = right; Console.WriteLine("right chosen"); }
                //successor chosen

                Console.WriteLine("Successor chosen . New main state : ");
                Print(mainState);

                //Victory condition
                if (isGoalState(mainState))
                {
                    Print(mainState);
                    return true;
                }


                history.Add(mainState);
                //if (checkIfStringExistsInList(ref mainState, ref history)) Console.WriteLine("New state in list"); else Console.WriteLine("New state not in list");

                //Console.WriteLine("list size : " + history.Count);
                //Move to next iteration
            }
            return false;
        }

        static public void Solve(byte[] array)
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

            Console.Write("\nSelect the technique to use : \n1 for Manhattan Distance\n2 for Euclidean distance\nAny other key to exit\n");
            string choice;
            choice=Console.ReadLine();
            switch (choice)
            {
                case "1":
                    MinStateSolution(s, 1);
                    break;
                case "2":
                    MinStateSolution(s, 2);
                    break;
                default:
                    Console.WriteLine("Wrong choice");
                    break;
            }

        }



    }
}
