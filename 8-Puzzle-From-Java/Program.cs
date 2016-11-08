using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;



namespace _8_Puzzle_From_Java
{

    class Program
    {
        static void Main(string[] args)
        {

            try
            {

                byte[] testArray1 = { 0, 2,1, 3, 4, 5, 6, 7, 8 };


                DFS_solution.solveByRecursiveDFSImplementation(testArray1);

                //new solveEightPuzzleByMinimumMisplacedSuccessorState(testArray1);
                //driverProgramToTestHashset.HashTester(1000);

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
