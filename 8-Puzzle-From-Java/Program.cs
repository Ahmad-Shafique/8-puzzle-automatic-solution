﻿using System;
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
            UInt64 hc = (UInt64)obj.arrayAccessor.Length;
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
            newMCA.spaceIndex = newSpaceIndex;
            Debug.Assert(newMCA != null);
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
        public void print() { Console.Write("Main state = "); for (int i = 0; i < array.Length; i += 3) Console.Write(array[i] + " " + array[i + 1] + " " + array[i + 2] + " "); Console.WriteLine(); }

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
    /// Priority Queue class starts here
    /// </summary>

    class customizedPriorityQueue
    {
        uint sizeOfQueue, numberOfNodes, headOfQueue;
        MisplacementCountedArray[] nodes;

        //Constructor initializes the internal array with size 10
        public customizedPriorityQueue() { sizeOfQueue = 10;
            //////nodes = new MisplacementCountedArray[sizeOfQueue];
            //////for (int i = 0; i < sizeOfQueue; i++) nodes[i] = new MisplacementCountedArray();
        }

        //For inserting elements into the queue
        public void Enqueue(MisplacementCountedArray node)
        {
            Debug.Assert(node != null);
            try
            {
                
                //Make sure insertion index is not negative
                Debug.Assert(numberOfNodes >= 0);

                if(numberOfNodes<2)
                {
                    //Queue empty, insert node directly
                    Debug.Assert(node != null);
                    Debug.Assert(numberOfNodes >= 0);
                    if (numberOfNodes == 0) nodes[numberOfNodes++] = node;
                    //Queue has one element , check priority and insert accordingly
                    else
                    {
                        Debug.Assert(nodes[0] != null);
                        //if(numberOfNodes)Debug.Assert(nodes[0].getNumberOfMisplacement > node.getNumberOfMisplacement);
                        if (nodes[0].getNumberOfMisplacement > node.getNumberOfMisplacement)
                        {
                            MisplacementCountedArray temp;
                            numberOfNodes++;
                            temp = nodes[0];
                            //Console.WriteLine("Entered if");
                            temp.print();
                            nodes[1] = temp;
                            Console.Write("-------");
                            nodes[1].print();
                            nodes[0] = node;
                            Debug.Assert(nodes[0] != null);
                            Debug.Assert(nodes[1] != null);
                            Debug.Assert(node != null);
                        }
                        else
                        {
                            nodes[0] = node;
                            numberOfNodes++;
                            Debug.Assert(nodes[0] != null);
                        }
                    }
                }
                //Check if queue already has node/s
                else if (numberOfNodes >= 2)
                {
                    //If the queue already has 2 nodes :
                    //iterate to find the right place
                    int i = 0,length=0;
                    length = (int)numberOfNodes;
                    Console.WriteLine(length);
                    Debug.Assert(length > 0);
                    Debug.Assert(nodes[0] != null);
                    Debug.Assert(nodes[1] != null);
                    for (; i < length ; i++)
                    {
                        Debug.Assert(nodes[i] != null);

                        //place found , break from loop
                        if (nodes[i] != null)
                        {
                            Debug.Assert(nodes[i] != null);
                            if (nodes[i].getNumberOfMisplacement > node.getNumberOfMisplacement) break;
                        }
                    }
                    //if the loop ended without breaking , insert at the end
                    if (i >= numberOfNodes) { nodes[numberOfNodes++] = node; }
                    //loop forcibly broken , move all nodes necessary and place the node
                    else
                    {
                        Debug.Assert(node != null);
                        for (int j =(int) numberOfNodes; j > i; j--) nodes[j] = nodes[j - 1];
                        nodes[i] = node; numberOfNodes++;
                    }

                }
                //queue doesn't have more than 2 nodes
                
                //Function to remove null element , if existing
                //checkAndFix();
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); Console.WriteLine("Problem enqueing elements"); }

        }


        //Increase queue size by 10
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
            if (headOfQueue < numberOfNodes)
            {
                Debug.Assert(nodes[headOfQueue] != null);
                return nodes[headOfQueue++];
            }
            else {
                Debug.Assert(nodes[headOfQueue] != null);
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
                for (int i = 0; i < nodes.Length - 1; i++)
                {
                    if (nodes[i] == null)
                    {
                        for (int j = i; j < nodes.Length - 1; j++) nodes[j] = nodes[j + 1];
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); Console.WriteLine("Problem in checkAndFix()"); }
        }

    }

    /// <summary>
    /// Priority Queue ends here
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
                    MisplacementCountedArray stateS = mainState.moveS(mainState); if (stateS != null && !history.contains(stateS)) queue.Enqueue(stateS); 
                    MisplacementCountedArray stateN = mainState.moveN(mainState); if (stateN != null && !history.contains(stateN)) queue.Enqueue(stateN);
                    MisplacementCountedArray stateE = mainState.moveE(mainState); if (stateE != null && !history.contains(stateE)) queue.Enqueue(stateE);
                    MisplacementCountedArray stateW = mainState.moveW(mainState); if (stateW != null && !history.contains(stateW)) queue.Enqueue(stateW);
                    //successors added... poll() and try to add the least one
                    MisplacementCountedArray temp = null;
                    for (;;)
                    {
                        if (temp != null) break;
                        MisplacementCountedArray temp2;
                        temp2 = queue.poll();
                        Debug.Assert(temp2 != null);
                        if (history.add(temp2)) { temp = temp2; }
                        Console.WriteLine("poll() loop");
                    }

                    //fetched the unvisited minimum successor state
                    //clear the queue;
                    queue.clear();
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

    class Program
    {
        static void Main(string[] args)
        {

            try
            {

                byte[] testArray1 = { 0, 2,1, 3, 4, 5, 6, 7, 8 };
                byte[] testArray2 = { 0, 1,3,2, 4, 5, 6, 7, 8 };
                byte[] testArray3 = { 0, 1, 2,4,3 ,5, 6, 7, 8 };
                byte[] testArray4 = { 0, 1, 2, 3, 5,4, 6, 7, 8 };
                byte[] testArray5 = { 0, 1, 2, 3, 4, 6,5, 7, 8 };

                new Random().Shuffle(testArray1);
                new Random().Shuffle(testArray2);
                new Random().Shuffle(testArray3);
                new Random().Shuffle(testArray4);
                new Random().Shuffle(testArray5);

                MisplacementCountedArray mca1 = new MisplacementCountedArray(testArray1, EightPuzzleGoal.goalTiles);
                MisplacementCountedArray mca2 = new MisplacementCountedArray(testArray2, EightPuzzleGoal.goalTiles);
                MisplacementCountedArray mca3 = new MisplacementCountedArray(testArray3, EightPuzzleGoal.goalTiles);
                MisplacementCountedArray mca4 = new MisplacementCountedArray(testArray4, EightPuzzleGoal.goalTiles);
                MisplacementCountedArray mca5 = new MisplacementCountedArray(testArray5, EightPuzzleGoal.goalTiles);

                mca1.print();
                mca2.print();
                mca3.print();
                mca4.print();
                mca5.print();

                
                customizedPriorityQueue history = new customizedPriorityQueue();
                history.Enqueue(mca1);
                //history.Enqueue(mca2);
                //history.Enqueue(mca3);
                //history.Enqueue(mca4);
                //history.Enqueue(mca5);
                

                //new solveEightPuzzleByMinimumMisplacedSuccessorState(testArray);

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