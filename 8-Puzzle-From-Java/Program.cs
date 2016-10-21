using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Priority Queue highly unoptimized. Only for single use
//Can become much more optimized if linked list is used inside priority queue

namespace _8_Puzzle_From_Java
{
    /// <summary>
    /// HashSet coding starts here
    /// </summary>
    //Rudimentary hashset for MisplacementCountedArray states
    class MyHashSet
    {
        UInt64[] hashValues;
        int numberOfElements;
        public MyHashSet()
        {
            hashValues = new UInt64[1000000];
            numberOfElements = new int();
        }

        //Constructs the hashset by adding elements one at a time
        //if the element already exists(searches using binarySearch) returns false
        public bool add(MisplacementCountedArray mca)
        {
            ulong hash = GetHashCode(mca);
            int i = new int();

            if(numberOfElements>1)
            {
                int index = binarySearch(hash, 0, numberOfElements - 1);
                //Console.WriteLine("Index number is " + index);
                bool flag = false;
                if(index == -1)
                {
                    for(i=0; i<numberOfElements; i++)
                    {
                        if (!flag)
                        {
                            if (hash < hashValues[i])
                            {
                                for (int j = numberOfElements; j > i; j--)
                                {
                                    hashValues[j] = hashValues[j - 1];
                                }
                                hashValues[i] = hash;
                                flag = true;
                                numberOfElements++;
                                //Console.WriteLine("Inserted in between " + hash + "--" + numberOfElements);
                            }
                        }
                    }
                    if(i == numberOfElements && !flag)
                    {
                        hashValues[numberOfElements++] = hash;
                        //Console.WriteLine("Inserted at the end " + hash + "--" + numberOfElements);
                    }
                }
            }
            else
            {
                if (numberOfElements == 0) { hashValues[numberOfElements++] = hash;
                    Console.WriteLine("Inserted as first element" +hash+"--"+numberOfElements); }
                else {
                    if (hash > hashValues[0])
                    {
                        hashValues[numberOfElements++] = hash;
                        //Console.WriteLine("Inserted as second element in if " + hash + "--" + numberOfElements);
                    }
                    else { hashValues[numberOfElements++] = hashValues[0]; hashValues[0] = hash;
                        //Console.WriteLine("Inserted as second element in else " + hash + "--" + numberOfElements);
                    }
                    
                }
                return true;
            }
            return false;
        }

        //Searches the record to find existing hash code. Returns the index of hashcode, if existing. Otherwise returns -1
        int binarySearch(ulong value, int init, int fin)
        {
            int mid = (init + fin) / 2;
            if (value > hashValues[fin] || value < hashValues[init]) return -1;
            else if (value == hashValues[mid]) return mid;
            else if (value < hashValues[mid]) return binarySearch(value, init, mid - 1);
            else return binarySearch(value, mid + 1, fin);
        }

        //Generates hash code for the array
        UInt64 GetHashCode(MisplacementCountedArray obj)
        {
            //throw new NotImplementedException();
            UInt64 hc = (UInt64) obj.arrayAccessor.Length;
            for (int i = 0; i < obj.arrayAccessor.Length; ++i)
            {
                hc = (hc * 11 + obj.arrayAccessor[i]);
            }
            //Console.WriteLine("Hash value = " + hc);
            return hc;
        }

        //prints the hash set values
        public void print()
        {
            for (int i = 0; i < numberOfElements; i++)
                Console.WriteLine(hashValues[i]);
        }

        public int getNumberOfElements() { return numberOfElements; }
    }
    /// <summary>
    /// Hashset coding ends here
    /// </summary>






    /// <summary>
    /// Misplacements-Counted-Array starts here
    /// </summary>
    //Arrray with number of misplacements already counted
    class MisplacementCountedArray 
    {
        public int spaceIndex;
        byte numberOfMisplacements;
        byte[] array;
        //Constructor starts here
        public MisplacementCountedArray(byte[] arr, byte[] correctlyPositioned)
        {
            try {
                array = new byte[9];
                byte nom = new byte();
                spaceIndex = new int();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] != correctlyPositioned[i]) nom++;
                    if(arr[i] == 0) spaceIndex = i;
                }
                numberOfMisplacements = nom;
                Array.Copy(arr, array, 9);
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        //Constructtor ends here

        //Function to access(get only) internal array
        public byte[] arrayAccessor{get{return this.array;}}
        //Function to access(get only) numberOfMisplacements
        public byte getNumberOfMisplacements() { return numberOfMisplacements; }

        //function to swap elements of an array
        void swap(ref byte[] arr,int indexOne, int indexTwo)
        {
            byte temp = arr[indexOne];
            arr[indexOne] = arr[indexTwo];
            arr[indexTwo] = temp; 
        }
        //function to swap elements and create new state
        MisplacementCountedArray swappedState(MisplacementCountedArray mca,int originalSpaceIndex,int newSpaceIndex)
        {
            byte[] arr = new byte[9];
            Array.Copy(mca.arrayAccessor, arr,9);
            swap(ref arr, originalSpaceIndex, newSpaceIndex);
            MisplacementCountedArray newMCA = new MisplacementCountedArray(arr, EightPuzzleGoal.goalTiles);
            newMCA.spaceIndex = newSpaceIndex;
            return newMCA;
        }

        //----------Functions for successor states---------
        //Move down state
        public MisplacementCountedArray moveS(MisplacementCountedArray mca)
        {
            if (mca.spaceIndex > 2) return swappedState(mca, mca.spaceIndex, mca.spaceIndex - 3);
            else return null; 
        }
        //Move up state
        public MisplacementCountedArray moveN(MisplacementCountedArray mca)
        {
            if (mca.spaceIndex < 6) return swappedState(mca, mca.spaceIndex, mca.spaceIndex + 3);
            else return null;
        }
        //Move right state
        public MisplacementCountedArray moveE(MisplacementCountedArray mca)
        {
            if (mca.spaceIndex%3 >0) return swappedState(mca, mca.spaceIndex, mca.spaceIndex - 1);
            else return null;
        }
        //Move left state
        public MisplacementCountedArray moveW(MisplacementCountedArray mca)
        {
            if (mca.spaceIndex%3 <2  ) return swappedState(mca, mca.spaceIndex, mca.spaceIndex + 1);
            else return null;
        }

        //Function to print the state
        public void print(){ Console.Write("Main state = "); for (int i = 0; i < array.Length; i += 3) Console.Write(array[i] + " " + array[i + 1] + " " + array[i + 2] + " ") ; Console.WriteLine(); }

    }
    /// <summary>
    /// Misplacements-Counted-Array Ends here
    /// </summary>




    /// <summary>
    /// Resources taken from internet starts here
    /// </summary>
    //Fisher-Yates algorithm to shuffle an array randomly
    //Courtesy : Matt Howels : stackoverflow.com/questions/108819
    static class RandomExtensions
    {
        public static void Shuffle(this Random rng, byte[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                byte temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }

    static class inversionCountingClass
    {
        //Function to return number of inversions
        //Courtesy : www.geeksforgeeks.org/counting-inversions
        public static int getInversionCount(byte[] arr)
        {
            int n = arr.Length - 1;
            int inv_count = new int();
            for (int i = 0; i < n - 1; i++)
                for (int j = i + 1; j < n; j++)
                    if (arr[i] > arr[j])
                        inv_count++;

            return inv_count;
        }
    }
    /// <summary>
    /// Resources taken from internet Ends here
    /// </summary>





    /// <summary>
    /// Misplacements-Counted-Array Container class starts here
    /// </summary>
    //Misplacements counted array container
    class MinimumMisplacedPriorityQueueContainer
    {
        //private variable
        byte nextEmptyIndex,headOfQueue;
        //holds the new states
        MisplacementCountedArray[] arrays;

        //Constructor starts here
        public MinimumMisplacedPriorityQueueContainer()
        {
            arrays = new MisplacementCountedArray[5];
            nextEmptyIndex = 0;
            headOfQueue = 0;
        }
        //Constructor ends here
        //Add function starts here... Check and stores the minimum state at the top...
        public bool add(MisplacementCountedArray mca2)
        {
            if (mca2 != null)
            {
                if (nextEmptyIndex != 0)
                {
                    //Iterating through to find proper place for the new state
                    int i;
                    for (i = 0; i < nextEmptyIndex; i++)
                    {
                        //Place found. Placement operation to begin
                        if (arrays[i].getNumberOfMisplacements() > mca2.getNumberOfMisplacements())
                        {
                            //Extending container size by 1
                            nextEmptyIndex++;
                            //moving all elements to make place
                            for (int j = nextEmptyIndex - 1; j > i; j--)
                            {
                                arrays[j] = arrays[j - 1];
                            }
                            //placement
                            arrays[i] = mca2;
                            //Exiting placement operation loop
                            return true;
                        }
                    }
                    if (i == nextEmptyIndex) { arrays[nextEmptyIndex++] = mca2; return true; }
                }
                else { arrays[nextEmptyIndex++] = mca2; return true; }
            }
            return false;
        }
        //Add function ends here

        //Function to retrieve the leading element
        public MisplacementCountedArray poll(){
            while (arrays[headOfQueue] == null) headOfQueue++;
            return arrays[headOfQueue++];

        }

        //Funcion to clear the queue
        public void clear() { arrays = null; arrays = new MisplacementCountedArray[5]; }
    }
    /// <summary>
    /// Misplacements-Counted-Array Container class Ends here
    /// </summary>


    





    
    static class EightPuzzleGoal {
        public static byte[] goalTiles = { 1, 2, 3, 4, 5, 6, 7, 8, 0 };
    }

    class solveEightPuzzleByMinimumMisplacedSuccessorState
    {
        public solveEightPuzzleByMinimumMisplacedSuccessorState(byte[] array)
        {
            try
            {
                
                //Declare necessary variables
                MyHashSet history = new MyHashSet();
                //Console.WriteLine("---Hashset created");
                //Shuffle the array first
                new Random().Shuffle(array);
                //Console.WriteLine("---Shuffling done");
                int inv_count = inversionCountingClass.getInversionCount(array);
                //Console.WriteLine("---Inversion counted");
                //Looking for solvable array
                while (inv_count % 2 != 0)
                {
                    new Random().Shuffle(array);
                    //Console.WriteLine("---Shuffling done");
                    inv_count = inversionCountingClass.getInversionCount(array);
                    //Console.WriteLine("---Inversion counted");
                }
                //=============solvable state found... start solving===============
                //Put the array in a new state object
                MisplacementCountedArray mainState = new MisplacementCountedArray(array, EightPuzzleGoal.goalTiles);
                Console.WriteLine("---Mainstate created");
                //print state
                mainState.print();
                history.add(mainState);
                Console.WriteLine("Number of inversions = " + inv_count);
                int c = 1;
                //Console.WriteLine("---priority container created");
                while (mainState.getNumberOfMisplacements() != 0)
                {
                    //Console.WriteLine("--Begin loop--");
                    MinimumMisplacedPriorityQueueContainer queue = new MinimumMisplacedPriorityQueueContainer();
                    //Console.WriteLine("Step number : " + c++);
                    //Making successor states (adapting from online resource) 
                    //Courtesy : EightPuzzle.java file found in lab(made by an anonymous person)
                    MisplacementCountedArray stateS = mainState.moveS(mainState); if (stateS != null) { queue.add(stateS); }
                    MisplacementCountedArray stateN = mainState.moveN(mainState);if (stateN != null) queue.add(stateN);
                    MisplacementCountedArray stateE = mainState.moveE(mainState); if (stateE != null) queue.add(stateE);
                    MisplacementCountedArray stateW = mainState.moveW(mainState); if (stateW != null) queue.add(stateW);
                    //successors added... poll() and try to add the least one
                    MisplacementCountedArray temp = null;
                    temp = queue.poll();
                    if (!history.add(temp)) history.add(queue.poll());
                    //fetched the unvisited minimum successor state
                    //clear the queue;
                    queue.clear();
                    //change mainstate to new successor
                    mainState = temp;
                    //Console.WriteLine("---New main state created");
                    //mainState.print();
                }
                //check if goal tiles reached. if reached, print results
                if (mainState.getNumberOfMisplacements() == 0)
                {
                    Console.WriteLine("Solved in " + history.getNumberOfElements());
                    mainState.print();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            try {
                
                byte[] testArray = { 0,1,2,3,4,5,6,7,8 };
                new solveEightPuzzleByMinimumMisplacedSuccessorState(testArray);

            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
        }
    }
}
