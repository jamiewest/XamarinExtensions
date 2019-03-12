//
// CollectionExtensions.cs
//
// Author:
//       Mark Smith <smmark@microsoft.com>
//
// Copyright (c) 2016-2018 Xamarin, Microsoft.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System.Linq;

namespace System.Collections.Generic
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// This is used to compare two collections.
        /// </summary>
        /// <typeparam name="T">Collection type</typeparam>
        /// <param name="collection">Collection Source</param>
        /// <param name="other">Collection to compare to</param>
        /// <param name="sameOrderRequired">Require same-order elements (exact match)</param>
        /// <returns></returns>
        public static bool Compare<T>(this ICollection<T> collection, ICollection<T> other, bool sameOrderRequired = false)
        {
            if (!ReferenceEquals(collection, other))
            {
                if (other == null)
                    throw new ArgumentNullException("other");

                // Not the same number of elements.  No match
                if (collection.Count != other.Count)
                    return false;

                // Require same-order; just defer to existing LINQ match
                if (sameOrderRequired)
                    return collection.SequenceEqual(other);

                // Otherwise allow it to be any order, but require same count of each item type.
                var comparer = EqualityComparer<T>.Default;
                return !(from item in collection
                         let thisItem = item
                         where !other.Contains(item, comparer) || collection.Count(check => comparer.Equals(thisItem, check)) != other.Count(check => comparer.Equals(thisItem, check))
                         select item).Any();
            }

            return true;
        }

        /// <summary>
        /// Add a range of IEnumerable collection to an existing Collection.
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="collection">Collection</param>
        /// <param name="items">Items to add</param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// Removes a set of items from the collection.
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="collection">Collection to remove from</param>
        /// <param name="items">Items to remove from collection.</param>
        public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
            {
                collection.Remove(item);
            }
        }
    }
}