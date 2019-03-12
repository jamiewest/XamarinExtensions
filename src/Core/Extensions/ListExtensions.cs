using System.Linq;

namespace System.Collections.Generic
{
    public static class ListExtensions
    {
        /// <summary>
        /// Perform a sort of the items in a collection. This is useful
        /// if the underlying collection does not support sorting. 
        /// </summary>
        /// <typeparam name="T">Item type in collection</typeparam>
        /// <param name="collection">Underlying collection to sort</param>
        /// <param name="comparer">Comparer delegate</param>
        /// <param name="reverse">True to reverse the collection</param>
        public static void BubbleSort<T>(this IList<T> collection, Func<T, T, int> comparer, bool reverse = false)
        {
            for (int index = collection.Count - 1; index >= 0; index--)
            {
                for (int child = 1; child <= index; child++)
                {
                    T d1 = collection[child - 1];
                    T d2 = collection[child];

                    int result = (!reverse) ? comparer(d1, d2) : comparer(d2, d1);
                    if (result > 0)
                    {
                        collection.Remove(d1);
                        collection.Insert(child, d1);
                    }
                }
            }
        }

        /// <summary>
        /// Perform a sort of the items in a collection. This is useful
        /// if the underlying collection does not support sorting. Note that
        /// the object type must be comparable.
        /// </summary>
        /// <param name="collection">Underlying collection to sort</param>
        /// <param name="comparer">Comparer interface</param>
        /// <param name="reverse">True to reverse the collection</param>
        public static void BubbleSort(this IList collection, IComparer comparer, bool reverse = false)
        {
            for (int index = collection.Count - 1; index >= 0; index--)
            {
                for (int child = 1; child <= index; child++)
                {
                    object d1 = collection[child - 1];
                    object d2 = collection[child];

                    int result = (!reverse)
                        ? comparer.Compare(d1, d2)
                        : comparer.Compare(d2, d1);

                    if (result > 0)
                    {
                        collection.Remove(d1);
                        collection.Insert(child, d1);
                    }
                }
            }
        }

        /// <summary>
        /// Swap a value in the collection
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="collection">Source collection</param>
        /// <param name="sourceIndex">Index</param>
        /// <param name="destIndex">Dest index</param>
        public static void Swap<T>(this IList<T> collection, int sourceIndex, int destIndex)
        {
            // Simple parameter checking
            if (sourceIndex < 0 || sourceIndex >= collection.Count)
                throw new ArgumentOutOfRangeException(nameof(sourceIndex));
            if (destIndex < 0 || destIndex >= collection.Count)
                throw new ArgumentOutOfRangeException(nameof(destIndex));

            // Ignore if same index
            if (sourceIndex == destIndex)
                return;

            T temp = collection[sourceIndex];
            collection[sourceIndex] = collection[destIndex];
            collection[destIndex] = temp;
        }

        /// <summary>
        /// This method moves a range of values in the collection
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="collection">Source collection</param>
        /// <param name="startingIndex">Index</param>
        /// <param name="count">Count of items</param>
        /// <param name="destIndex">Dest index</param>
        public static void MoveRange<T>(this IList<T> collection, int startingIndex, int count, int destIndex)
        {
            // Simple parameter checking
            if (startingIndex < 0 || startingIndex >= collection.Count)
                throw new ArgumentOutOfRangeException(nameof(startingIndex));
            if (destIndex < 0 || destIndex >= collection.Count)
                throw new ArgumentOutOfRangeException(nameof(destIndex));
            if (count < 0 || startingIndex + count > collection.Count)
                throw new ArgumentOutOfRangeException(nameof(count));

            // Ignore if same index or count is zero
            if (startingIndex == destIndex || count == 0)
                return;

            // Make sure we can modify this directly
            if (collection.GetType().IsArray)
                throw new NotSupportedException("Collection is fixed-size and items cannot be efficiently moved.");

            // Go through the collection element-by-element
            var range = Enumerable.Range(0, count);
            if (startingIndex < destIndex)
                range = range.Reverse();

            foreach (var i in range)
            {
                int start = startingIndex + i;
                int dest = destIndex + i;

                T item = collection[start];
                collection.RemoveAt(start);
                collection.Insert(dest, item);
            }
        }
    }
}