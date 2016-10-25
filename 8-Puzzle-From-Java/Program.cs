using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

//Priority Queue highly unoptimized. Only for single use
//Can become much more optimized if linked list is used inside priority queue

namespace _8_Puzzle_From_Java
{

    /// <summary>
    /// Misplacements-Counted-Array starts here
    /// </summary>
    //Arrray with number of misplacements already counted
    class MisplacementCountedArray
    {
        public int spaceIndex;
        byte numberOfMisplacements;
        byte[] array;
        //Constructors starts here
        public MisplacementCountedArray()
        {
            array = new byte[9];
            numberOfMisplacements = 99;
            spaceIndex = new int();
        }
        public MisplacementCountedArray(byte[] arr, byte[] correctlyPositioned)
        {
            try
            {
                array = new byte[9];
                byte nom = new byte();
                spaceIndex = new int();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] != correctlyPositioned[i]) nom++;
                    if (arr[i] == 0) spaceIndex = i;
                }
                numberOfMisplacements = nom;
                Array.Copy(arr, array, 9);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        //Constructtor ends here

        //Function to access(get only) internal array
        public byte[] arrayAccessor { get { return array; } }
        //Function to access(get only) numberOfMisplacements
        public byte getNumberOfMisplacement { get { return numberOfMisplacements; } }

        //function to swap elements of an array
        void swap(byte[] arr, int indexOne, int indexTwo)
        {
            Debug.Assert(arr != null);
            byte temp = arr[indexOne];
            arr[indexOne] = arr[indexTwo];
            arr[indexTwo] = temp;
        }
        //function to swap elements and create new state
        MisplacementCountedArray swappedState(MisplacementCountedArray mca, int originalSpaceIndex, int newSpaceIndex)
        {
            Debug.Assert(mca != null);
            byte[] arr = new byte[9];
            Array.Copy(mca.arrayAccessor, arr, 9);
            swap(arr, originalSpaceIndex, newSpaceIndex);
            MisplacementCountedArray newMCA = new MisplacementCountedArray(arr, EightPuzzleGoal.goalTiles);
            //newMCA.spaceIndex = newSpaceIndex;
            Debug.Assert(newMCA != null);
            //Console.Write("newMCA = ");
            //newMCA.print();
            return newMCA;
        }

        //----------Functions for successor states---------
        //Successor addition taken from a java file found in the lab created by an unknown person 
        //Move down state
        public MisplacementCountedArray moveS(MisplacementCountedArray mca)
        {
            Debug.Assert(mca != null);
            if (mca.spaceIndex > 2) return swappedState(mca, mca.spaceIndex, mca.spaceIndex - 3);
            else return null;
        }
        //Move up state
        public MisplacementCountedArray moveN(MisplacementCountedArray mca)
        {
            Debug.Assert(mca != null);
            if (mca.spaceIndex < 6) return swappedState(mca, mca.spaceIndex, mca.spaceIndex + 3);
            else return null;
        }
        //Move right state
        public MisplacementCountedArray moveE(MisplacementCountedArray mca)
        {
            Debug.Assert(mca != null);
            if (mca.spaceIndex % 3 > 0) return swappedState(mca, mca.spaceIndex, mca.spaceIndex - 1);
            else return null;
        }
        //Move left state
        public MisplacementCountedArray moveW(MisplacementCountedArray mca)
        {
            Debug.Assert(mca != null);
            if (mca.spaceIndex % 3 < 2) return swappedState(mca, mca.spaceIndex, mca.spaceIndex + 1);
            else return null;
        }

        //Function to print the state
        public void print() { Console.Write("Space index = "+ spaceIndex+"::\n"); for (int i = 0; i < array.Length; i += 3) { Console.Write(array[i] + " " + array[i + 1] + " " + array[i + 2] + " "); Console.WriteLine(); } }

    }
    /// <summary>
    /// Misplacements-Counted-Array Ends here
    /// </summary>









    /// <summary>
    /// Priority Queue class starts here
    /// </summary>

    class customizedPriorityQueue
    {
        uint sizeOfQueue, numberOfNodes, headOfQueue;
        MisplacementCountedArray[] nodes;

        //Constructor initializes the internal array with size 10
        public customizedPriorityQueue() { sizeOfQueue = 10; headOfQueue = 0;
            nodes = new MisplacementCountedArray[sizeOfQueue];
            //for (int i = 0; i < sizeOfQueue; i++) nodes[i] = new MisplacementCountedArray();
        }

        //For inserting elements into the queue

        public bool Enqueue(MisplacementCountedArray node)
        {
            
            Debug.Assert(node != null);
            if(numberOfNodes >= 2)
            {
                int i;
                for(i=(int)headOfQueue; i<(int)numberOfNodes; i++)
                {
                    if (node.getNumberOfMisplacement < nodes[i].getNumberOfMisplacement)
                    {
                        for (int j = (int)numberOfNodes; j > i; j--) { nodes[j] = nodes[j - 1]; Debug.Assert(nodes[j] != null); }
                        //Console.WriteLine("Elements moved ");
                        break;
                    }
                }
                nodes[i] = node;
                //if(i<numberOfNodes) Console.WriteLine("Inserted in between ");
                //else Console.WriteLine("Inserted at the end ");
                numberOfNodes++;
                return true;
            }
            else
            {
                if (numberOfNodes == 0)
                {
                    nodes[numberOfNodes++] = node;
                    //Console.WriteLine("Inserted at the beginning ");
                    Debug.Assert(nodes[0] != null);
                }
                else
                {
                    if (node.getNumberOfMisplacement < nodes[0].getNumberOfMisplacement)
                    {
                        nodes[numberOfNodes++] = nodes[0];
                        nodes[0] = node;
                        Debug.Assert(nodes[0] != null);
                        Debug.Assert(nodes[1] != null);
                        //Console.WriteLine("Inserted second place position 1 ");
                    }
                    else
                    {
                        nodes[numberOfNodes++] = node;
                        Debug.Assert(nodes[numberOfNodes - 1] != null);
                        //Console.WriteLine("Inserted second place position 1 ");
                    }
                }
                return true;
            }
            
        }
        
        public void Resize()
        {
            try
            {
                sizeOfQueue += 10;
                MisplacementCountedArray[] NewArray = new MisplacementCountedArray[sizeOfQueue];
                for (int i = 0; i < sizeOfQueue; i++) NewArray[i] = new MisplacementCountedArray();
                for (int i = 0; i < nodes.Length; i++)
                {
                    NewArray[i] = nodes[i];
                }
                nodes = NewArray;
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); Console.WriteLine("Unable to resize!!!"); }
        }

        //Return the top most element
        public MisplacementCountedArray poll() {
            if (headOfQueue < numberOfNodes-1)
            {
                //Console.WriteLine("head and tail : " + headOfQueue + " " + numberOfNodes);
                //checkAndFix();
                Debug.Assert(nodes[headOfQueue] != null);
                return nodes[headOfQueue++];
            }
            else {
                //Console.WriteLine("head and tail : " + headOfQueue + " " + numberOfNodes);
                Debug.Assert(nodes[headOfQueue] != null);
                //checkAndFix();
                return nodes[headOfQueue];
            }
        }

        //reset the array
        public void clear() { Array.Clear(nodes, 0, (int)sizeOfQueue); headOfQueue = 0; numberOfNodes = 0;  }

        //Check and remove null elements
        public void checkAndFix()
        {
            try
            {
                for (int i = 0; i < numberOfNodes - 1; i++)
                {
                    if (nodes[i] == null)
                    {
                        for (int j = i; j < numberOfNodes - 1; j++) nodes[j] = nodes[j + 1];
                        numberOfNodes--;
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); Console.WriteLine("Problem in checkAndFix()"); }
        }

    }

    /// <summary>
    /// Priority Queue ends here
    /// </summary>






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

            if (numberOfElements > 1)
            {
                int index = binarySearch(hash, 0, numberOfElements - 1);
                //Console.WriteLine("Index number is " + index);
                bool flag = false;
                if (index == -1)
                {
                    for (i = 0; i < numberOfElements; i++)
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
                    if (i == numberOfElements && !flag)
                    {
                        hashValues[numberOfElements++] = hash;
                        //Console.WriteLine("Inserted at the end " + hash + "--" + numberOfElements);
                    }
                    Console.WriteLine("Added = " + hash);
                    return true;
                }
            }
            else
            {
                if (numberOfElements == 0)
                {
                    hashValues[numberOfElements++] = hash;
                    //Console.WriteLine("Inserted as first element" + hash + "--" + numberOfElements);
                }
                else {
                    if (hash > hashValues[0])
                    {
                        hashValues[numberOfElements++] = hash;
                        //Console.WriteLine("Inserted as second element in if " + hash + "--" + numberOfElements);
                    }
                    else {
                        hashValues[numberOfElements++] = hashValues[0]; hashValues[0] = hash;
                        //Console.WriteLine("Inserted as second element in else " + hash + "--" + numberOfElements);
                    }

                }
                Console.WriteLine("Added = " + hash);
                return true;
            }
            Console.WriteLine("Hash value could not be added");
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

        public bool contains(MisplacementCountedArray mca)
        {
            ulong hash = GetHashCode(mca);
            int index = binarySearch(hash, 0, numberOfElements - 1);
            if (index == -1) return false;
            else return true;
        }

        //Generates hash code for the array
        UInt64 GetHashCode(MisplacementCountedArray obj)
        {
            //throw new NotImplementedException();
            //for (int i = 0; i < 9; i++) Console.WriteLine(obj.arrayAccessor[i]);
            Debug.Assert(obj != null);
            UInt64 hc = 1;
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



    static class driverProgramToTestHashset
    {
        public static void HashTester(int size) {
            //create a hash set;
            MyHashSet mhs = new MyHashSet();

            //Keep a storage for test cases
            MisplacementCountedArray[] mca = new MisplacementCountedArray[size];

            byte[] baseCase = { 1, 1, 1, 1, 1, 1, 1, 1, 1 };

            //generate the test cases :
            int baseCaseIndexToChange = 8;
            for (int i = 0; i < size; i++) {
                if (baseCase[baseCaseIndexToChange]>= 254) { baseCaseIndexToChange -= 1; }
                baseCase[baseCaseIndexToChange] = (byte)(baseCase[baseCaseIndexToChange] + (i%255));
                mca[i] = new MisplacementCountedArray(baseCase, EightPuzzleGoal.goalTiles);
                
            }

            //printing the test cases
            for(int i=0; i<size; i++)
            {
                mca[i].print();
            }

            //adding values to hash set
            for(int i=0; i<size; i++)
            {
                mhs.add(mca[i]);
            }

            //Testing set by tryng to add same values again
            for (int i = 0; i < size; i++)
            {
                mhs.add(mca[i]);
            }

        }

    }
    /// <summary>
    /// Hashset coding ends here
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
    /// Solution by minimum state starts here
    /// </summary>

    static class EightPuzzleGoal
    {
        public static byte[] goalTiles = { 1, 2, 3, 4, 5, 6, 7, 8, 0 };
    }

    class solveEightPuzzleByMinimumMisplacedSuccessorState
    {
        public solveEightPuzzleByMinimumMisplacedSuccessorState(byte [] array)
        {
            try
            {
                { for (int i = 0; i < 9; i++) Console.Write(i + " "); Console.WriteLine(); }

                //Declare necessary variables
                MyHashSet history = new MyHashSet();
                Console.WriteLine("---Hashset created");
                //Shuffle the array first
                new Random().Shuffle(array);
                Console.WriteLine("---Shuffling done");
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
                Debug.Assert(mainState != null);
                history.add(mainState);
                Console.WriteLine("Number of inversions = " + inv_count);
                int c = 1;
                while (mainState.getNumberOfMisplacement != 0)
                {
                    //Console.WriteLine("--Begin loop--");
                    customizedPriorityQueue queue = new customizedPriorityQueue();
                    Console.WriteLine("---priority container created");
                    //Console.WriteLine("Step number : " + c++);
                    //Making successor states (adapting from online resource) 
                    //Courtesy : EightPuzzle.java file found in lab(made by an anonymous person)
                    Debug.Assert(mainState != null);
                    MisplacementCountedArray stateS = mainState.moveS(mainState);
                    if (stateS != null && !history.contains(stateS)) queue.Enqueue(stateS); 
                    MisplacementCountedArray stateN = mainState.moveN(mainState);
                    if (stateN != null && !history.contains(stateN)) queue.Enqueue(stateN);
                    MisplacementCountedArray stateE = mainState.moveE(mainState);
                    if (stateE != null && !history.contains(stateE)) queue.Enqueue(stateE);
                    MisplacementCountedArray stateW = mainState.moveW(mainState);
                    if (stateW != null && !history.contains(stateW)) queue.Enqueue(stateW);
                    //successors added... poll() and try to add the least one
                    //Console.WriteLine("poll() begin");
                    MisplacementCountedArray temp = queue.poll();
                    //Console.WriteLine("poll() End");

                    history.add(temp);
                    //fetched the unvisited minimum successor state
                    //clear the queue;
                    queue.clear();
                    Console.WriteLine("Queue clear");
                    //change mainstate to new successor
                    mainState = temp;
                    Console.WriteLine("---New main state created");
                    Debug.Assert(mainState != null);
                    mainState.print();
                }
                //check if goal tiles reached. if reached, print results
                if (mainState.getNumberOfMisplacement == 0)
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

    /// <summary>
    /// Solution by minimum state starts here
    /// </summary>
    /// 









    class Program
    {
        static void Main(string[] args)
        {

            try
            {

                byte[] testArray1 = { 0, 2,1, 3, 4, 5, 6, 7, 8 };

                //byte[] testArray2 = { 0, 1,3,2, 4, 5, 6, 7, 8 };
                //byte[] testArray3 = { 0, 1, 2,4,3 ,5, 6, 7, 8 };
                //byte[] testArray4 = { 0, 1, 2, 3, 5,4, 6, 7, 8 };
                //byte[] testArray5 = { 0, 1, 2, 3, 4, 6,5, 7, 8 };

                //new Random().Shuffle(testArray1);
                //new Random().Shuffle(testArray2);
                //new Random().Shuffle(testArray3);
                //new Random().Shuffle(testArray4);
                //new Random().Shuffle(testArray5);

                //MisplacementCountedArray mca1 = new MisplacementCountedArray(testArray1, EightPuzzleGoal.goalTiles);
                //MisplacementCountedArray mca2 = new MisplacementCountedArray(testArray2, EightPuzzleGoal.goalTiles);
                //MisplacementCountedArray mca3 = new MisplacementCountedArray(testArray3, EightPuzzleGoal.goalTiles);
                //MisplacementCountedArray mca4 = new MisplacementCountedArray(testArray4, EightPuzzleGoal.goalTiles);
                //MisplacementCountedArray mca5 = new MisplacementCountedArray(testArray5, EightPuzzleGoal.goalTiles);

                //mca1.print();
                //mca2.print();
                //mca3.print();
                //mca4.print();
                //mca5.print();


                //customizedPriorityQueue history = new customizedPriorityQueue();
                //history.Enqueue(mca1);
                //history.Enqueue(mca2);
                //if (history.Enqueue(mca3)) history.poll().print();
                //if (history.Enqueue(mca4)) history.poll().print();
                //if (history.Enqueue(mca5)) history.poll().print();

                //MyHashSet hs = new MyHashSet();
                //hs.add(mca1);
                //hs.add(mca2);
                //hs.add(mca3);
                //hs.add(mca4);
                //hs.add(mca5);



                //history.poll().print();
                //history.poll().print();
                //history.poll().print();
                //history.poll().print();
                //history.poll().print();

                //new Random().Shuffle(testArray1);
                //MisplacementCountedArray mca1 = new MisplacementCountedArray(testArray1, EightPuzzleGoal.goalTiles);
                //customizedPriorityQueue queue = new customizedPriorityQueue();
                //mca1.print();

                //MisplacementCountedArray moveS = mca1.moveS(mca1);
                //MisplacementCountedArray moveN = mca1.moveN(mca1);
                //MisplacementCountedArray moveE = mca1.moveE(mca1);
                //MisplacementCountedArray moveW = mca1.moveW(mca1);
                //if (moveS != null ) queue.Enqueue(moveS);
                //if (moveN != null) queue.Enqueue(moveN);
                //if (moveE != null ) queue.Enqueue(moveE);
                //if (moveW != null ) queue.Enqueue(moveW);

                //queue.poll().print();
                //queue.poll().print();
                //queue.poll().print();
                //queue.poll().print();

                new solveEightPuzzleByMinimumMisplacedSuccessorState(testArray1);

                //driverProgramToTestHashset.HashTester(100);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
        }
    }
}
