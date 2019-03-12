using System.Collections.ObjectModel;
using System.Linq;
using West.Extensions.Xamarin;

namespace System.Collections.Generic
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Takes a LINQ GroupBy value and turns it into a set of GroupedObservableCollection objects.
        /// </summary>
        /// <returns>The grouped observable.</returns>
        /// <param name="group">Group.</param>
        /// <typeparam name="TKey">The 1st type parameter.</typeparam>
        /// <typeparam name="TValue">The 2nd type parameter.</typeparam>
        public static IEnumerable<GroupedObservableCollection<TKey, TValue>> ToGroupedObservable<TKey, TValue>(
           this IEnumerable<IGrouping<TKey, TValue>> group)
        {
            foreach (var item in group)
            {
                yield return new GroupedObservableCollection<TKey, TValue>(item.Key, item);
            }
        }

        /// <summary>
        /// Returns an ObservableCollection from a set of enumerable items.
        /// </summary>
        /// <returns>The observable collection.</returns>
        /// <param name="items">Items.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source) => 
            new OptimizedObservableCollection<T>(source);

        public static ObservableDictionary<TKey, TValue> ToObservableDictionary<TKey, TValue>(this IDictionary<TKey, TValue> source) => 
            new ObservableDictionary<TKey, TValue>(source);

        ///<summary>
        /// This method tests an enumerable sequence and returns the index of the first item that
        /// passes the test.
        ///</summary>
        ///<typeparam name="T">Type of collection</typeparam>
        ///<param name="collection">Collection</param>
        ///<param name="test">Predicate test</param>
        ///<returns>Index (zero based) of first element that passed test, -1 if none did</returns>
        public static int IndexOf<T>(this IEnumerable<T> collection, Predicate<T> test)
        {
            int pos = 0;
            foreach (var item in collection)
            {
                if (test(item))
                    return pos;
                pos++;
            }
            return -1;
        }
    }
}
