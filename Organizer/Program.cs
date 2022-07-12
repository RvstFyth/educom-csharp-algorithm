using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Organizer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter a number:");
            string num = Console.ReadLine();
            var timer = new Stopwatch();
            var shiftHighest = new ShiftHighestSort();
            var rotateSort = new RotateSort<int>();
            var randomNumbers = GenerateNumbers(Int32.Parse(num));
            ShowList("Unsorted", randomNumbers);
            
            timer.Start();
            ShowList("rotateSort: Sorted", rotateSort.Sort(randomNumbers, Comparer<int>.Default));
            timer.Stop();
            TimeSpan fTime = timer.Elapsed;

            timer.Start();
            ShowList("shiftHighest: Sorted", shiftHighest.Sort(randomNumbers));
            timer.Stop();
            TimeSpan sTime = timer.Elapsed;
            
            // Console.WriteLine("RotateSort: " + fTime.ToString(@"m\:ss\.fff"));
            Console.WriteLine("ShiftHighestSort: " + sTime.ToString(@"m\:ss\.fff"));
        }

        private static List<int> GenerateNumbers(int max)
        {
            var result = new List<int>();
            
            var rd = new Random();
            for (var i = 0; i < max; i++)
            {
                result.Add(rd.Next(-99, 99));    
            }

            return result;
        }

        /* Example of a static function */

        /// <summary>
        /// Show the list in lines of 20 numbers each
        /// </summary>
        /// <param name="label">The label for this list</param>
        /// <param name="theList">The list to show</param>
        public static void ShowList(string label, List<int> theList)
        {
            int count = theList.Count;
            if (count > 200)
            {
                count = 200; // Do not show more than 300 numbers
            }
            Console.WriteLine();
            Console.Write(label);
            Console.Write(':');
            for (int index = 0; index < count; index++)
            {
                if (index % 20 == 0) // when index can be divided by 20 exactly, start a new line
                {
                    Console.WriteLine();
                }
                Console.Write(string.Format("{0,3}, ", theList[index]));  // Show each number right aligned within 3 characters, with a comma and a space
            }
            Console.WriteLine();
        }
    }
}
