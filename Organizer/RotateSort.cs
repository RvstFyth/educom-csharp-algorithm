using System.Collections.Generic;

namespace Organizer
{
    public class RotateSort<T>
    {

        private List<T> array = new List<T>();

        private IComparer<T> Comparer;

        public List<T> Sort(List<T> input, IComparer<T> comparer)
        {
            array = new List<T>(input);
            Comparer = comparer;
            SortFunction(0, array.Count - 1);
            return array;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="low">De index within this.array to start with</param>
        /// <param name="high">De index within this.array to stop with</param>
        private void SortFunction(int low, int high)
        {
            if (low < high)
            {
                int pivot = Partitioning(low, high);
                SortFunction(low, pivot - 1);
                SortFunction(pivot + 1, high);
            }
        }

        /// <summary>
        /// Partition the array in a group 'low' digits (e.a. lower than a choosen pivot) and a group 'high' digits
        /// </summary>
        /// <param name="low">De index within this.array to start with</param>
        /// <param name="high">De index within this.array to stop with</param>
        /// <returns>The index in the array of the first of the 'high' digits</returns>
        private int Partitioning(int low, int high)
        {
            var pivot = array[high];
            int p = low - 1;

            for (int j = low; j < high; j++)
            {
                if (Comparer.Compare(array[j], pivot) < 1)
                {
                    p += 1;
                    (array[p], array[j]) = (array[j], array[p]);
                }
            }

            (array[p + 1], array[high]) = (array[high], array[p + 1]);
            return p + 1;
        }
    }
}
