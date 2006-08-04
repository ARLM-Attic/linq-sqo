using System;
using System.Collections;
using System.Collections.Generic;

/*=======================================================================
  Custom implementation of the LINQ Standard Query Operators.

  Bart De Smet - June 06
=======================================================================*/

namespace BdsSoft
{
    namespace Query
    {
        #region 1.1 The Func delegate types

        /// <summary>
        /// Generic delegate with 0 parameters useful to construct delegate types on the fly, e.g. for implicit usage in lambda expressions.
        /// </summary>
        /// <typeparam name="TR">Return type of the function.</typeparam>
        /// <returns></returns>
        public delegate TR Func<TR>();

        /// <summary>
        /// Generic delegate with 1 parameter useful to construct delegate types on the fly, e.g. for implicit usage in lambda expressions.
        /// </summary>
        /// <typeparam name="T0">Type of the first parameter.</typeparam>
        /// <typeparam name="TR">Return type of the function.</typeparam>
        /// <param name="a0">Dummy variable to denote the first parameter.</param>
        /// <returns></returns>
        public delegate TR Func<T0, TR>(T0 a0);

        /// <summary>
        /// Generic delegate with 2 parameters useful to construct delegate types on the fly, e.g. for implicit usage in lambda expressions.
        /// </summary>
        /// <typeparam name="T0">Type of the first parameter.</typeparam>
        /// <typeparam name="T1">Type of the second parameter.</typeparam>
        /// <typeparam name="TR">Return type of the function.</typeparam>
        /// <param name="a0">Dummy variable to denote the first parameter.</param>
        /// <param name="a1">Dummy variable to denote the second parameter.</param>
        /// <returns></returns>
        public delegate TR Func<T0, T1, TR>(T0 a0, T1 a1);

        /// <summary>
        /// Generic delegate with 3 parameters useful to construct delegate types on the fly, e.g. for implicit usage in lambda expressions.
        /// </summary>
        /// <typeparam name="T0">Type of the first parameter.</typeparam>
        /// <typeparam name="T1">Type of the second parameter.</typeparam>
        /// <typeparam name="T2">Type of the third parameter.</typeparam>
        /// <typeparam name="TR">Return type of the function.</typeparam>
        /// <param name="a0">Dummy variable to denote the first parameter.</param>
        /// <param name="a1">Dummy variable to denote the second parameter.</param>
        /// <param name="a2">Dummy variable to denote the third parameter.</param>
        /// <returns></returns>
        public delegate TR Func<T0, T1, T2, TR>(T0 a0, T1 a1, T2 a2);

        /// <summary>
        /// Generic delegate with 4 parameters useful to construct delegate types on the fly, e.g. for implicit usage in lambda expressions.
        /// </summary>
        /// <typeparam name="T0">Type of the first parameter.</typeparam>
        /// <typeparam name="T1">Type of the second parameter.</typeparam>
        /// <typeparam name="T2">Type of the third parameter.</typeparam>
        /// <typeparam name="T3">Type of the fourth parameter.</typeparam>
        /// <typeparam name="TR">Return type of the function.</typeparam>
        /// <param name="a0">Dummy variable to denote the first parameter.</param>
        /// <param name="a1">Dummy variable to denote the second parameter.</param>
        /// <param name="a2">Dummy variable to denote the third parameter.</param>
        /// <param name="a3">Dummy variable to denote the fourth parameter.</param>
        /// <returns></returns>
        public delegate TR Func<T0, T1, T2, T3, TR>(T0 a0, T1 a1, T2 a2, T3 a3);

        #endregion

        #region 1.2 The Sequence class

        /// <summary>
        /// Static class with definitions of the LINQ Standard Query Operators. The majority of the Standard Query Operators are extension methods that extend <c>IEnumerable&lt;T&gt;</c>. Taken together, the methods compose to form a complete query language for arrays and collections that implement <c>IEnumerable&lt;T&gt;</c>.
        /// </summary>
        public static class Sequence
        {
            #region 1.3 Restriction operators

            #region 1.3.1 Where

            /// <summary>
            /// Filters a sequence based on a predicate. When the returned sequence is enumerated, it enumerates the source sequence and yields those elements for which the <paramref name="predicate">predicate</paramref> function returns true.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to filter.</param>
            /// <param name="predicate">Predicate to filter elements of the source sequence. The first argument of the predicate function represents the element to test.</param>
            /// <returns>Filtered sequence.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            {
                if (source == null || predicate == null)
                    throw new ArgumentNullException();

                foreach (T item in source)
                    if (predicate(item))
                        yield return item;
            }

            /// <summary>
            /// Filters a sequence based on a predicate. When the returned sequence is enumerated, it enumerates the source sequence and yields those elements for which the predicate function returns true.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to filter.</param>
            /// <param name="predicate">Predicate to filter elements of the source sequence. The first argument of the predicate function represents the element to test. The second argument represents the zero based index of the element within the source sequence.</param>
            /// <returns>Filtered sequence.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, int, bool> predicate)
            {
                if (source == null || predicate == null)
                    throw new ArgumentNullException();

                int i = 0;
                foreach (T item in source)
                    if (predicate(item, i++))
                        yield return item;
            }

            #endregion

            #endregion

            #region 1.4 Projection operators

            #region 1.4.1 Select

            /// <summary>
            /// Performs a projection over a sequence with a one-to-many source-projection mapping. When the returned sequence is enumerated, it enumerates the source sequence and yields the results of evaluating the <paramref name="selector">selector</paramref> function for each element.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="S">Type of the projection results.</typeparam>
            /// <param name="source">Sequence to perform the projection over.</param>
            /// <param name="selector">Selector function to generate projections for the elements in the source sequence. The first argument of the selector function represents the element to process.</param>
            /// <returns>Sequence with the projected elements.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<S> Select<T, S>(this IEnumerable<T> source, Func<T, S> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                foreach (T item in source)
                    yield return selector(item);
            }

            /// <summary>
            /// Performs a projection over a sequence with a one-to-many source-projection mapping. When returned sequence is enumerated, it enumerates the source sequence and yields the results of evaluating the <paramref name="selector">selector</paramref> function for each element.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="S">Type of the projection results.</typeparam>
            /// <param name="source">Sequence to perform the projection over.</param>
            /// <param name="selector">Selector function to generate projections for the elements in the source sequence. The first argument of the selector function represents the element to process. The second argument represents the zero based index of the element within the source sequence.</param>
            /// <returns>Sequence with the projected elements.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<S> Select<T, S>(this IEnumerable<T> source, Func<T, int, S> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                int i = 0;
                foreach (T item in source)
                    yield return selector(item, i++);
            }

            #endregion

            #region 1.4.2 SelectMany

            /// <summary>
            /// Performs a projection over a sequence with a one-to-many source-projection mapping. When the returned sequence is enumerated, it enumerates the source sequence, maps each element to an enumerable object using the <paramref name="selector">selector</paramref> function, and enumerates and yields the elements of each such enumerable object.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="S">Type of the projection results.</typeparam>
            /// <param name="source">Sequence to perform the projection over.</param>
            /// <param name="selector">Selector function to generate projection sequences for the elements in the source sequence. The first argument of the selector function represents the element to process.</param>
            /// <returns>Sequence with the projected elements.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<S> SelectMany<T, S>(this IEnumerable<T> source, Func<T, IEnumerable<S>> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                foreach (T item in source)
                    foreach (S child in selector(item))
                        yield return child;
            }

            /// <summary>
            /// Performs a projection over a sequence with a one-to-many source-projection mapping. When the returned sequence is enumerated, it enumerates the source sequence, maps each element to an enumerable object using the <paramref name="selector">selector</paramref> function, and enumerates and yields the elements of each such enumerable object.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="S">Type of the projection results.</typeparam>
            /// <param name="source">Sequence to perform the projection over.</param>
            /// <param name="selector">Selector function to generate projection sequences for the elements in the source sequence. The first argument of the selector function represents the element to process. The second argument represents the zero based index of the element within the source sequence.</param>
            /// <returns>Sequence with the projected elements.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<S> SelectMany<T, S>(this IEnumerable<T> source, Func<T, int, IEnumerable<S>> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                int i = 0;
                foreach (T item in source)
                    foreach (S child in selector(item, i++))
                        yield return child;
            }

            #endregion

            #endregion

            #region 1.5 Partitioning operators

            #region 1.5.1 Take

            /// <summary>
            /// Yields a given number of elements from a sequence and then skips the remainder of the sequence. When the returned sequence is enumerated, it enumerates the source sequence and yields elements until the number of elements given by the <paramref name="count">count</paramref> argument have been yielded or the end of the source is reached. If the <paramref name="count">count</paramref> argument is less than or equal to zero, the source sequence is not enumerated and no elements are yielded.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to yield elements from.</param>
            /// <param name="count">Number of elements to yield from the source sequence.</param>
            /// <returns>Sequence with the first <paramref name="count">count</paramref> elements.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. The <c>Take</c> and <c>Skip</c> operators are functional complements: For a given sequence <c>s</c>, the concatenation of <c>s.Take(n)</c> and <c>s.Skip(n)</c> yields the same sequence as <c>s</c>.</remarks>
            public static IEnumerable<T> Take<T>(this IEnumerable<T> source, int count)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();
                for (int i = 0; i < count && enumerator.MoveNext(); i++)
                    yield return enumerator.Current;
            }

            #endregion

            #region 1.5.2 Skip

            /// <summary>
            /// Skips a given number of elements from a sequence and then yields the remainder of the sequence. When the returned sequence is enumerated, it enumerates the source sequence, skipping the number of elements given by the <paramref name="count">count</paramref> argument and yielding the rest. If the source sequence contains fewer elements than given by the <paramref name="count">count</paramref> argument, nothing is yielded. If the <paramref name="count">count</paramref> argument is less an or equal to zero, all elements of the source sequence are yielded.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to yield elements from.</param>
            /// <param name="count">Number of elements to skip before yielding from the source sequence.</param>
            /// <returns>Sequence with the remaining elements after skipping <paramref name="count">count</paramref> elements.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. The <c>Take</c> and <c>Skip</c> operators are functional complements: For a given sequence <c>s</c>, the concatenation of <c>s.Take(n)</c> and <c>s.Skip(n)</c> yields the same sequence as <c>s</c>.</remarks>
            public static IEnumerable<T> Skip<T>(this IEnumerable<T> source, int count)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();
                for (int i = 0; i < count && enumerator.MoveNext(); i++)
                    ;

                while (enumerator.MoveNext())
                    yield return enumerator.Current;
            }

            #endregion

            #region 1.5.3 TakeWhile

            /// <summary>
            /// Yields elements from a sequence while a test is true and then skips the remainder of the sequence. When the returned sequence is enumerated, it enumerates the source sequence, testing each element using the <paramref name="predicate">predicate</paramref> function and yielding the element if the result was true. The enumeration stops when the <paramref name="predicate">predicate</paramref> function returns false or the end of the source sequence is reached.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to yield elements from.</param>
            /// <param name="predicate">Predicate to decide when to stop yielding (on first return of <c>false</c>) from the source sequence. The first argument of the predicate function represents the element to test.</param>
            /// <returns>Head of the source sequence, ending with the last element for which the <paramref name="predicate">predicate</paramref> function evaluates to true.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. The <c>TakeWhile</c> and <c>SkipWhile</c> operators are functional complements: For a given sequence <c>s</c> and a predicate function <c>p</c>, the concatenation of <c>s.TakeWhile(p)</c> and <c>s.SkipWhile(p)</c> yields the same sequence as <c>s</c>.</remarks>
            public static IEnumerable<T> TakeWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            {
                if (source == null || predicate == null)
                    throw new ArgumentNullException();

                foreach (T item in source)
                    if (predicate(item))
                        yield return item;
                    else
                        break;
            }

            /// <summary>
            /// Yields elements from a sequence while a test is true and then skips the remainder of the sequence. When the returned sequence is enumerated, it enumerates the source sequence, testing each element using the <paramref name="predicate">predicate</paramref> function and yielding the element if the result was true. The enumeration stops when the <paramref name="predicate">predicate</paramref> function returns false or the end of the source sequence is reached.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to yield elements from.</param>
            /// <param name="predicate">Predicate to decide when to stop yielding (on first return of <c>false</c>) from the source sequence. The first argument of the predicate function represents the element to test. The second argument represents the zero based index of the element within the source sequence.</param>
            /// <returns>Head of the source sequence, ending with the last element for which the <paramref name="predicate">predicate</paramref> function evaluates to true.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. The <c>TakeWhile</c> and <c>SkipWhile</c> operators are functional complements: For a given sequence <c>s</c> and a predicate function <c>p</c>, the concatenation of <c>s.TakeWhile(p)</c> and <c>s.SkipWhile(p)</c> yields the same sequence as <c>s</c>.</remarks>
            public static IEnumerable<T> TakeWhile<T>(this IEnumerable<T> source, Func<T, int, bool> predicate)
            {
                if (source == null || predicate == null)
                    throw new ArgumentNullException();

                int i = 0;
                foreach (T item in source)
                    if (predicate(item, i++))
                        yield return item;
                    else
                        break;
            }

            #endregion

            #region 1.5.4 SkipWhile

            /// <summary>
            /// Skips elements from a sequence while a test is true and then yields the remainder of the sequence. When the returned sequence is enumerated, it enumerates the source sequence, testing each element using the <paramref name="predicate">predicate</paramref> function and skipping the element if the result was true. Once the predicate function returns false for an element, that element and the remaining elements are yielded with no further invocations of the <paramref name="predicate">predicate</paramref> function. If the <paramref name="predicate">predicate</paramref> function returns true for all elements in the sequence, no elements are yielded.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to yield elements from.</param>
            /// <param name="predicate">Predicate to decide when to start yielding (on first return of <c>false</c>) from the source sequence. The first argument of the predicate function represents the element to test.</param>
            /// <returns>Tail of the source sequence, starting with the first element for which the <paramref name="predicate">predicate</paramref> function evaluates to false.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. The <c>TakeWhile</c> and <c>SkipWhile</c> operators are functional complements: For a given sequence <c>s</c> and a predicate function <c>p</c>, the concatenation of <c>s.TakeWhile(p)</c> and <c>s.SkipWhile(p)</c> yields the same sequence as <c>s</c>.</remarks>
            public static IEnumerable<T> SkipWhile<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            {
                if (source == null || predicate == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();
                if (enumerator.MoveNext())
                {
                    do
                    {
                        if (!predicate(enumerator.Current))
                        {
                            yield return enumerator.Current;
                            break;
                        }
                    } while (enumerator.MoveNext());

                    while (enumerator.MoveNext())
                        yield return enumerator.Current;
                }
            }

            /// <summary>
            /// Skips elements from a sequence while a test is true and then yields the remainder of the sequence. When the returned sequence is enumerated, it enumerates the source sequence, testing each element using the <paramref name="predicate">predicate</paramref> function and skipping the element if the result was true. Once the predicate function returns false for an element, that element and the remaining elements are yielded with no further invocations of the <paramref name="predicate">predicate</paramref> function. If the <paramref name="predicate">predicate</paramref> function returns true for all elements in the sequence, no elements are yielded.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to yield elements from.</param>
            /// <param name="predicate">Predicate to decide when to start yielding (on first return of <c>false</c>) from the source sequence. The first argument of the predicate function represents the element to test. The second argument represents the zero based index of the element within the source sequence.</param>
            /// <returns>Tail of the source sequence, starting with the first element for which the <paramref name="predicate">predicate</paramref> function evaluates to false.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. The <c>TakeWhile</c> and <c>SkipWhile</c> operators are functional complements: For a given sequence <c>s</c> and a predicate function <c>p</c>, the concatenation of <c>s.TakeWhile(p)</c> and <c>s.SkipWhile(p)</c> yields the same sequence as <c>s</c>.</remarks>
            public static IEnumerable<T> SkipWhile<T>(this IEnumerable<T> source, Func<T, int, bool> predicate)
            {
                if (source == null || predicate == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();
                if (enumerator.MoveNext())
                {
                    int i = 0;
                    do
                    {
                        if (!predicate(enumerator.Current, i++))
                        {
                            yield return enumerator.Current;
                            break;
                        }
                    } while (enumerator.MoveNext());

                    while (enumerator.MoveNext())
                        yield return enumerator.Current;
                }
            }

            #endregion

            #endregion

            #region 1.6 Join operators

            #region 1.6.1 Join

            /// <summary>
            /// Performs an inner join of two sequences based on matching keys extracted from the elements. When the returned sequence is enumerated, it first enumerates the <paramref name="inner">inner</paramref> sequence and evaluates the <paramref name="innerKeySelector">innerKeySelector</paramref> function once for each inner element, collecting the elements by their keys in a hash table. Once all inner elements and keys have been collected, the <paramref name="outer">outer</paramref> sequence is enumerated. For each outer element, the <paramref name="outerKeySelector">outerKeySelector</paramref> function is evaluated and the resulting key is used to look up the corresponding inner elements in the hash table. For each matching inner element (if any), the <paramref name="resultSelector">resultSelector</paramref> function is evaluated for the outer and inner element pair, and the resulting object is yielded.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the outer source sequence.</typeparam>
            /// <typeparam name="U">Type of the elements in the inner source sequence.</typeparam>
            /// <typeparam name="K">Type of the key field to perform the join on.</typeparam>
            /// <typeparam name="V">Type of the elements in the result sequence.</typeparam>
            /// <param name="outer">Outer sequence to perform the join with.</param>
            /// <param name="inner">Inner sequence to perform the join with.</param>
            /// <param name="outerKeySelector">Function that extracts the join key values from elements of the <paramref name="outer">outer</paramref> sequence.</param>
            /// <param name="innerKeySelector">Function that extracts the join key values from elements of the <paramref name="inner">inner</paramref> sequence.</param>
            /// <param name="resultSelector">Function that generates a join result of type <typeparamref name="V">V</typeparamref> based on a pair of one outer element (type <typeparamref name="T">T</typeparamref>) and one inner element (type <typeparamref name="U">U</typeparamref>).</param>
            /// <returns>Sequence with elements resulting from a join operation on the outer and inner sequence based on a specified key.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. The <c>Join</c> operator preserves the order of the <paramref name="outer">outer</paramref> sequence elements, and for each outer element, the order of the matching <paramref name="inner">inner</paramref> sequence elements. In relational database terms, the <c>Join</c> operator implements an inner equijoin. Other join operations, such as left outer join and right outer join have no dedicated standard query operators, but are subsets of the capabilities of the <see cref="GroupJoin&lt;T, U, K, V&gt;(IEnumerable&lt;T&gt;, IEnumerable&lt;U&gt;, Func&lt;T, K&gt;, Func&lt;U, K&gt;, Func&lt;T, IEnumerable&lt;U&gt;, V&gt;)">GroupJoin</see> operator.</remarks>
            public static IEnumerable<V> Join<T, U, K, V>(this IEnumerable<T> outer, IEnumerable<U> inner, Func<T, K> outerKeySelector, Func<U, K> innerKeySelector, Func<T, U, V> resultSelector)
            {
                if (outer == null || inner == null || outerKeySelector == null || innerKeySelector == null || resultSelector == null)
                    throw new ArgumentNullException();

                Lookup<K, U> innerLookup = ToLookup(inner, innerKeySelector);

                foreach (T o in outer)
                    foreach (U i in innerLookup[outerKeySelector(o)])
                        yield return resultSelector(o, i);
            }

            #endregion

            #region 1.6.2 GroupJoin

            /// <summary>
            /// Performs a grouped join of two sequences based on matching keys extracted from the elements. When the returned sequence is enumerated, it first enumerates the <paramref name="inner">inner</paramref> sequence and evaluates the <paramref name="innerKeySelector">innerKeySelector</paramref> function once for each inner element, collecting the elements by their keys in a hash table. Once all inner elements and keys have been collected, the <paramref name="outer">outer</paramref> sequence is enumerated. For each outer element, the <paramref name="outerKeySelector">outerKeySelector</paramref> function is evaluated, the resulting key is used to look up the corresponding inner elements in the hash table, the <paramref name="resultSelector">resultSelector</paramref> function is evaluated for the outer element and the (possibly empty) sequence of matching inner elements, and the resulting object is yielded.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the outer source sequence.</typeparam>
            /// <typeparam name="U">Type of the elements in the inner source sequence.</typeparam>
            /// <typeparam name="K">Type of the key field to perform the join on.</typeparam>
            /// <typeparam name="V">Type of the elements in the result sequence.</typeparam>
            /// <param name="outer">Outer sequence to perform the join with.</param>
            /// <param name="inner">Inner sequence to perform the join with.</param>
            /// <param name="outerKeySelector">Function that extracts the join key values from elements of the <paramref name="outer">outer</paramref> sequence.</param>
            /// <param name="innerKeySelector">Function that extracts the join key values from elements of the <paramref name="inner">inner</paramref> sequence.</param>
            /// <param name="resultSelector">Function that generates a join result of type <typeparamref name="V">V</typeparamref> based on a pair of one outer element (type <typeparamref name="T">T</typeparamref>) and a (possibly empty) sequence of inner elements (type <typeparamref name="U">U</typeparamref>).</param>
            /// <returns>Sequence with elements resulting from a group join operation on the outer and inner sequence based on a specified key.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. The <c>GroupJoin</c> operator preserves the order of the <paramref name="outer">outer</paramref> sequence elements, and for each outer element, the order of the matching <paramref name="inner">inner</paramref> sequence elements. The <c>GroupJoin</c> operator produces hierarchical results (outer elements paired with sequences of matching inner elements) and has no direct equivalent in traditional relational database terms.</remarks>
            public static IEnumerable<V> GroupJoin<T, U, K, V>(this IEnumerable<T> outer, IEnumerable<U> inner, Func<T, K> outerKeySelector, Func<U, K> innerKeySelector, Func<T, IEnumerable<U>, V> resultSelector)
            {
                if (outer == null || inner == null || outerKeySelector == null || innerKeySelector == null || resultSelector == null)
                    throw new ArgumentNullException();

                Lookup<K, U> innerLookup = ToLookup(inner, innerKeySelector);

                foreach (T o in outer)
                {
                    K key = outerKeySelector(o);
                    if (innerLookup.Contains(key))
                        yield return resultSelector(o, innerLookup[key]);
                    else
                        yield return resultSelector(o, new List<U>());
                }
            }

            #endregion

            #endregion

            #region 1.7 Concatenation operator

            #region 1.7.1 Concat

            /// <summary>
            /// Concatenates two sequences. When the returned sequence is enumerated, it enumerates the first sequence, yielding each element, and then enumerates the second sequence, yielding each element.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="first">First sequence to concatenate.</param>
            /// <param name="second">Second sequence to concatenate.</param>
            /// <returns>Sequence yielding the first and second sequence in a consecutive order.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, IEnumerable<T> second)
            {
                if (first == null || second == null)
                    throw new ArgumentNullException();

                foreach (T item in first)
                    yield return item;

                foreach (T item in second)
                    yield return item;
            }

            #endregion

            #endregion

            #region 1.8 Ordering operators

            #region 1.8.1 OrderBy / ThenBy

            #region OrderBy

            /// <summary>
            /// Sorts a sequence based on extracted key values in ascending order using the default comparer for the key type <typeparamref name="K">K</typeparamref>. When the returned sequence is enumerated, it first enumerates the source sequence, collecting all elements; then evaluates the <paramref name="keySelector">keySelector</paramref> function once for each element, collecting the key values to order by; then sorts the elements according to the collected key values in ascending order; and finally, yields the elements in the resulting order.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key on which the sort operation is performed.</typeparam>
            /// <param name="source">Sequence to sort elements from.</param>
            /// <param name="keySelector">Function that extracts the sort key values from the source sequence to perform the sort operation on.</param>
            /// <returns>Sequence yielding the source sequence elements in ascending order according to the extracted key values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. The <c>OrderBy</c> operator performs an unstable sort; that is, if the key values of two elements are equal, the order of the elements might not be preserved. In contrast, a stable sort preserves the order of elements that have equal key values. The <c>OrderBy</c> operator establishes a primary ordering, for subsequent orderings use the <c>ThenBy</c> family of operators.</remarks>
            public static OrderedSequence<T> OrderBy<T, K>(this IEnumerable<T> source, Func<T, K> keySelector)
            {
                return OrderBy(source, keySelector, Comparer<K>.Default);
            }

            /// <summary>
            /// Sorts a sequence based on extracted key values in ascending order using the specified comparer for the key type <typeparamref name="K">K</typeparamref>. When the returned sequence is enumerated, it first enumerates the source sequence, collecting all elements; then evaluates the <paramref name="keySelector">keySelector</paramref> function once for each element, collecting the key values to order by; then sorts the elements according to the collected key values in ascending order; and finally, yields the elements in the resulting order.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key on which the sort operation is performed.</typeparam>
            /// <param name="source">Sequence to sort elements from.</param>
            /// <param name="keySelector">Function that extracts the sort key values from the source sequence to perform the sort operation on.</param>
            /// <param name="comparer">Comparer object to compare the extracted key values for sorting purposes.</param>
            /// <returns>Sequence yielding the source sequence elements in ascending order according to the extracted key values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. The <c>OrderBy</c> operator performs an unstable sort; that is, if the key values of two elements are equal, the order of the elements might not be preserved. In contrast, a stable sort preserves the order of elements that have equal key values. The <c>OrderBy</c> operator establishes a primary ordering, for subsequent orderings use the <c>ThenBy</c> family of operators.</remarks>
            public static OrderedSequence<T> OrderBy<T, K>(this IEnumerable<T> source, Func<T, K> keySelector, IComparer<K> comparer)
            {
                return OrderByInternal(source, keySelector, comparer, false);
            }

            /// <summary>
            /// Sorts a sequence based on extracted key values in descending order using the default comparer for the key type <typeparamref name="K">K</typeparamref>. When the returned sequence is enumerated, it first enumerates the source sequence, collecting all elements; then evaluates the <paramref name="keySelector">keySelector</paramref> function once for each element, collecting the key values to order by; then sorts the elements according to the collected key values in descending order; and finally, yields the elements in the resulting order.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key on which the sort operation is performed.</typeparam>
            /// <param name="source">Sequence to sort elements from.</param>
            /// <param name="keySelector">Function that extracts the sort key values from the source sequence to perform the sort operation on.</param>
            /// <returns>Sequence yielding the source sequence elements in descending order according to the extracted key values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. The <c>OrderBy</c> operator performs an unstable sort; that is, if the key values of two elements are equal, the order of the elements might not be preserved. In contrast, a stable sort preserves the order of elements that have equal key values. The <c>OrderBy</c> operator establishes a primary ordering, for subsequent orderings use the <c>ThenBy</c> family of operators.</remarks>
            public static OrderedSequence<T> OrderByDescending<T, K>(this IEnumerable<T> source, Func<T, K> keySelector)
            {
                return OrderByDescending(source, keySelector, Comparer<K>.Default);
            }

            /// <summary>
            /// Sorts a sequence based on extracted key values in descending order using the specified comparer for the key type <typeparamref name="K">K</typeparamref>. When the returned sequence is enumerated, it first enumerates the source sequence, collecting all elements; then evaluates the <paramref name="keySelector">keySelector</paramref> function once for each element, collecting the key values to order by; then sorts the elements according to the collected key values in descending order; and finally, yields the elements in the resulting order.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key on which the sort operation is performed.</typeparam>
            /// <param name="source">Sequence to sort elements from.</param>
            /// <param name="keySelector">Function that extracts the sort key values from the source sequence to perform the sort operation on.</param>
            /// <param name="comparer">Comparer object to compare the extracted key values for sorting purposes.</param>
            /// <returns>Sequence yielding the source sequence elements in descending order according to the extracted key values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. The <c>OrderBy</c> operator performs an unstable sort; that is, if the key values of two elements are equal, the order of the elements might not be preserved. In contrast, a stable sort preserves the order of elements that have equal key values. The <c>OrderBy</c> operator establishes a primary ordering, for subsequent orderings use the <c>ThenBy</c> family of operators.</remarks>
            public static OrderedSequence<T> OrderByDescending<T, K>(this IEnumerable<T> source, Func<T, K> keySelector, IComparer<K> comparer)
            {
                return OrderByInternal(source, keySelector, comparer, true);
            }

            #endregion

            #region ThenBy

            /// <summary>
            /// Sorts a sequence, which has been sorted before using an <c>OrderBy</c> operator invocation and one or more <c>ThenBy</c> operator invocations, based on extracted key values in ascending order using the default comparer for the key type <typeparamref name="K">K</typeparamref>. When the returned sequence is enumerated, it first enumerates the source sequence, collecting all elements; then evaluates the <paramref name="keySelector">keySelector</paramref> function once for each element, collecting the key values to order by; then sorts the elements according to the collected key values in ascending order; and finally, yields the elements in the resulting order.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key on which the sort operation is performed.</typeparam>
            /// <param name="source">Sequence to sort elements from.</param>
            /// <param name="keySelector">Function that extracts the sort key values from the source sequence to perform the sort operation on.</param>
            /// <returns>Sequence yielding the source sequence elements in ascending order according to the extracted key values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. The <c>ThenBy</c> operator performs an unstable sort; that is, if the key values of two elements are equal, the order of the elements might not be preserved. In contrast, a stable sort preserves the order of elements that have equal key values. The <c>ThenBy</c> operator establishes an n-ary (n &gt; 1) ordering, after a primary ordering using the <c>OrderBy</c> family of operators and a series of higher order m-ary orderings (1 &lt; m &lt; n) using the <c>ThenBy</c> family of operators.</remarks>
            public static OrderedSequence<T> ThenBy<T, K>(this OrderedSequence<T> source, Func<T, K> keySelector)
            {
                return ThenBy(source, keySelector, Comparer<K>.Default);
            }

            /// <summary>
            /// Sorts a sequence, which has been sorted before using an <c>OrderBy</c> operator invocation and one or more <c>ThenBy</c> operator invocations, based on extracted key values in ascending order using the specified comparer for the key type <typeparamref name="K">K</typeparamref>. When the returned sequence is enumerated, it first enumerates the source sequence, collecting all elements; then evaluates the <paramref name="keySelector">keySelector</paramref> function once for each element, collecting the key values to order by; then sorts the elements according to the collected key values in ascending order; and finally, yields the elements in the resulting order.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key on which the sort operation is performed.</typeparam>
            /// <param name="source">Sequence to sort elements from.</param>
            /// <param name="keySelector">Function that extracts the sort key values from the source sequence to perform the sort operation on.</param>
            /// <param name="comparer">Comparer object to compare the extracted key values for sorting purposes.</param>
            /// <returns>Sequence yielding the source sequence elements in ascending order according to the extracted key values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. The <c>ThenBy</c> operator performs an unstable sort; that is, if the key values of two elements are equal, the order of the elements might not be preserved. In contrast, a stable sort preserves the order of elements that have equal key values. The <c>ThenBy</c> operator establishes an n-ary (n &gt; 1) ordering, after a primary ordering using the <c>OrderBy</c> family of operators and a series of higher order m-ary orderings (1 &lt; m &lt; n) using the <c>ThenBy</c> family of operators.</remarks>
            public static OrderedSequence<T> ThenBy<T, K>(this OrderedSequence<T> source, Func<T, K> keySelector, IComparer<K> comparer)
            {
                return ThenByInternal(source, keySelector, comparer, false);
            }

            /// <summary>
            /// Sorts a sequence, which has been sorted before using an <c>OrderBy</c> operator invocation and one or more <c>ThenBy</c> operator invocations, based on extracted key values in descending order using the default comparer for the key type <typeparamref name="K">K</typeparamref>. When the returned sequence is enumerated, it first enumerates the source sequence, collecting all elements; then evaluates the <paramref name="keySelector">keySelector</paramref> function once for each element, collecting the key values to order by; then sorts the elements according to the collected key values in descending order; and finally, yields the elements in the resulting order.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key on which the sort operation is performed.</typeparam>
            /// <param name="source">Sequence to sort elements from.</param>
            /// <param name="keySelector">Function that extracts the sort key values from the source sequence to perform the sort operation on.</param>
            /// <returns>Sequence yielding the source sequence elements in descending order according to the extracted key values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. The <c>ThenBy</c> operator performs an unstable sort; that is, if the key values of two elements are equal, the order of the elements might not be preserved. In contrast, a stable sort preserves the order of elements that have equal key values. The <c>ThenBy</c> operator establishes an n-ary (n &gt; 1) ordering, after a primary ordering using the <c>OrderBy</c> family of operators and a series of higher order m-ary orderings (1 &lt; m &lt; n) using the <c>ThenBy</c> family of operators.</remarks>
            public static OrderedSequence<T> ThenByDescending<T, K>(this OrderedSequence<T> source, Func<T, K> keySelector)
            {
                return ThenByDescending(source, keySelector, Comparer<K>.Default);
            }

            /// <summary>
            /// Sorts a sequence, which has been sorted before using an <c>OrderBy</c> operator invocation and one or more <c>ThenBy</c> operator invocations, based on extracted key values in descending order using the specified comparer for the key type <typeparamref name="K">K</typeparamref>. When the returned sequence is enumerated, it first enumerates the source sequence, collecting all elements; then evaluates the <paramref name="keySelector">keySelector</paramref> function once for each element, collecting the key values to order by; then sorts the elements according to the collected key values in descending order; and finally, yields the elements in the resulting order.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key on which the sort operation is performed.</typeparam>
            /// <param name="source">Sequence to sort elements from.</param>
            /// <param name="keySelector">Function that extracts the sort key values from the source sequence to perform the sort operation on.</param>
            /// <param name="comparer">Comparer object to compare the extracted key values for sorting purposes.</param>
            /// <returns>Sequence yielding the source sequence elements in descending order according to the extracted key values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. The <c>ThenBy</c> operator performs an unstable sort; that is, if the key values of two elements are equal, the order of the elements might not be preserved. In contrast, a stable sort preserves the order of elements that have equal key values. The <c>ThenBy</c> operator establishes an n-ary (n &gt; 1) ordering, after a primary ordering using the <c>OrderBy</c> family of operators and a series of higher order m-ary orderings (1 &lt; m &lt; n) using the <c>ThenBy</c> family of operators.</remarks>
            public static OrderedSequence<T> ThenByDescending<T, K>(this OrderedSequence<T> source, Func<T, K> keySelector, IComparer<K> comparer)
            {
                return ThenByInternal(source, keySelector, comparer, true);
            }

            #endregion

            #region Internal implementation

            internal static OrderedSequence<T> OrderByInternal<T, K>(IEnumerable<T> source, Func<T, K> keySelector, IComparer<K> comparer, bool descending)
            {
                if (source == null || keySelector == null)
                    throw new ArgumentNullException();

                return OrderByInternal2(source, keySelector, comparer, descending);
            }

            internal static OrderedSequence<T> OrderByInternal2<T, K>(IEnumerable<T> source, Func<T, K> keySelector, IComparer<K> comparer, bool descending)
            {
                SortedList<K, List<T>> lst = new SortedList<K, List<T>>((descending ? new ReverseComparer<K>(comparer) : comparer));

                foreach (T item in source)
                {
                    K key = keySelector(item);

                    if (!lst.ContainsKey(key))
                        lst[key] = new List<T>();
                    lst[key].Add(item);
                }

                return new OrderedSequence<T>(lst.Values);
            }

            internal static OrderedSequence<T> ThenByInternal<T, K>(OrderedSequence<T> source, Func<T, K> keySelector, IComparer<K> comparer, bool descending)
            {
                if (source == null || keySelector == null)
                    throw new ArgumentNullException();

                OrderedSequence<T> res = source.Clone();
                ThenByInternal2<T, K>(res, keySelector, comparer, descending);
                return res;
            }

            internal static void ThenByInternal2<T, K>(OrderedSequence<T> o, Func<T, K> keySelector, IComparer<K> comparer, bool descending)
            {
                if (o.IsLeaf)
                {
                    List<OrderedSequence<T>> lst = new List<OrderedSequence<T>>();
                    foreach (IList<T> bucket in o.Items)
                        lst.Add(OrderByInternal2(bucket, keySelector, comparer, descending));

                    o.Patch(lst);
                }
                else
                {
                    foreach (OrderedSequence<T> child in o.Children)
                        ThenByInternal2<T, K>(child, keySelector, comparer, descending);
                }
            }

            #endregion

            #endregion

            #region 1.8.2 Reverse

            /// <summary>
            /// Reverses the elements of a sequence. When the returned sequence is enumerated, it enumerates the source sequence, collecting all elements, and then yields the elements of the source sequence in reverse order.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to be yielded in reverse order.</param>
            /// <returns>Sequence yielding the source sequence elements in reverse order.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<T> Reverse<T>(this IEnumerable<T> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                List<T> lst = new List<T>();
                foreach (T item in source)
                    lst.Add(item);

                for (int i = lst.Count - 1; i >= 0; i--)
                    yield return lst[i];
            }

            #endregion

            #endregion

            #region 1.9 Grouping operators

            #region 1.9.1 GroupBy

            /// <summary>
            /// Groups a sequence based on extracted key values using the default equality comparer for the key type <typeparamref name="K">K</typeparamref>. When the returned sequence is enumerated, it enumerates source and evaluates the <paramref name="keySelector">keySelector</paramref> function once for each source element. Once all key and destination element pairs have been collected, a sequence of <see cref="IGrouping&lt;K, T&gt;">IGrouping</see>&lt;<typeparamref name="K">K</typeparamref>, <typeparamref name="T">T</typeparamref>&gt; instances are yielded. Each <see cref="IGrouping&lt;K, T&gt;">IGrouping</see>&lt;<typeparamref name="K">K</typeparamref>, <typeparamref name="T">T</typeparamref>&gt; instance represents a sequence of destination elements with a particular key value. The groupings are yielded in the order that their key values first occurred in the source sequence, and destination elements within a grouping are yielded in the order their source elements occurred in the source sequence. When creating the groupings, key values are compared using the default equality comparer, EqualityComparer&lt;<typeparamref name="K">K</typeparamref>&gt;.Default.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key on which the group operation is performed.</typeparam>
            /// <param name="source">Sequence to group elements from.</param>
            /// <param name="keySelector">Function that extracts the group key values from the source sequence to perform the group operation on.</param>
            /// <returns>Sequence of <see cref="IGrouping&lt;K, T&gt;">IGrouping</see>&lt;<typeparamref name="K">K</typeparamref>, <typeparamref name="T">T</typeparamref>&gt; elements each representing a sequence of elements with a particular key value.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<IGrouping<K, T>> GroupBy<T, K>(this IEnumerable<T> source, Func<T, K> keySelector)
            {
                return GroupBy<T, K>(source, keySelector, EqualityComparer<K>.Default);
            }

            /// <summary>
            /// Groups a sequence based on extracted key values using the specified equality comparer for the key type <typeparamref name="K">K</typeparamref>. When the returned sequence is enumerated, it enumerates source and evaluates the <paramref name="keySelector">keySelector</paramref> function once for each source element. Once all key and destination element pairs have been collected, a sequence of <see cref="IGrouping&lt;K, T&gt;">IGrouping</see>&lt;<typeparamref name="K">K</typeparamref>, <typeparamref name="T">T</typeparamref>&gt; instances are yielded. Each <see cref="IGrouping&lt;K, T&gt;">IGrouping</see>&lt;<typeparamref name="K">K</typeparamref>, <typeparamref name="T">T</typeparamref>&gt; instance represents a sequence of destination elements with a particular key value. The groupings are yielded in the order that their key values first occurred in the source sequence, and destination elements within a grouping are yielded in the order their source elements occurred in the source sequence. When creating the groupings, key values are compared using the given <paramref name="comparer">comparer</paramref>, or, if a null <paramref name="comparer">comparer</paramref> was specified, using the default equality comparer, EqualityComparer&lt;<typeparamref name="K">K</typeparamref>&gt;.Default.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key on which the group operation is performed.</typeparam>
            /// <param name="source">Sequence to group elements from.</param>
            /// <param name="keySelector">Function that extracts the group key values from the source sequence to perform the group operation on.</param>
            /// <param name="comparer">Equality comparer to compare the extracted key values for grouping purposes.</param>
            /// <returns>Sequence of <see cref="IGrouping&lt;K, T&gt;">IGrouping</see>&lt;<typeparamref name="K">K</typeparamref>, <typeparamref name="T">T</typeparamref>&gt; elements each representing a sequence of elements with a particular key value.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<IGrouping<K, T>> GroupBy<T, K>(this IEnumerable<T> source, Func<T, K> keySelector, IEqualityComparer<K> comparer)
            {
                return GroupBy<T, K, T>(source, keySelector, e => e, comparer);
            }

            /// <summary>
            /// Groups a sequence based on extracted key values using the default equality comparer for the key type <typeparamref name="K">K</typeparamref>. When the returned sequence is enumerated, it enumerates source and evaluates the <paramref name="keySelector">keySelector</paramref> and <paramref name="elementSelector">elementSelector</paramref> functions once for each source element. Once all key and destination element pairs have been collected, a sequence of <see cref="IGrouping&lt;K, T&gt;">IGrouping</see>&lt;<typeparamref name="K">K</typeparamref>, <typeparamref name="E">E</typeparamref>&gt; instances are yielded. Each <see cref="IGrouping&lt;K, T&gt;">IGrouping</see>&lt;<typeparamref name="K">K</typeparamref>, <typeparamref name="E">E</typeparamref>&gt; instance represents a sequence of destination elements with a particular key value. The groupings are yielded in the order that their key values first occurred in the source sequence, and destination elements within a grouping are yielded in the order their source elements occurred in the source sequence. When creating the groupings, key values are compared using the default equality comparer, EqualityComparer&lt;<typeparamref name="K">K</typeparamref>&gt;.Default.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key on which the group operation is performed.</typeparam>
            /// <typeparam name="E">Type of the result elements.</typeparam>
            /// <param name="source">Sequence to group elements from.</param>
            /// <param name="keySelector">Function that extracts the group key values from the source sequence to perform the group operation on.</param>
            /// <param name="elementSelector">Function that generates result elements for the elements in the source sequence.</param>
            /// <returns>Sequence of <see cref="IGrouping&lt;K, T&gt;">IGrouping</see>&lt;<typeparamref name="K">K</typeparamref>, <typeparamref name="E">E</typeparamref>&gt; elements each representing a sequence of elements with a particular key value.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<IGrouping<K, E>> GroupBy<T, K, E>(this IEnumerable<T> source, Func<T, K> keySelector, Func<T, E> elementSelector)
            {
                return GroupBy<T, K, E>(source, keySelector, elementSelector, EqualityComparer<K>.Default);
            }

            /// <summary>
            /// Groups a sequence based on extracted key values using the specified equality comparer for the key type <typeparamref name="K">K</typeparamref>. When the returned sequence is enumerated, it enumerates source and evaluates the <paramref name="keySelector">keySelector</paramref> and <paramref name="elementSelector">elementSelector</paramref> functions once for each source element. Once all key and destination element pairs have been collected, a sequence of <see cref="IGrouping&lt;K, T&gt;">IGrouping</see>&lt;<typeparamref name="K">K</typeparamref>, <typeparamref name="E">E</typeparamref>&gt; instances are yielded. Each <see cref="IGrouping&lt;K, T&gt;">IGrouping</see>&lt;<typeparamref name="K">K</typeparamref>, <typeparamref name="E">E</typeparamref>&gt; instance represents a sequence of destination elements with a particular key value. The groupings are yielded in the order that their key values first occurred in the source sequence, and destination elements within a grouping are yielded in the order their source elements occurred in the source sequence. When creating the groupings, key values are compared using the given <paramref name="comparer">comparer</paramref>, or, if a null <paramref name="comparer">comparer</paramref> was specified, using the default equality comparer, EqualityComparer&lt;<typeparamref name="K">K</typeparamref>&gt;.Default.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key on which the group operation is performed.</typeparam>
            /// <typeparam name="E">Type of the result elements.</typeparam>
            /// <param name="source">Sequence to group elements from.</param>
            /// <param name="keySelector">Function that extracts the group key values from the source sequence to perform the group operation on.</param>
            /// <param name="elementSelector">Function that generates result elements for the elements in the source sequence.</param>
            /// <param name="comparer">Equality comparer to compare the extracted key values for grouping purposes.</param>
            /// <returns>Sequence of <see cref="IGrouping&lt;K, T&gt;">IGrouping</see>&lt;<typeparamref name="K">K</typeparamref>, <typeparamref name="E">E</typeparamref>&gt; elements each representing a sequence of elements with a particular key value.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<IGrouping<K, E>> GroupBy<T, K, E>(this IEnumerable<T> source, Func<T, K> keySelector, Func<T, E> elementSelector, IEqualityComparer<K> comparer)
            {
                if (source == null || keySelector == null || elementSelector == null) //comparer may be null
                    throw new ArgumentNullException();

                Lookup<K, E> lookup = ToLookup(source, keySelector, elementSelector, comparer);
                foreach (K key in lookup.keys)
                    yield return lookup.dictionary[key];
            }

            #endregion

            #endregion

            #region 1.10 Set operators

            #region 1.10.1 Distinct

            /// <summary>
            /// Eliminates duplicate elements from a sequence. When the returned sequence is enumerated, it enumerates the source sequence, yielding each element that hasn't previously been yielded. Elements are compared using their <c>GetHashCode</c> and <c>Equals</c> methods.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to eliminate duplicate elements from.</param>
            /// <returns>Sequence yielding the distinct elements from the source sequence in order of occurrence.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<T> Distinct<T>(this IEnumerable<T> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                Hashtable tbl = new Hashtable();
                foreach (T item in source)
                {
                    if (!tbl.ContainsKey(item))
                    {
                        tbl.Add(item, null);
                        yield return item;
                    }
                }
            }

            #endregion

            #region 1.10.2 Union

            /// <summary>
            /// Produces the set union of two sequences. When the returned sequence is enumerated, it enumerates the first and second sequences, in that order, yielding each element that hasn't previously been yielded. Elements are compared using their <c>GetHashCode</c> and <c>Equals</c> methods.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequences.</typeparam>
            /// <param name="first">First sequence to include in the set union.</param>
            /// <param name="second">Second sequence to include in the set union.</param>
            /// <returns>Sequence yielding the set union of the <paramref name="first">first</paramref> sequence and <paramref name="second">second</paramref> sequence in order of occurrence.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<T> Union<T>(this IEnumerable<T> first, IEnumerable<T> second)
            {
                if (first == null || second == null)
                    throw new ArgumentNullException();

                Hashtable tbl = new Hashtable();
                foreach (T item in first)
                {
                    if (!tbl.ContainsKey(item))
                    {
                        tbl.Add(item, null);
                        yield return item;
                    }
                }
                foreach (T item in second)
                {
                    if (!tbl.ContainsKey(item))
                    {
                        tbl.Add(item, null);
                        yield return item;
                    }
                }
            }

            #endregion

            #region 1.10.3 Intersect

            /// <summary>
            /// Produces the set intersection of two sequences. When the returned sequence is enumerated, it enumerates the first sequence, collecting all distinct elements of that sequence. It then enumerates the second sequence, marking those elements that occur in both sequences. It finally yields the marked elements in the order in which they were collected. Elements are compared using their <c>GetHashCode</c> and <c>Equals</c> methods.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequences.</typeparam>
            /// <param name="first">First sequence to apply the set intersection operation on.</param>
            /// <param name="second">Second sequence to apply the set intersection operation on.</param>
            /// <returns>Sequence yielding the set intersection of the <paramref name="first">first</paramref> sequence and <paramref name="second">second</paramref> sequence in order of occurrence.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<T> Intersect<T>(this IEnumerable<T> first, IEnumerable<T> second)
            {
                if (first == null || second == null)
                    throw new ArgumentNullException();

                Dictionary<T, bool> tbl = new Dictionary<T, bool>(); //comparer OK for GetHashCode and Equals?
                List<T> lst = new List<T>();

                foreach (T item in first)
                    if (!tbl.ContainsKey(item))
                    {
                        tbl.Add(item, false);
                        lst.Add(item);
                    }

                foreach (T item in second)
                    if (tbl.ContainsKey(item))
                        tbl[item] = true;

                foreach (T item in lst)
                    if (tbl[item])
                        yield return item;
            }

            #endregion

            #region 1.10.4 Except

            /// <summary>
            /// Produces the set difference between two sequences. When the returned sequence is enumerated, it enumerates the first sequence, collecting all distinct elements of that sequence. It then enumerates the second sequence, removing those elements that were also contained in the first sequence. It finally yields the remaining elements in the order in which they were collected. Elements are compared using their <c>GetHashCode</c> and <c>Equals</c> methods.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequences.</typeparam>
            /// <param name="first">First sequence to apply the subtraction of the <paramref name="second">second</paramref> sequence on.</param>
            /// <param name="second">Second sequence to be subtracted from the <paramref name="first">first</paramref> sequence.</param>
            /// <returns>Sequence yielding the set difference of the <paramref name="first">first</paramref> sequence and <paramref name="second">second</paramref> sequence in order of occurrence.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<T> Except<T>(this IEnumerable<T> first, IEnumerable<T> second)
            {
                if (first == null || second == null)
                    throw new ArgumentNullException();

                Dictionary<T, bool> tbl = new Dictionary<T, bool>(); //comparer OK for GetHashCode and Equals?
                List<T> lst = new List<T>();

                foreach (T item in first)
                    if (!tbl.ContainsKey(item)) //O(1)
                    {
                        tbl.Add(item, true); //[O(1), O(n)]
                        lst.Add(item); //[O(1), O(n)]
                    }

                foreach (T item in second)
                    if (tbl.ContainsKey(item)) //O(1)
                        tbl[item] = false; //O(1)

                foreach (T item in lst)
                    if (tbl[item]) //O(1)
                        yield return item;
            }

            #endregion

            #endregion

            #region 1.11 Conversion operators

            #region 1.11.1 ToSequence

            /// <summary>
            /// Returns the specified sequence typed as IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. The operator has no effect other than to change the compile-time type of the source argument to IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to return.</param>
            /// <returns>The source sequence typed as IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<T> ToSequence<T>(this IEnumerable<T> source)
            {
                return source;
            }

            #endregion

            #region 1.11.2 ToArray

            /// <summary>
            /// Creates an array from a sequence by enumerating the source sequence and returning an array of type <typeparamref name="T">T</typeparamref>[] containing the elements of the sequence.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to create an array for.</param>
            /// <returns>Array of type <typeparamref name="T">T</typeparamref>[] containing the elements of the source sequence in order of occurrence.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static T[] ToArray<T>(this IEnumerable<T> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                List<T> lst = new List<T>();
                foreach (T item in source)
                    lst.Add(item);

                return lst.ToArray();
            }

            #endregion

            #region 1.11.3 ToList

            /// <summary>
            /// Creates a list from a sequence by enumerating the source sequence and returning a list of type List&lt;<typeparamref name="T">T</typeparamref>&gt; containing the elements of the sequence.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to create a list for.</param>
            /// <returns>List of type List&lt;<typeparamref name="T">T</typeparamref>&gt; containing the elements of the source sequence in order of occurrence.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static List<T> ToList<T>(this IEnumerable<T> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                List<T> lst = new List<T>();
                foreach (T item in source)
                    lst.Add(item);

                return lst;
            }

            #endregion

            #region 1.11.4 ToDictionary

            /// <summary>
            /// Enumerates a sequence and creates a Dictionary&lt;<typeparamref name="K">K</typeparamref>,<typeparamref name="T">T</typeparamref>&gt; object out of it, mapping each key value extracted by the <paramref name="keySelector">keySelector</paramref> function on one element from the source sequence. If the produced key value is null or a duplicate key is encountered, a <c>ArgumentNullException</c> is thrown. In the resulting dictionary, key values are compared using the default equality comparer, EqualityComparer&lt;<typeparamref name="K">K</typeparamref>&gt;.Default.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key used in the resulting dictionary.</typeparam>
            /// <param name="source">Sequence to create a dictionary from.</param>
            /// <param name="keySelector">Function that extracts the key values from the source sequence for usage in the resulting dictionary.</param>
            /// <returns>A Dictionary&lt;<typeparamref name="K">K</typeparamref>,<typeparamref name="T">T</typeparamref>&gt; collection mapping a key value extracted by the <paramref name="keySelector">keySelector</paramref> function on the source element, for every the source element.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static Dictionary<K, T> ToDictionary<T, K>(this IEnumerable<T> source, Func<T, K> keySelector)
            {
                return ToDictionary<T, K, T>(source, keySelector, t => t, EqualityComparer<K>.Default);
            }

            /// <summary>
            /// Enumerates a sequence and creates a Dictionary&lt;<typeparamref name="K">K</typeparamref>,<typeparamref name="T">T</typeparamref>&gt; object out of it, mapping each key value extracted by the <paramref name="keySelector">keySelector</paramref> function on one element from the source sequence. If the produced key value is null or a duplicate key is encountered, a <c>ArgumentNullException</c> is thrown. In the resulting dictionary, key values are compared using the given <paramref name="comparer">comparer</paramref>, or, if a null <paramref name="comparer">comparer</paramref> was specified, using the default equality comparer, EqualityComparer&lt;<typeparamref name="K">K</typeparamref>&gt;.Default.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key used in the resulting dictionary.</typeparam>
            /// <param name="source">Sequence to create a dictionary from.</param>
            /// <param name="keySelector">Function that extracts the key values from the source sequence for usage in the resulting dictionary.</param>
            /// <param name="comparer">Equality comparer to compare the extracted key values in the resulting dictionary.</param>
            /// <returns>A Dictionary&lt;<typeparamref name="K">K</typeparamref>,<typeparamref name="T">T</typeparamref>&gt; collection mapping a key value extracted by the <paramref name="keySelector">keySelector</paramref> function on the source element, for every the source element.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static Dictionary<K, T> ToDictionary<T, K>(this IEnumerable<T> source, Func<T, K> keySelector, IEqualityComparer<K> comparer)
            {
                return ToDictionary<T, K, T>(source, keySelector, t => t, comparer);
            }

            /// <summary>
            /// Enumerates a sequence and creates a Dictionary&lt;<typeparamref name="K">K</typeparamref>,<typeparamref name="E">E</typeparamref>&gt; object out of it, mapping each key value extracted by the <paramref name="keySelector">keySelector</paramref> function on the result of the <paramref name="elementSelector">elementSelector</paramref> function invocation on the source element. If the produced key value is null or a duplicate key is encountered, a <c>ArgumentNullException</c> is thrown. In the resulting dictionary, key values are compared using the default equality comparer, EqualityComparer&lt;<typeparamref name="K">K</typeparamref>&gt;.Default.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key used in the resulting dictionary.</typeparam>
            /// <typeparam name="E">Type of the value stored in the resulting dictionary.</typeparam>
            /// <param name="source">Sequence to create a dictionary from.</param>
            /// <param name="keySelector">Function that extracts the key values from the source sequence for usage in the resulting dictionary.</param>
            /// <param name="elementSelector">Function to generate a result object for each source object.</param>
            /// <returns>A Dictionary&lt;<typeparamref name="K">K</typeparamref>,<typeparamref name="E">E</typeparamref>&gt; collection mapping a key value extracted by the <paramref name="keySelector">keySelector</paramref> function on the result of the <paramref name="elementSelector">elementSelector</paramref> function invocation, for every the source element.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static Dictionary<K, E> ToDictionary<T, K, E>(this IEnumerable<T> source, Func<T, K> keySelector, Func<T, E> elementSelector)
            {
                return ToDictionary<T, K, E>(source, keySelector, elementSelector, EqualityComparer<K>.Default);
            }

            /// <summary>
            /// Enumerates a sequence and creates a Dictionary&lt;<typeparamref name="K">K</typeparamref>,<typeparamref name="E">E</typeparamref>&gt; object out of it, mapping each key value extracted by the <paramref name="keySelector">keySelector</paramref> function on the result of the <paramref name="elementSelector">elementSelector</paramref> function invocation on the source element. If the produced key value is null or a duplicate key is encountered, a <c>ArgumentNullException</c> is thrown. In the resulting dictionary, key values are compared using the given <paramref name="comparer">comparer</paramref>, or, if a null <paramref name="comparer">comparer</paramref> was specified, using the default equality comparer, EqualityComparer&lt;<typeparamref name="K">K</typeparamref>&gt;.Default.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key used in the resulting dictionary.</typeparam>
            /// <typeparam name="E">Type of the value stored in the resulting dictionary.</typeparam>
            /// <param name="source">Sequence to create a dictionary from.</param>
            /// <param name="keySelector">Function that extracts the key values from the source sequence for usage in the resulting dictionary.</param>
            /// <param name="elementSelector">Function to generate a result object for each source object.</param>
            /// <param name="comparer">Equality comparer to compare the extracted key values in the resulting dictionary.</param>
            /// <returns>A Dictionary&lt;<typeparamref name="K">K</typeparamref>,<typeparamref name="E">E</typeparamref>&gt; collection mapping a key value extracted by the <paramref name="keySelector">keySelector</paramref> function on the result of the <paramref name="elementSelector">elementSelector</paramref> function invocation, for every the source element.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static Dictionary<K, E> ToDictionary<T, K, E>(this IEnumerable<T> source, Func<T, K> keySelector, Func<T, E> elementSelector, IEqualityComparer<K> comparer)
            {
                if (source == null || keySelector == null || elementSelector == null) //comparer may be null
                    throw new ArgumentNullException();

                Dictionary<K, E> dictionary = new Dictionary<K, E>(comparer);
                foreach (T item in source)
                {
                    K key = keySelector(item);
                    if (key == null)
                        throw new ArgumentNullException();
                    if (dictionary.ContainsKey(key))
                        throw new ArgumentException();
                    dictionary.Add(key, elementSelector(item));
                }

                return dictionary;
            }

            #endregion

            #region 1.11.5 ToLookup

            /// <summary>
            /// Enumerates a sequence and creates a Lookup&lt;<typeparamref name="K">K</typeparamref>,<typeparamref name="E">E</typeparamref>&gt; object out of it, mapping each key value extracted by the <paramref name="keySelector">keySelector</paramref> function on the corresponding source elements. In the resulting one-to-many dictionary, key values are compared using the default equality comparer, EqualityComparer&lt;<typeparamref name="K">K</typeparamref>&gt;.Default.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key values in the one-to-many dictionary.</typeparam>
            /// <param name="source">Sequence to create a one-to-many dictionary from.</param>
            /// <param name="keySelector">Function that extracts the key values from the source sequence for usage in the resulting one-to-many dictionary.</param>
            /// <returns>A Lookup&lt;<typeparamref name="K">K</typeparamref>,<typeparamref name="E">E</typeparamref>&gt; collection mapping a key value extracted by the <paramref name="keySelector">keySelector</paramref> function on the source elements matching the key.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static Lookup<K, T> ToLookup<T, K>(this IEnumerable<T> source, Func<T, K> keySelector)
            {
                return ToLookup<T, K, T>(source, keySelector, t => t, EqualityComparer<K>.Default);
            }

            /// <summary>
            /// Enumerates a sequence and creates a Lookup&lt;<typeparamref name="K">K</typeparamref>,<typeparamref name="E">E</typeparamref>&gt; object out of it, mapping each key value extracted by the <paramref name="keySelector">keySelector</paramref> function on the corresponding source elements. In the resulting one-to-many dictionary, key values are compared using the given <paramref name="comparer">comparer</paramref>, or, if a null <paramref name="comparer">comparer</paramref> was specified, using the default equality comparer, EqualityComparer&lt;<typeparamref name="K">K</typeparamref>&gt;.Default.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key values in the one-to-many dictionary.</typeparam>
            /// <param name="source">Sequence to create a one-to-many dictionary from.</param>
            /// <param name="keySelector">Function that extracts the key values from the source sequence for usage in the resulting one-to-many dictionary.</param>
            /// <param name="comparer">Equality comparer to compare the extracted key values in the resulting one-to-many dictionary.</param>
            /// <returns>A Lookup&lt;<typeparamref name="K">K</typeparamref>,<typeparamref name="E">E</typeparamref>&gt; collection mapping a key value extracted by the <paramref name="keySelector">keySelector</paramref> function on the source elements matching the key.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static Lookup<K, T> ToLookup<T, K>(this IEnumerable<T> source, Func<T, K> keySelector, IEqualityComparer<K> comparer)
            {
                return ToLookup<T, K, T>(source, keySelector, t => t, comparer);
            }

            /// <summary>
            /// Enumerates a sequence and creates a Lookup&lt;<typeparamref name="K">K</typeparamref>,<typeparamref name="E">E</typeparamref>&gt; object out of it, mapping each key value extracted by the <paramref name="keySelector">keySelector</paramref> function on the results of the <paramref name="elementSelector">elementSelector</paramref> function invocations on the corresponding source elements. In the resulting one-to-many dictionary, key values are compared using the default equality comparer, EqualityComparer&lt;<typeparamref name="K">K</typeparamref>&gt;.Default.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key values in the one-to-many dictionary.</typeparam>
            /// <typeparam name="E">Type of the value stored in the resulting one-to-many dictionary.</typeparam>
            /// <param name="source">Sequence to create a one-to-many dictionary from.</param>
            /// <param name="keySelector">Function that extracts the key values from the source sequence for usage in the resulting one-to-many dictionary.</param>
            /// <param name="elementSelector">Function to generate a result object for each source object.</param>
            /// <returns>A Lookup&lt;<typeparamref name="K">K</typeparamref>,<typeparamref name="E">E</typeparamref>&gt; collection mapping a key value extracted by the <paramref name="keySelector">keySelector</paramref> function on the results of the <paramref name="elementSelector">elementSelector</paramref> function invocations for every source element matching the key.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static Lookup<K, E> ToLookup<T, K, E>(this IEnumerable<T> source, Func<T, K> keySelector, Func<T, E> elementSelector)
            {
                return ToLookup<T, K, E>(source, keySelector, elementSelector, EqualityComparer<K>.Default);
            }

            /// <summary>
            /// Enumerates a sequence and creates a Lookup&lt;<typeparamref name="K">K</typeparamref>,<typeparamref name="E">E</typeparamref>&gt; object out of it, mapping each key value extracted by the <paramref name="keySelector">keySelector</paramref> function on the results of the <paramref name="elementSelector">elementSelector</paramref> function invocations on the corresponding source elements. In the resulting one-to-many dictionary, key values are compared using the given <paramref name="comparer">comparer</paramref>, or, if a null <paramref name="comparer">comparer</paramref> was specified, using the default equality comparer, EqualityComparer&lt;<typeparamref name="K">K</typeparamref>&gt;.Default.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="K">Type of the key values in the one-to-many dictionary.</typeparam>
            /// <typeparam name="E">Type of the value stored in the resulting one-to-many dictionary.</typeparam>
            /// <param name="source">Sequence to create a one-to-many dictionary from.</param>
            /// <param name="keySelector">Function that extracts the key values from the source sequence for usage in the resulting one-to-many dictionary.</param>
            /// <param name="elementSelector">Function to generate a result object for each source object.</param>
            /// <param name="comparer">Equality comparer to compare the extracted key values in the resulting one-to-many dictionary.</param>
            /// <returns>A Lookup&lt;<typeparamref name="K">K</typeparamref>,<typeparamref name="E">E</typeparamref>&gt; collection mapping a key value extracted by the <paramref name="keySelector">keySelector</paramref> function on the results of the <paramref name="elementSelector">elementSelector</paramref> function invocations for every source element matching the key.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static Lookup<K, E> ToLookup<T, K, E>(this IEnumerable<T> source, Func<T, K> keySelector, Func<T, E> elementSelector, IEqualityComparer<K> comparer)
            {
                if (source == null || keySelector == null || elementSelector == null) //comparer may be null
                    throw new ArgumentNullException();

                Lookup<K, E> lookup = new Lookup<K, E>(comparer);
                foreach (T item in source)
                {
                    K key = keySelector(item);
                    Grouping<K, E> group;
                    if (lookup.Contains(key))
                        group = ((Grouping<K, E>)lookup.dictionary[key]);
                    else
                    {
                        group = new Grouping<K, E>(key);
                        lookup.Add(group);
                    }
                    group.Add(elementSelector(item));
                }
                return lookup;
            }

            #endregion

            #region 1.11.6 OfType

            /// <summary>
            /// Filters the elements of a sequence based on a type. When the returned sequence is enumerated, it enumerates the source sequence and yields those elements that are of type <typeparamref name="T">T</typeparamref>. Specifically, each element <c>e</c> for which <c>e is T</c> evaluated to true is yielded by evaluating <c>(T)e</c>.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the result sequence.</typeparam>
            /// <param name="source">Source sequence (non-generic) to filter by the given type <typeparamref name="T">T</typeparamref>.</param>
            /// <returns>Sequence with the elements from the source sequence that are of type <typeparamref name="T">T</typeparamref>.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<T> OfType<T>(this IEnumerable source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                foreach (object e in source)
                    if (e is T)
                        yield return (T)e;
            }

            #endregion

            #region 1.11.7 Cast

            /// <summary>
            /// Casts the elements of a sequence to a given type. When the returned sequence is enumerated, it enumerates the source sequence and yields each element cast to type <typeparamref name="T">T</typeparamref>. An <c>InvalidCastException</c> is thrown if an element in the sequence cannot be cast to type <typeparamref name="T">T</typeparamref>.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Source sequence to cast elements to type <typeparamref name="T">T</typeparamref>.</param>
            /// <returns>Sequence with the elements of the source sequence casted to type <typeparamref name="T">T</typeparamref>.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<T> Cast<T>(this IEnumerable source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                foreach (object e in source)
                    yield return (T)e;
            }

            #endregion

            #endregion

            #region 1.12 Equality operator

            #region 1.12.1 EqualAll

            /// <summary>
            /// Checks whether two sequences are equal by enumerating the two source sequences in parallel and comparing corresponding elements using the <c>Equals</c> static method in <c>System.Object</c>. The method returns true if all corresponding elements compare equal and the two sequences are of equal length. Otherwise, the method returns false. An <c>ArgumentNullException</c> is thrown if either argument is null.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequences.</typeparam>
            /// <param name="first">The first sequence to compare for equality.</param>
            /// <param name="second">The second sequence to compare for equality.</param>
            /// <returns>True if both sequences are of the same length and all corresponding elements of both sequences are equal, false otherwise.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static bool EqualAll<T>(this IEnumerable<T> first, IEnumerable<T> second)
            {
                if (first == null || second == null)
                    throw new ArgumentNullException();

                if (first.Count() != second.Count())
                    return false;

                for (IEnumerator<T> e1 = first.GetEnumerator(), e2 = second.GetEnumerator(); e1.MoveNext() && e2.MoveNext(); )
                    if (!System.Object.Equals(e1.Current, e2.Current))
                        return false;
                return true;
            }

            #endregion

            #endregion

            #region 1.13 Element operators

            #region 1.13.1 First

            /// <summary>
            /// Returns the first element of a sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to get the first element from.</param>
            /// <returns>The first element from the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. Use <see cref="FirstOrDefault&lt;T&gt;(IEnumerable&lt;T&gt;)">FirstOrDefault</see> to return <c>default(<typeparamref name="T">T</typeparamref>)</c> in case the sequence is empty.</remarks>
            public static T First<T>(this IEnumerable<T> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();
                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();
                else
                    return enumerator.Current;
            }

            /// <summary>
            /// Returns the first element of a sequence for which the <paramref name="predicate">predicate</paramref> function returns true. An <c>InvalidOperationException</c> is thrown if no element matches the <paramref name="predicate">predicate</paramref> or if the source sequence is empty.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to get the first element from that matches the <paramref name="predicate">predicate</paramref> condition.</param>
            /// <param name="predicate">Predicate function to check elements of the source sequence to meet certain criteria. The first element of a sequence for which the <paramref name="predicate">predicate</paramref> function returns true will be returned.</param>
            /// <returns>The first element from the source sequence, matching the <paramref name="predicate">predicate</paramref>. An <c>InvalidOperationException</c> is thrown if no element matches the <paramref name="predicate">predicate</paramref> or if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. Use <see cref="FirstOrDefault&lt;T&gt;(IEnumerable&lt;T&gt;, Func&lt;T, bool&gt;)">FirstOrDefault</see> to return <c>default(<typeparamref name="T">T</typeparamref>)</c> in case the sequence is empty or no matching element is found.</remarks>
            public static T First<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            {
                if (source == null || predicate == null)
                    throw new ArgumentNullException();

                foreach (T item in source)
                    if (predicate(item))
                        return item;

                throw new InvalidOperationException();
            }

            #endregion

            #region 1.13.2 FirstOrDefault

            /// <summary>
            /// Returns the first element of a sequence. If the source sequence is empty, <c>default(<typeparamref name="T">T</typeparamref>)</c> is returned.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to get the first element from.</param>
            /// <returns>The first element from the source sequence. If the source sequence is empty, <c>default(<typeparamref name="T">T</typeparamref>)</c> is returned.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. Use <see cref="First&lt;T&gt;(IEnumerable&lt;T&gt;)">First</see> to throw an <c>ArgumentNullException</c> in case the sequence is empty.</remarks>
            public static T FirstOrDefault<T>(this IEnumerable<T> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();
                if (!enumerator.MoveNext())
                    return default(T);
                else
                    return enumerator.Current;
            }

            /// <summary>
            /// Returns the first element of a sequence for which the <paramref name="predicate">predicate</paramref> function returns true. If no element matches the <paramref name="predicate">predicate</paramref> or if the source sequence is empty, <c>default(<typeparamref name="T">T</typeparamref>)</c> is returned.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to get the first element from that matches the <paramref name="predicate">predicate</paramref> condition.</param>
            /// <param name="predicate">Predicate function to check elements of the source sequence to meet certain criteria. The first element of a sequence for which the <paramref name="predicate">predicate</paramref> function returns true will be returned.</param>
            /// <returns>The first element from the source sequence, matching the <paramref name="predicate">predicate</paramref>. If no element matches the <paramref name="predicate">predicate</paramref> or if the source sequence is empty, <c>default(<typeparamref name="T">T</typeparamref>)</c> is returned.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. Use <see cref="First&lt;T&gt;(IEnumerable&lt;T&gt;, Func&lt;T, bool&gt;)">First</see> to throw an <c>ArgumentNullException</c> in case the sequence is empty or no matching element is found.</remarks>
            public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            {
                if (source == null || predicate == null)
                    throw new ArgumentNullException();

                foreach (T item in source)
                    if (predicate(item))
                        return item;

                return default(T);
            }

            #endregion

            #region 1.13.3 Last

            /// <summary>
            /// Returns the last element of a sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to get the last element from.</param>
            /// <returns>The last element from the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. Use <see cref="LastOrDefault&lt;T&gt;(IEnumerable&lt;T&gt;)">LastOrDefault</see> to return <c>default(<typeparamref name="T">T</typeparamref>)</c> in case the sequence is empty.</remarks>
            public static T Last<T>(this IEnumerable<T> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();
                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();
                else
                {
                    T last = enumerator.Current;
                    while (enumerator.MoveNext())
                        last = enumerator.Current;
                    return last;
                }
            }

            /// <summary>
            /// Returns the last element of a sequence for which the <paramref name="predicate">predicate</paramref> function returns true. An <c>InvalidOperationException</c> is thrown if no element matches the <paramref name="predicate">predicate</paramref> or if the source sequence is empty.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to get the last element from that matches the <paramref name="predicate">predicate</paramref> condition.</param>
            /// <param name="predicate">Predicate function to check elements of the source sequence to meet certain criteria. The last element of a sequence for which the <paramref name="predicate">predicate</paramref> function returns true will be returned.</param>
            /// <returns>The last element from the source sequence, matching the <paramref name="predicate">predicate</paramref>. An <c>InvalidOperationException</c> is thrown if no element matches the <paramref name="predicate">predicate</paramref> or if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. Use <see cref="LastOrDefault&lt;T&gt;(IEnumerable&lt;T&gt;, Func&lt;T, bool&gt;)">LastOrDefault</see> to return <c>default(<typeparamref name="T">T</typeparamref>)</c> in case the sequence is empty or no matching element is found.</remarks>
            public static T Last<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            {
                if (source == null || predicate == null)
                    throw new ArgumentNullException();

                T last = default(T);
                bool found = false;
                foreach (T item in source)
                {
                    if (predicate(item))
                    {
                        found = true;
                        last = item;
                    }
                }

                if (!found)
                    throw new InvalidOperationException();
                else
                    return last;
            }

            #endregion

            #region 1.13.4 LastOrDefault

            /// <summary>
            /// Returns the last element of a sequence. If the source sequence is empty, <c>default(<typeparamref name="T">T</typeparamref>)</c> is returned.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to get the last element from.</param>
            /// <returns>The last element from the source sequence. If the source sequence is empty, <c>default(<typeparamref name="T">T</typeparamref>)</c> is returned.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. Use <see cref="Last&lt;T&gt;(IEnumerable&lt;T&gt;)">Last</see> to throw an <c>ArgumentNullException</c> in case the sequence is empty.</remarks>
            public static T LastOrDefault<T>(this IEnumerable<T> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();
                if (!enumerator.MoveNext())
                    return default(T);
                else
                {
                    T last = enumerator.Current;
                    while (enumerator.MoveNext())
                        last = enumerator.Current;
                    return last;
                }
            }

            /// <summary>
            /// Returns the last element of a sequence for which the <paramref name="predicate">predicate</paramref> function returns true. If no element matches the <paramref name="predicate">predicate</paramref> or if the source sequence is empty, <c>default(<typeparamref name="T">T</typeparamref>)</c> is returned.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to get the last element from that matches the <paramref name="predicate">predicate</paramref> condition.</param>
            /// <param name="predicate">Predicate function to check elements of the source sequence to meet certain criteria. The last element of a sequence for which the <paramref name="predicate">predicate</paramref> function returns true will be returned.</param>
            /// <returns>The last element from the source sequence, matching the <paramref name="predicate">predicate</paramref>. If no element matches the <paramref name="predicate">predicate</paramref> or if the source sequence is empty, <c>default(<typeparamref name="T">T</typeparamref>)</c> is returned.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. Use <see cref="Last&lt;T&gt;(IEnumerable&lt;T&gt;)">Last</see> to throw an <c>ArgumentNullException</c> in case the sequence is empty or no matching element is found.</remarks>
            public static T LastOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            {
                if (source == null || predicate == null)
                    throw new ArgumentNullException();

                T last = default(T);
                bool found = false;
                foreach (T item in source)
                {
                    if (predicate(item))
                    {
                        found = true;
                        last = item;
                    }
                }

                if (!found)
                    return default(T);
                else
                    return last;
            }

            #endregion

            #region 1.13.5 Single

            /// <summary>
            /// Returns the single element of a sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty or if the source sequence contains more than one element.
            /// </summary>
            /// <typeparam name="T">Type of the element in the source sequence.</typeparam>
            /// <param name="source">Sequence to get the single element from.</param>
            /// <returns>The single element from the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty or if the source sequence contains more than one element.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. Use <see cref="SingleOrDefault&lt;T&gt;(IEnumerable&lt;T&gt;)">SingleOrDefault</see> to return <c>default(<typeparamref name="T">T</typeparamref>)</c> in case the sequence is empty.</remarks>
            public static T Single<T>(this IEnumerable<T> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();
                if (enumerator.MoveNext())
                {
                    T single = enumerator.Current;
                    if (!enumerator.MoveNext()) //only one
                        return single;
                    else //more than one
                        throw new InvalidOperationException();
                }
                else //empty
                    throw new InvalidOperationException();
            }

            /// <summary>
            /// Returns the single element of a sequence for which the <paramref name="predicate">predicate</paramref> function returns true. An <c>InvalidOperationException</c> is thrown if no element matches the <paramref name="predicate">predicate</paramref>, if more than one element matching the <paramref name="predicate">predicate</paramref> is found, or if the source sequence is empty.
            /// </summary>
            /// <typeparam name="T">Type of the element in the source sequence.</typeparam>
            /// <param name="source">Sequence to get the single element from that matches the <paramref name="predicate">predicate</paramref> condition.</param>
            /// <param name="predicate">Predicate function to check elements of the source sequence to meet certain criteria. The single element of a sequence for which the <paramref name="predicate">predicate</paramref> function returns true will be returned.</param>
            /// <returns>The single element from the source sequence, matching the <paramref name="predicate">predicate</paramref>. An <c>InvalidOperationException</c> is thrown if no element matches the <paramref name="predicate">predicate</paramref>, if more than one element matching the <paramref name="predicate">predicate</paramref> is found, or if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. Use <see cref="SingleOrDefault&lt;T&gt;(IEnumerable&lt;T&gt;, Func&lt;T, bool&gt;)">SingleOrDefault</see> to return <c>default(<typeparamref name="T">T</typeparamref>)</c> in case the sequence is empty or no matching element is found.</remarks>
            public static T Single<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            {
                if (source == null || predicate == null)
                    throw new ArgumentNullException();

                T result = default(T);
                bool found = false;
                foreach (T item in source)
                {
                    if (predicate(item))
                    {
                        if (found == true) //more than one
                            throw new InvalidOperationException();

                        found = true;
                        result = item;
                    }
                }

                if (found)
                    return result;
                else //no match
                    throw new InvalidOperationException();
            }

            #endregion

            #region 1.13.6 SingleOrDefault

            /// <summary>
            /// Returns the single element of a sequence. If the source sequence is empty, <c>default(<typeparamref name="T">T</typeparamref>)</c> is returned. An <c>InvalidOperationException</c> is thrown if the source sequence contains more than one element.
            /// </summary>
            /// <typeparam name="T">Type of the element in the source sequence.</typeparam>
            /// <param name="source">Sequence to get the single element from.</param>
            /// <returns>The single element from the source sequence. If the source sequence is empty, <c>default(<typeparamref name="T">T</typeparamref>)</c> is returned. An <c>InvalidOperationException</c> is thrown if the source sequence contains more than one element.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. Use <see cref="Single&lt;T&gt;(IEnumerable&lt;T&gt;)">Single</see> to throw an <c>ArgumentNullException</c> in case the sequence is empty.</remarks>
            public static T SingleOrDefault<T>(this IEnumerable<T> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();
                if (enumerator.MoveNext())
                {
                    T single = enumerator.Current;
                    if (!enumerator.MoveNext()) //only one
                        return single;
                    else //more than one
                        throw new InvalidOperationException();
                }
                else //empty
                    return default(T);
            }

            /// <summary>
            /// Returns the single element of a sequence for which the <paramref name="predicate">predicate</paramref> function returns true. If no element matches the <paramref name="predicate">predicate</paramref> or if the source sequence is empty, <c>default(<typeparamref name="T">T</typeparamref>)</c> is returned. An <c>InvalidOperationException</c> is thrown if more than one element matching the <paramref name="predicate">predicate</paramref> is found.
            /// </summary>
            /// <typeparam name="T">Type of the element in the source sequence.</typeparam>
            /// <param name="source">Sequence to get the single element from that matches the <paramref name="predicate">predicate</paramref> condition.</param>
            /// <param name="predicate">Predicate function to check elements of the source sequence to meet certain criteria. The single element of a sequence for which the <paramref name="predicate">predicate</paramref> function returns true will be returned.</param>
            /// <returns>The single element from the source sequence, matching the <paramref name="predicate">predicate</paramref>. If no element matches the <paramref name="predicate">predicate</paramref> or if the source sequence is empty, <c>default(<typeparamref name="T">T</typeparamref>)</c> is returned. An <c>InvalidOperationException</c> is thrown if more than one element matching the <paramref name="predicate">predicate</paramref> is found.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. Use <see cref="Single&lt;T&gt;(IEnumerable&lt;T&gt;, Func&lt;T, bool&gt;)">Single</see> to throw an <c>ArgumentNullException</c> in case the sequence is empty or no matching element is found.</remarks>
            public static T SingleOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            {
                if (source == null || predicate == null)
                    throw new ArgumentNullException();

                T result = default(T);
                bool found = false;
                foreach (T item in source)
                {
                    if (predicate(item))
                    {
                        if (found == true) //more than one
                            throw new InvalidOperationException();

                        found = true;
                        result = item;
                    }
                }

                if (found)
                    return result;
                else //no match
                    return default(T);
            }

            #endregion

            #region 1.13.7 ElementAt

            /// <summary cref="ElementAt">
            /// Return the element at a given index in a sequence. The operator first checks whether the source sequence implements IList&lt;<typeparamref name="T">T</typeparamref>&gt;. If so, the source sequence’s implementation of IList&lt;<typeparamref name="T">T</typeparamref>&gt; is used to obtain the element at the given index. Otherwise, the source sequence is enumerated until index elements have been skipped, and the element found at that position in the sequence is returned. An <c>ArgumentOutOfRangeException</c> is thrown if the index is less than zero or greater than or equal to the number of elements in the sequence.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to get the element on the specified position from.</param>
            /// <param name="index">Index of the element to retrieve from the sequence.</param>
            /// <returns>The element on the position indicated by <paramref name="index">index</paramref> in the sequence. An <c>ArgumentOutOfRangeException</c> is thrown if the index is less than zero or greater than or equal to the number of elements in the sequence.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. Use <see cref="ElementAtOrDefault">ElementAtOrDefault</see> to return <c>default(<typeparamref name="T">T</typeparamref>)</c> in case the <paramref name="index">index</paramref> is less than zero or greater than or equal to the number of elements in the sequence.</remarks>
            public static T ElementAt<T>(this IEnumerable<T> source, int index)
            {
                if (source == null)
                    throw new ArgumentNullException();

                if (index < 0)
                    throw new ArgumentOutOfRangeException();

                if (source is IList<T>)
                    return ((IList<T>)source)[index];

                int i = 0;
                foreach (T item in source)
                    if (i++ == index)
                        return item;

                throw new ArgumentOutOfRangeException();
            }

            #endregion

            #region 1.13.8 ElementAtOrDefault

            /// <summary cref="ElementAtOrDefault">
            /// Return the element at a given index in a sequence. The operator first checks whether the source sequence implements IList&lt;<typeparamref name="T">T</typeparamref>&gt;. If so, the source sequence’s implementation of IList&lt;<typeparamref name="T">T</typeparamref>&gt; is used to obtain the element at the given index. Otherwise, the source sequence is enumerated until index elements have been skipped, and the element found at that position in the sequence is returned. If the index is less than zero or greater than or equal to the number of elements in the sequence, <c>default(<typeparamref name="T">T</typeparamref>)</c> is returned.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to get the element on the specified position from.</param>
            /// <param name="index">Index of the element to retrieve from the sequence.</param>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;. Use <see cref="ElementAt">ElementAt</see> to throw an <c>ArgumentOutOfRangeException</c> in case the <paramref name="index">index</paramref> is less than zero or greater than or equal to the number of elements in the sequence.</remarks>
            public static T ElementAtOrDefault<T>(this IEnumerable<T> source, int index)
            {
                if (source == null)
                    throw new ArgumentNullException();

                if (index < 0)
                    return default(T);

                IList<T> lst = source as IList<T>;
                if (lst != null)
                {
                    if (index >= lst.Count)
                        return default(T);
                    else
                        return lst[index];
                }

                int i = 0;
                foreach (T item in source)
                    if (i++ == index)
                        return item;

                return default(T);
            }

            #endregion

            #region 1.13.9 DefaultIfEmpty

            /// <summary>
            /// Supplies a default element for an empty sequence. When the returned object is enumerated, it enumerates the source sequence and yields its elements. If the source sequence is empty, <c>default(<typeparamref name="T">T</typeparamref>)</c> is yielded in place of an empty sequence.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to be yielded if not empty. Otherwise <c>default(<typeparamref name="T">T</typeparamref>)</c> will be yielded.</param>
            /// <returns>A sequence identical to the source sequence in case it's not empty, otherwise a singleton sequence yielding <c>default(<typeparamref name="T">T</typeparamref>)</c>.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<T> DefaultIfEmpty<T>(this IEnumerable<T> source)
            {
                return DefaultIfEmpty<T>(source, default(T));
            }

            /// <summary>
            /// Supplies a default element for an empty sequence. When the returned object is enumerated, it enumerates the source sequence and yields its elements. If the source sequence is empty, the <paramref name="defaultValue">defaultValue</paramref> is yielded in place of an empty sequence.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to be yielded if not empty. Otherwise <paramref name="defaultValue">defaultValue</paramref> will be yielded.</param>
            /// <param name="defaultValue">The value to be yielded if the source sequence is empty.</param>
            /// <returns>A sequence identical to the source sequence in case it's not empty, otherwise a singleton sequence yielding <paramref name="defaultValue">defaultValue</paramref>.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static IEnumerable<T> DefaultIfEmpty<T>(this IEnumerable<T> source, T defaultValue)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();
                if (!enumerator.MoveNext())
                    yield return defaultValue;
                else
                    do
                    {
                        yield return enumerator.Current;
                    } while (enumerator.MoveNext());
            }

            #endregion

            #endregion

            #region 1.14 Generation operators

            #region 1.14.1 Range

            /// <summary>
            /// Generates a sequence of integral numbers. An <c>ArgumentOutOfRangeException</c> is thrown if <c><paramref name="count">count</paramref></c> is less than zero or if <c><paramref name="start">start</paramref> + <paramref name="count">count</paramref> - 1</c> is larger than <c>int.MaxValue</c>. When the returned object is enumerated, it yields <paramref name="count">count</paramref> sequential integers starting with the value <paramref name="start">start</paramref>.
            /// </summary>
            /// <param name="start">First integer to be yielded.</param>
            /// <param name="count">Number of integers to be yielded.</param>
            /// <returns>A sequence with <paramref name="count">count</paramref> sequential integers starting with the value <paramref name="start">start</paramref>.</returns>
            public static IEnumerable<int> Range(int start, int count)
            {
                if (count < 0 || (long) start + count - 1 > int.MaxValue)
                    throw new ArgumentOutOfRangeException();

                for (int i = start; i <= start + count - 1; i++)
                    yield return i;
            }

            #endregion

            #region 1.14.2 Repeat

            /// <summary>
            /// Generates a sequence by repeating a <paramref name="element">value</paramref> a <paramref name="count">number</paramref> of times.
            /// </summary>
            /// <typeparam name="T">Type of the <paramref name="element">element</paramref> to be repeated in the result sequence.</typeparam>
            /// <param name="element">Element to be repeated <paramref name="count">count</paramref> times in the result sequence.</param>
            /// <param name="count">Number of times to repeat the <paramref name="element">element</paramref>.</param>
            /// <returns>A sequence with the <paramref name="element">element</paramref> repeated <paramref name="count">count</paramref> times.</returns>
            public static IEnumerable<T> Repeat<T>(T element, int count)
            {
                if (count < 0)
                    throw new ArgumentOutOfRangeException();

                for (int i = 0; i < count; i++)
                    yield return element;
            }

            #endregion

            #region 1.14.3 Empty

            /// <summary>
            /// Returns an empty sequence of a given type <typeparamref name="T">T</typeparamref>. When the returned object is enumerated, nothing is yielded.
            /// </summary>
            /// <typeparam name="T">Type of the empty sequence to be yielded.</typeparam>
            /// <returns>An empty sequence of the given type <typeparamref name="T">T</typeparamref>.</returns>
            public static IEnumerable<T> Empty<T>()
            {
                yield break;
            }

            #endregion

            #endregion

            #region 1.15 Quantifiers

            #region 1.15.1 Any

            /// <summary>
            /// Checks whether a sequence contains any elements.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to check.</param>
            /// <returns>True if the sequence contains any elements, false otherwise.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static bool Any<T>(this IEnumerable<T> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                return source.GetEnumerator().MoveNext();
            }

            /// <summary>
            /// Checks whether any element of a sequence satisfies a <paramref name="predicate">condition</paramref>.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to check for any elements matching the <paramref name="predicate">condition</paramref>.</param>
            /// <param name="predicate">Predicate used to check every element whether it matches the underlying condition.</param>
            /// <returns>True if the sequence contains any elements matching the <paramref name="predicate">predicate</paramref>, false otherwise.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static bool Any<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            {
                if (source == null || predicate == null)
                    throw new ArgumentNullException();

                foreach (T item in source)
                    if (predicate(item))
                        return true;

                return false;
            }

            #endregion

            #region 1.15.2 All

            /// <summary>
            /// Checks whether all elements of a sequence satisfy a <paramref name="predicate">condition</paramref>.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to check whether all elements match the <paramref name="predicate">condition</paramref>.</param>
            /// <param name="predicate">Predicate used to check every element whether it matches the underlying condition.</param>
            /// <returns>True if all elements in the sequence match the <paramref name="predicate">predicate</paramref>, false otherwise.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static bool All<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            {
                if (source == null || predicate == null)
                    throw new ArgumentNullException();

                foreach (T item in source)
                    if (!predicate(item))
                        return false;

                return true;
            }

            #endregion

            #region 1.15.3 Contains

            /// <summary>
            /// Checks whether a sequence contains a <paramref name="value">value</paramref>.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to be searched for the <paramref name="value">value</paramref>.</param>
            /// <param name="value">Value to be searched for in the sequence.</param>
            /// <returns>True if the <paramref name="value">value</paramref> was found, false otherwise.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static bool Contains<T>(this IEnumerable<T> source, T value)
            {
                if (source == null)
                    throw new ArgumentNullException();

                ICollection<T> collection = source as ICollection<T>;
                if (collection != null)
                    return collection.Contains(value);

                foreach (T item in source)
                    if (EqualityComparer<T>.Default.Equals(item, value))
                        return true;

                return false;
            }

            #endregion

            #endregion

            #region 1.16 Aggregate operators

            #region 1.16.1 Count

            /// <summary>
            /// Counts the number of elements in a sequence. If the source sequence implements ICollection&lt;<typeparamref name="T">T</typeparamref>&gt;, the sequence's implementation of ICollection&lt;<typeparamref name="T">T</typeparamref>&gt; is used to obtain the element count. Otherwise, the source sequence is enumerated to count the number of elements. An <c>OverflowException</c> is thrown is the count exceeds <c>int.MaxValue</c>.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to count the elements of.</param>
            /// <returns>The number of elements in the sequence. An <c>OverflowException</c> is thrown is the count exceeds <c>int.MaxValue</c>.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static int Count<T>(this IEnumerable<T> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                ICollection<T> collection = source as ICollection<T>;
                if (collection != null)
                    return collection.Count;

                checked
                {
                    int i = 0;
                    foreach (T item in source)
                        i++;

                    return i;
                }
            }

            /// <summary>
            /// Counts the number of elements in a sequence that match a <paramref name="predicate">predicate</paramref>. An <c>OverflowException</c> is thrown is the count exceeds <c>int.MaxValue</c>.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to count the elements of.</param>
            /// <param name="predicate">Predicate to evaluate for each element in the source sequence. If the evaluation returns true, the element is included in the count.</param>
            /// <returns>The number of elements in the sequence that match the <paramref name="predicate">predicate</paramref>. An <c>OverflowException</c> is thrown is the count exceeds <c>int.MaxValue</c>.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static int Count<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            {
                if (source == null || predicate == null)
                    throw new ArgumentNullException();

                int i = 0;
                foreach (T item in source)
                    if (predicate(item))
                        i++;

                return i;
            }

            #endregion

            #region 1.16.2 LongCount

            /// <summary>
            /// Counts the number of elements in a sequence by enumerating it.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to count the elements of.</param>
            /// <returns>The number of elements in the sequence.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static long LongCount<T>(this IEnumerable<T> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                long i = 0;
                foreach (T item in source)
                    i++;

                return i;
            }

            /// <summary>
            /// Counts the number of elements in a sequence that match a <paramref name="predicate">predicate</paramref>.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to count the elements of.</param>
            /// <param name="predicate">Predicate to evaluate for each element in the source sequence. If the evaluation returns true, the element is included in the count.</param>
            /// <returns>The number of elements in the sequence that match the <paramref name="predicate">predicate</paramref>.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static long LongCount<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            {
                if (source == null || predicate == null)
                    throw new ArgumentNullException();

                long i = 0;
                foreach (T item in source)
                    if (predicate(item))
                        i++;

                return i;
            }

            #endregion

            #region 1.16.3 Sum

            #region int

            /// <summary>
            /// Computes the integer sum of a sequence of integer values. If the sum is too large to represent as an <c>int</c>, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <param name="source">Sequence to compute the sum for.</param>
            /// <returns>The integer sum of the sequence, 0 if the sequence is empty. If the sum is too large to represent as an <c>int</c>, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static int Sum(this IEnumerable<int> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                checked
                {
                    int sum = 0;
                    foreach (int i in source)
                        sum += i;

                    return sum;
                }
            }

            /// <summary>
            /// Computes the integer sum of the <paramref name="selector">selector</paramref> function evaluations for each element in the source sequence. If the sum is too large to represent as an <c>int</c>, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the sum operation on.</param>
            /// <param name="selector">Function returning an integer value for each element of the source sequence.</param>
            /// <returns>The integer sum of the <paramref name="selector">selector</paramref> function evaluations for each element in the source sequence, 0 if the sequence is empty. If the sum is too large to represent as an <c>int</c>, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static int Sum<T>(this IEnumerable<T> source, Func<T, int> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                checked
                {
                    int sum = 0;
                    foreach (T item in source)
                        sum += selector(item);

                    return sum;
                }
            }

            #endregion

            #region int?

            /// <summary>
            /// Computes the integer sum of a sequence of integer values excluding null values. If the sum is too large to represent as an <c>int</c>, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <param name="source">Sequence to compute the sum for.</param>
            /// <returns>The integer sum of the sequence excluding null values, 0 if the sequence is empty. If the sum is too large to represent as an <c>int</c>, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static int? Sum(this IEnumerable<int?> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                checked
                {
                    int sum = 0;
                    foreach (int? i in source)
                        if (i != null)
                            sum += i.Value;

                    return sum;
                }
            }

            /// <summary>
            /// Computes the integer sum of the non-null <paramref name="selector">selector</paramref> function evaluations for each element in the source sequence. If the sum is too large to represent as an <c>int</c>, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the sum operation on.</param>
            /// <param name="selector">Function returning an integer value for each element of the source sequence.</param>
            /// <returns>The integer sum of the non-null <paramref name="selector">selector</paramref> function evaluations for each element in the source sequence, 0 if the sequence is empty. If the sum is too large to represent as an <c>int</c>, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static int? Sum<T>(this IEnumerable<T> source, Func<T, int?> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                checked
                {
                    int sum = 0;
                    foreach (T item in source)
                    {
                        int? i = selector(item);
                        if (i != null)
                            sum += i.Value;
                    }

                    return sum;
                }
            }

            #endregion

            #region long

            /// <summary>
            /// Computes the long sum of a sequence of long values. If the sum is too large to represent as a <c>long</c>, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <param name="source">Sequence to compute the sum for.</param>
            /// <returns>The long sum of the sequence, 0 if the sequence is empty. If the sum is too large to represent as a <c>long</c>, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static long Sum(this IEnumerable<long> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                checked
                {
                    long sum = 0;
                    foreach (long i in source)
                        sum += i;

                    return sum;
                }
            }

            /// <summary>
            /// Computes the long sum of the <paramref name="selector">selector</paramref> function evaluations for each element in the source sequence. If the sum is too large to represent as a <c>long</c>, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the sum operation on.</param>
            /// <param name="selector">Function returning a long value for each element of the source sequence.</param>
            /// <returns>The long sum of the <paramref name="selector">selector</paramref> function evaluations for each element in the source sequence, 0 if the sequence is empty. If the sum is too large to represent as a <c>long</c>, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static long Sum<T>(this IEnumerable<T> source, Func<T, long> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                checked
                {
                    long sum = 0;
                    foreach (T item in source)
                        sum += selector(item);

                    return sum;
                }
            }

            #endregion

            #region long?

            /// <summary>
            /// Computes the long sum of a sequence of long values excluding null values. If the sum is too large to represent as a <c>long</c>, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <param name="source">Sequence to compute the sum for.</param>
            /// <returns>The long sum of the sequence excluding null values, 0 if the sequence is empty. If the sum is too large to represent as a <c>long</c>, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static long? Sum(this IEnumerable<long?> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                checked
                {
                    long sum = 0;
                    foreach (long? i in source)
                        if (i != null)
                            sum += i.Value;

                    return sum;
                }
            }

            /// <summary>
            /// Computes the long sum of the non-null <paramref name="selector">selector</paramref> function evaluations for each element in the source sequence. If the sum is too large to represent as a <c>long</c>, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the sum operation on.</param>
            /// <param name="selector">Function returning a long value for each element of the source sequence.</param>
            /// <returns>The long sum of the non-null <paramref name="selector">selector</paramref> function evaluations for each element in the source sequence, 0 if the sequence is empty. If the sum is too large to represent as a <c>long</c>, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static long? Sum<T>(this IEnumerable<T> source, Func<T, long?> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                checked
                {
                    long sum = 0;
                    foreach (T item in source)
                    {
                        long? i = selector(item);
                        if (i != null)
                            sum += i.Value;
                    }

                    return sum;
                }
            }

            #endregion

            #region double

            /// <summary>
            /// Computes the double sum of a sequence of double values. If the sum is too large to represent as a <c>double</c>, a positive or negative infinity is returned.
            /// </summary>
            /// <param name="source">Sequence to compute the sum for.</param>
            /// <returns>The double sum of the sequence, 0.0 if the sequence is empty. If the sum is too large to represent as a <c>double</c>, a positive or negative infinity is returned.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double Sum(this IEnumerable<double> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                double sum = 0.0;
                foreach (double i in source)
                    sum += i;

                return sum;
            }

            /// <summary>
            /// Computes the double sum of the <paramref name="selector">selector</paramref> function evaluations for each element in the source sequence. If the sum is too large to represent as a <c>double</c>, a positive or negative infinity is returned.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the sum operation on.</param>
            /// <param name="selector">Function returning an integer value for each element of the source sequence.</param>
            /// <returns>The double sum of the <paramref name="selector">selector</paramref> function evaluations for each element in the source sequence, 0.0 if the sequence is empty. If the sum is too large to represent as a <c>double</c>, a positive or negative infinity is returned.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double Sum<T>(this IEnumerable<T> source, Func<T, double> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                double sum = 0.0;
                foreach (T item in source)
                    sum += selector(item);

                return sum;
            }

            #endregion

            #region double?

            /// <summary>
            /// Computes the double sum of a sequence of double values excluding null values. If the sum is too large to represent as a <c>double</c>, a positive or negative infinity is returned.
            /// </summary>
            /// <param name="source">Sequence to compute the sum for.</param>
            /// <returns>The double sum of the sequence excluding null values, 0.0 if the sequence is empty. If the sum is too large to represent as a <c>double</c>, a positive or negative infinity is returned.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double? Sum(this IEnumerable<double?> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                double sum = 0.0;
                foreach (double? i in source)
                    if (i != null)
                        sum += i.Value;

                return sum;
            }

            /// <summary>
            /// Computes the double sum of the non-null <paramref name="selector">selector</paramref> function evaluations for each element in the source sequence. If the sum is too large to represent as a <c>double</c>, a positive or negative infinity is returned.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the sum operation on.</param>
            /// <param name="selector">Function returning an integer value for each element of the source sequence.</param>
            /// <returns>The double sum of the non-null <paramref name="selector">selector</paramref> function evaluations for each element in the source sequence, 0.0 if the sequence is empty. If the sum is too large to represent as a <c>double</c>, a positive or negative infinity is returned.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double? Sum<T>(this IEnumerable<T> source, Func<T, double?> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                double sum = 0.0;
                foreach (T item in source)
                {
                    double? i = selector(item);
                    if (i != null)
                        sum += i.Value;
                }

                return sum;
            }

            #endregion

            #region decimal

            /// <summary>
            /// Computes the decimal sum of a sequence of decimal values. If the sum is too large to represent as a <c>decimal</c>, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <param name="source">Sequence to compute the sum for.</param>
            /// <returns>The decimal sum of the sequence, 0 if the sequence is empty. If the sum is too large to represent as a <c>decimal</c>, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static decimal Sum(this IEnumerable<decimal> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                checked
                {
                    decimal sum = 0;
                    foreach (decimal i in source)
                        sum += i;

                    return sum;
                }
            }

            /// <summary>
            /// Computes the decimal sum of the <paramref name="selector">selector</paramref> function evaluations for each element in the source sequence. If the sum is too large to represent as a <c>decimal</c>, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the sum operation on.</param>
            /// <param name="selector">Function returning an decimal value for each element of the source sequence.</param>
            /// <returns>The decimal sum of the <paramref name="selector">selector</paramref> function evaluations for each element in the source sequence, 0 if the sequence is empty. If the sum is too large to represent as a <c>decimal</c>, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static decimal Sum<T>(this IEnumerable<T> source, Func<T, decimal> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                checked
                {
                    decimal sum = 0;
                    foreach (T item in source)
                        sum += selector(item);

                    return sum;
                }
            }

            #endregion

            #region decimal?

            /// <summary>
            /// Computes the decimal sum of a sequence of decimal values excluding null values. If the sum is too large to represent as a <c>decimal</c>, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <param name="source">Sequence to compute the sum for.</param>
            /// <returns>The decimal sum of the sequence excluding null values, 0 if the sequence is empty. If the sum is too large to represent as a <c>decimal</c>, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static decimal? Sum(this IEnumerable<decimal?> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                checked
                {
                    decimal sum = 0;
                    foreach (decimal? i in source)
                        if (i != null)
                            sum += i.Value;

                    return sum;
                }
            }

            /// <summary>
            /// Computes the decimal sum of the non-null <paramref name="selector">selector</paramref> function evaluations for each element in the source sequence. If the sum is too large to represent as a <c>decimal</c>, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the sum operation on.</param>
            /// <param name="selector">Function returning an decimal value for each element of the source sequence.</param>
            /// <returns>The decimal sum of the non-null <paramref name="selector">selector</paramref> function evaluations for each element in the source sequence, 0 if the sequence is empty. If the sum is too large to represent as a <c>decimal</c>, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static decimal? Sum<T>(this IEnumerable<T> source, Func<T, decimal?> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                checked
                {
                    decimal sum = 0;
                    foreach (T item in source)
                    {
                        decimal? i = selector(item);
                        if (i != null)
                            sum += i.Value;
                    }

                    return sum;
                }
            }

            #endregion

            #endregion

            #region 1.16.4 Min

            #region general

            /// <summary>
            /// Finds the minimum of a sequence of values by enumerating the sequence and comparing the values using their IComparable&lt;<typeparamref name="T">T</typeparamref>&gt; implementation, or, if the values do not implement that interface, the non-generic IComparable interface.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to compute the minimum for.</param>
            /// <returns>The minimum value encountered while enumerating the source sequence.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static T Min<T>(this IEnumerable<T> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                T min = enumerator.Current;

                if (min is IComparable<T>)
                {
                    IComparable<T> mmin = min as IComparable<T>;

                    while (enumerator.MoveNext())
                    {
                        T current = enumerator.Current;
                        if (mmin.CompareTo(current) > 0)
                            mmin = current as IComparable<T>;
                    }

                    return (T)mmin;
                }
                else if (min is IComparable)
                {
                    IComparable mmin = min as IComparable;

                    while (enumerator.MoveNext())
                    {
                        T current = enumerator.Current;
                        if (mmin.CompareTo(current) > 0)
                            mmin = current as IComparable;
                    }

                    return (T)mmin;
                }
                else
                    return default(T);
            }

            /// <summary>
            /// Finds the minimum of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and finding the minimum of the resulting values, by comparing the values using their IComparable&lt;<typeparamref name="T">T</typeparamref>&gt; implementation, or, if the values do not implement that interface, the non-generic IComparable interface.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="S">Type of the <paramref name="selector">selector</paramref> result value to compute the minimum for.</typeparam>
            /// <param name="source">Sequence to perform the minimum computation on.</param>
            /// <param name="selector">Function returning a value of type <paramref name="S">S</paramref> for each element of the source sequence, used to compute the minimum.</param>
            /// <returns>The minimum <paramref name="selector">selector</paramref> result value encountered while enumerating the source sequence.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static S Min<T, S>(this IEnumerable<T> source, Func<T, S> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                S min = selector(enumerator.Current);

                if (min is IComparable<S>)
                {
                    IComparable<S> mmin = min as IComparable<S>;

                    while (enumerator.MoveNext())
                    {
                        S current = selector(enumerator.Current);
                        if (mmin.CompareTo(current) > 0)
                            mmin = current as IComparable<S>;
                    }

                    return (S)mmin;
                }
                else if (min is IComparable)
                {
                    IComparable mmin = min as IComparable;

                    while (enumerator.MoveNext())
                    {
                        S current = selector(enumerator.Current);
                        if (mmin.CompareTo(current) > 0)
                            mmin = current as IComparable;
                    }

                    return (S)mmin;
                }
                else
                    return default(S);
            }

            #endregion

            #region int

            /// <summary>
            /// Finds the minimum of a sequence of integer values. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <param name="source">Sequence to compute the minimum for.</param>
            /// <returns>The minimum value encountered while enumerating the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static int Min(this IEnumerable<int> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<int> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                int min = enumerator.Current;
                while (enumerator.MoveNext())
                    min = Math.Min(min, enumerator.Current);

                return min;
            }

            /// <summary>
            /// Finds the minimum of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and finding the minimum of the resulting integer values. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the minimum computation on.</param>
            /// <param name="selector">Function returning an integer value for each element of the source sequence, used to compute the minimum.</param>
            /// <returns>The minimum <paramref name="selector">selector</paramref> result value encountered while enumerating the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static int Min<T>(this IEnumerable<T> source, Func<T, int> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                int min = selector(enumerator.Current);
                while (enumerator.MoveNext())
                    min = Math.Min(min, selector(enumerator.Current));

                return min;
            }

            #endregion

            #region long

            /// <summary>
            /// Finds the minimum of a sequence of long values. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <param name="source">Sequence to compute the minimum for.</param>
            /// <returns>The minimum value encountered while enumerating the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static long Min(this IEnumerable<long> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<long> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                long min = enumerator.Current;
                while (enumerator.MoveNext())
                    min = Math.Min(min, enumerator.Current);

                return min;
            }

            /// <summary>
            /// Finds the minimum of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and finding the minimum of the resulting long values. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the minimum computation on.</param>
            /// <param name="selector">Function returning a long value for each element of the source sequence, used to compute the minimum.</param>
            /// <returns>The minimum <paramref name="selector">selector</paramref> result value encountered while enumerating the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static long Min<T>(this IEnumerable<T> source, Func<T, long> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                long min = selector(enumerator.Current);
                while (enumerator.MoveNext())
                    min = Math.Min(min, selector(enumerator.Current));

                return min;
            }

            #endregion

            #region double

            /// <summary>
            /// Finds the minimum of a sequence of double values. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <param name="source">Sequence to compute the minimum for.</param>
            /// <returns>The minimum value encountered while enumerating the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double Min(this IEnumerable<double> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<double> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                double min = enumerator.Current;
                while (enumerator.MoveNext())
                    min = Math.Min(min, enumerator.Current);

                return min;
            }

            /// <summary>
            /// Finds the minimum of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and finding the minimum of the resulting double values. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the minimum computation on.</param>
            /// <param name="selector">Function returning a double value for each element of the source sequence, used to compute the minimum.</param>
            /// <returns>The minimum <paramref name="selector">selector</paramref> result value encountered while enumerating the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double Min<T>(this IEnumerable<T> source, Func<T, double> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                double min = selector(enumerator.Current);
                while (enumerator.MoveNext())
                    min = Math.Min(min, selector(enumerator.Current));

                return min;
            }

            #endregion

            #region decimal

            /// <summary>
            /// Finds the minimum of a sequence of decimal values. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <param name="source">Sequence to compute the minimum for.</param>
            /// <returns>The minimum value encountered while enumerating the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static decimal Min(this IEnumerable<decimal> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<decimal> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                decimal min = enumerator.Current;
                while (enumerator.MoveNext())
                    min = Math.Min(min, enumerator.Current);

                return min;
            }

            /// <summary>
            /// Finds the minimum of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and finding the minimum of the resulting decimal values. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the minimum computation on.</param>
            /// <param name="selector">Function returning a decimal value for each element of the source sequence, used to compute the minimum.</param>
            /// <returns>The minimum <paramref name="selector">selector</paramref> result value encountered while enumerating the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static decimal Min<T>(this IEnumerable<T> source, Func<T, decimal> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                decimal min = selector(enumerator.Current);
                while (enumerator.MoveNext())
                    min = Math.Min(min, selector(enumerator.Current));

                return min;
            }

            #endregion

            #region int?

            /// <summary>
            /// Finds the minimum of a sequence of integer values. A null value is returned if the source sequence is empty or if the source sequence only contains null values.
            /// </summary>
            /// <param name="source">Sequence to compute the minimum for.</param>
            /// <returns>The minimum value encountered while enumerating the source sequence. A null value is returned if the source sequence is empty or if the source sequence only contains null values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static int? Min(this IEnumerable<int?> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<int?> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    return null;

                int? min = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    int? current = enumerator.Current;
                    if (min != null && current != null)
                        min = Math.Min(min.Value, current.Value);
                }

                return min;
            }

            /// <summary>
            /// Finds the minimum of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and finding the minimum of the resulting integer values. A null value is returned if the source sequence is empty or if the source sequence only contains null values.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the minimum computation on.</param>
            /// <param name="selector">Function returning an integer value for each element of the source sequence, used to compute the minimum.</param>
            /// <returns>The minimum <paramref name="selector">selector</paramref> result value encountered while enumerating the source sequence. A null value is returned if the source sequence is empty or if the source sequence only contains null values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static int? Min<T>(this IEnumerable<T> source, Func<T, int?> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    return null;

                int? min = selector(enumerator.Current);
                while (enumerator.MoveNext())
                {
                    int? current = selector(enumerator.Current);
                    if (min != null && current != null)
                        min = Math.Min(min.Value, current.Value);
                }

                return min;
            }

            #endregion

            #region long?

            /// <summary>
            /// Finds the minimum of a sequence of long values. A null value is returned if the source sequence is empty or if the source sequence only contains null values.
            /// </summary>
            /// <param name="source">Sequence to compute the minimum for.</param>
            /// <returns>The minimum value encountered while enumerating the source sequence. A null value is returned if the source sequence is empty or if the source sequence only contains null values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static long? Min(this IEnumerable<long?> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<long?> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    return null;

                long? min = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    long? current = enumerator.Current;
                    if (min != null && current != null)
                        min = Math.Min(min.Value, current.Value);
                }

                return min;
            }

            /// <summary>
            /// Finds the minimum of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and finding the minimum of the resulting long values. A null value is returned if the source sequence is empty or if the source sequence only contains null values.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the minimum computation on.</param>
            /// <param name="selector">Function returning a long value for each element of the source sequence, used to compute the minimum.</param>
            /// <returns>The minimum <paramref name="selector">selector</paramref> result value encountered while enumerating the source sequence. A null value is returned if the source sequence is empty or if the source sequence only contains null values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static long? Min<T>(this IEnumerable<T> source, Func<T, long?> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    return null;

                long? min = selector(enumerator.Current);
                while (enumerator.MoveNext())
                {
                    long? current = selector(enumerator.Current);
                    if (min != null && current != null)
                        min = Math.Min(min.Value, current.Value);
                }

                return min;
            }

            #endregion

            #region double?

            /// <summary>
            /// Finds the minimum of a sequence of double values. A null value is returned if the source sequence is empty or if the source sequence only contains null values.
            /// </summary>
            /// <param name="source">Sequence to compute the minimum for.</param>
            /// <returns>The minimum value encountered while enumerating the source sequence. A null value is returned if the source sequence is empty or if the source sequence only contains null values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double? Min(this IEnumerable<double?> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<double?> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    return null;

                double? min = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    double? current = enumerator.Current;
                    if (min != null && current != null)
                        min = Math.Min(min.Value, current.Value);
                }

                return min;
            }

            /// <summary>
            /// Finds the minimum of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and finding the minimum of the resulting double values. A null value is returned if the source sequence is empty or if the source sequence only contains null values.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the minimum computation on.</param>
            /// <param name="selector">Function returning a double value for each element of the source sequence, used to compute the minimum.</param>
            /// <returns>The minimum <paramref name="selector">selector</paramref> result value encountered while enumerating the source sequence. A null value is returned if the source sequence is empty or if the source sequence only contains null values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double? Min<T>(this IEnumerable<T> source, Func<T, double?> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    return null;

                double? min = selector(enumerator.Current);
                while (enumerator.MoveNext())
                {
                    double? current = selector(enumerator.Current);
                    if (min != null && current != null)
                        min = Math.Min(min.Value, current.Value);
                }

                return min;
            }

            #endregion

            #region decimal?

            /// <summary>
            /// Finds the minimum of a sequence of decimal values. A null value is returned if the source sequence is empty or if the source sequence only contains null values.
            /// </summary>
            /// <param name="source">Sequence to compute the minimum for.</param>
            /// <returns>The minimum value encountered while enumerating the source sequence. A null value is returned if the source sequence is empty or if the source sequence only contains null values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static decimal? Min(this IEnumerable<decimal?> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<decimal?> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    return null;

                decimal? min = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    decimal? current = enumerator.Current;
                    if (min != null && current != null)
                        min = Math.Min(min.Value, current.Value);
                }

                return min;
            }

            /// <summary>
            /// Finds the minimum of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and finding the minimum of the resulting decimal values. A null value is returned if the source sequence is empty or if the source sequence only contains null values.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the minimum computation on.</param>
            /// <param name="selector">Function returning a decimal value for each element of the source sequence, used to compute the minimum.</param>
            /// <returns>The minimum <paramref name="selector">selector</paramref> result value encountered while enumerating the source sequence. A null value is returned if the source sequence is empty or if the source sequence only contains null values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static decimal? Min<T>(this IEnumerable<T> source, Func<T, decimal?> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    return null;

                decimal? min = selector(enumerator.Current);
                while (enumerator.MoveNext())
                {
                    decimal? current = selector(enumerator.Current);
                    if (min != null && current != null)
                        min = Math.Min(min.Value, current.Value);
                }

                return min;
            }

            #endregion

            #endregion

            #region 1.16.5 Max

            #region general

            /// <summary>
            /// Finds the maximum of a sequence of values by enumerating the sequence and comparing the values using their IComparable&lt;<typeparamref name="T">T</typeparamref>&gt; implementation, or, if the values do not implement that interface, the non-generic IComparable interface.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to compute the maximum for.</param>
            /// <returns>The maximum value encountered while enumerating the source sequence.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static T Max<T>(this IEnumerable<T> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                T max = enumerator.Current;

                if (max is IComparable<T>)
                {
                    IComparable<T> mmax = max as IComparable<T>;

                    while (enumerator.MoveNext())
                    {
                        T current = enumerator.Current;
                        if (mmax.CompareTo(current) < 0)
                            mmax = current as IComparable<T>;
                    }

                    return (T)mmax;
                }
                else if (max is IComparable)
                {
                    IComparable mmax = max as IComparable;

                    while (enumerator.MoveNext())
                    {
                        T current = enumerator.Current;
                        if (mmax.CompareTo(current) < 0)
                            mmax = current as IComparable;
                    }

                    return (T)mmax;
                }
                else
                    return default(T);
            }

            /// <summary>
            /// Finds the maximum of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and finding the maximum of the resulting values, by comparing the values using their IComparable&lt;<typeparamref name="T">T</typeparamref>&gt; implementation, or, if the values do not implement that interface, the non-generic IComparable interface.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="S">Type of the <paramref name="selector">selector</paramref> result value to compute the maximum for.</typeparam>
            /// <param name="source">Sequence to perform the maximum computation on.</param>
            /// <param name="selector">Function returning a value of type <paramref name="S">S</paramref> for each element of the source sequence, used to compute the maximum.</param>
            /// <returns>The maximum <paramref name="selector">selector</paramref> result value encountered while enumerating the source sequence.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static S Max<T, S>(this IEnumerable<T> source, Func<T, S> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                S max = selector(enumerator.Current);

                if (max is IComparable<S>)
                {
                    IComparable<S> mmax = max as IComparable<S>;

                    while (enumerator.MoveNext())
                    {
                        S current = selector(enumerator.Current);
                        if (mmax.CompareTo(current) < 0)
                            mmax = current as IComparable<S>;
                    }

                    return (S)mmax;
                }
                else if (max is IComparable)
                {
                    IComparable mmax = max as IComparable;

                    while (enumerator.MoveNext())
                    {
                        S current = selector(enumerator.Current);
                        if (mmax.CompareTo(current) < 0)
                            mmax = current as IComparable;
                    }

                    return (S)mmax;
                }
                else
                    return default(S);
            }

            #endregion

            #region int

            /// <summary>
            /// Finds the maximum of a sequence of integer values. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <param name="source">Sequence to compute the maximum for.</param>
            /// <returns>The maximum value encountered while enumerating the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static int Max(this IEnumerable<int> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<int> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                int max = enumerator.Current;
                while (enumerator.MoveNext())
                    max = Math.Max(max, enumerator.Current);

                return max;
            }

            /// <summary>
            /// Finds the maximum of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and finding the maximum of the resulting integer values. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the maximum computation on.</param>
            /// <param name="selector">Function returning an integer value for each element of the source sequence, used to compute the maximum.</param>
            /// <returns>The maximum <paramref name="selector">selector</paramref> result value encountered while enumerating the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static int Max<T>(this IEnumerable<T> source, Func<T, int> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                int max = selector(enumerator.Current);
                while (enumerator.MoveNext())
                    max = Math.Max(max, selector(enumerator.Current));

                return max;
            }

            #endregion

            #region long

            /// <summary>
            /// Finds the maximum of a sequence of long values. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <param name="source">Sequence to compute the maximum for.</param>
            /// <returns>The maximum value encountered while enumerating the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static long Max(this IEnumerable<long> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<long> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                long max = enumerator.Current;
                while (enumerator.MoveNext())
                    max = Math.Max(max, enumerator.Current);

                return max;
            }

            /// <summary>
            /// Finds the maximum of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and finding the maximum of the resulting long values. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the maximum computation on.</param>
            /// <param name="selector">Function returning a long value for each element of the source sequence, used to compute the maximum.</param>
            /// <returns>The maximum <paramref name="selector">selector</paramref> result value encountered while enumerating the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static long Max<T>(this IEnumerable<T> source, Func<T, long> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                long max = selector(enumerator.Current);
                while (enumerator.MoveNext())
                    max = Math.Max(max, selector(enumerator.Current));

                return max;
            }

            #endregion

            #region double

            /// <summary>
            /// Finds the maximum of a sequence of double values. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <param name="source">Sequence to compute the maximum for.</param>
            /// <returns>The maximum value encountered while enumerating the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double Max(this IEnumerable<double> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<double> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                double max = enumerator.Current;
                while (enumerator.MoveNext())
                    max = Math.Max(max, enumerator.Current);

                return max;
            }

            /// <summary>
            /// Finds the maximum of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and finding the maximum of the resulting double values. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the maximum computation on.</param>
            /// <param name="selector">Function returning a double value for each element of the source sequence, used to compute the maximum.</param>
            /// <returns>The maximum <paramref name="selector">selector</paramref> result value encountered while enumerating the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double Max<T>(this IEnumerable<T> source, Func<T, double> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                double max = selector(enumerator.Current);
                while (enumerator.MoveNext())
                    max = Math.Max(max, selector(enumerator.Current));

                return max;
            }

            #endregion

            #region decimal

            /// <summary>
            /// Finds the maximum of a sequence of decimal values. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <param name="source">Sequence to compute the maximum for.</param>
            /// <returns>The maximum value encountered while enumerating the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static decimal Max(this IEnumerable<decimal> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<decimal> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                decimal max = enumerator.Current;
                while (enumerator.MoveNext())
                    max = Math.Max(max, enumerator.Current);

                return max;
            }

            /// <summary>
            /// Finds the maximum of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and finding the maximum of the resulting decimal values. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the maximum computation on.</param>
            /// <param name="selector">Function returning a decimal value for each element of the source sequence, used to compute the maximum.</param>
            /// <returns>The maximum <paramref name="selector">selector</paramref> result value encountered while enumerating the source sequence. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static decimal Max<T>(this IEnumerable<T> source, Func<T, decimal> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                decimal max = selector(enumerator.Current);
                while (enumerator.MoveNext())
                    max = Math.Max(max, selector(enumerator.Current));

                return max;
            }

            #endregion

            #region int?

            /// <summary>
            /// Finds the maximum of a sequence of integer values. A null value is returned if the source sequence is empty or if the source sequence only contains null values.
            /// </summary>
            /// <param name="source">Sequence to compute the maximum for.</param>
            /// <returns>The maximum value encountered while enumerating the source sequence. A null value is returned if the source sequence is empty or if the source sequence only contains null values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static int? Max(this IEnumerable<int?> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<int?> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    return null;

                int? max = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    int? current = enumerator.Current;
                    if (max != null && current != null)
                        max = Math.Max(max.Value, current.Value);
                }

                return max;
            }

            /// <summary>
            /// Finds the maximum of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and finding the maximum of the resulting integer values. A null value is returned if the source sequence is empty or if the source sequence only contains null values.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the maximum computation on.</param>
            /// <param name="selector">Function returning an integer value for each element of the source sequence, used to compute the maximum.</param>
            /// <returns>The maximum <paramref name="selector">selector</paramref> result value encountered while enumerating the source sequence. A null value is returned if the source sequence is empty or if the source sequence only contains null values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static int? Max<T>(this IEnumerable<T> source, Func<T, int?> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    return null;

                int? max = selector(enumerator.Current);
                while (enumerator.MoveNext())
                {
                    int? current = selector(enumerator.Current);
                    if (max != null && current != null)
                        max = Math.Max(max.Value, current.Value);
                }

                return max;
            }

            #endregion

            #region long?

            /// <summary>
            /// Finds the maximum of a sequence of long values. A null value is returned if the source sequence is empty or if the source sequence only contains null values.
            /// </summary>
            /// <param name="source">Sequence to compute the maximum for.</param>
            /// <returns>The maximum value encountered while enumerating the source sequence. A null value is returned if the source sequence is empty or if the source sequence only contains null values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static long? Max(this IEnumerable<long?> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<long?> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    return null;

                long? max = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    long? current = enumerator.Current;
                    if (max != null && current != null)
                        max = Math.Max(max.Value, current.Value);
                }

                return max;
            }

            /// <summary>
            /// Finds the maximum of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and finding the maximum of the resulting long values. A null value is returned if the source sequence is empty or if the source sequence only contains null values.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the maximum computation on.</param>
            /// <param name="selector">Function returning a long value for each element of the source sequence, used to compute the maximum.</param>
            /// <returns>The maximum <paramref name="selector">selector</paramref> result value encountered while enumerating the source sequence. A null value is returned if the source sequence is empty or if the source sequence only contains null values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static long? Max<T>(this IEnumerable<T> source, Func<T, long?> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    return null;

                long? max = selector(enumerator.Current);
                while (enumerator.MoveNext())
                {
                    long? current = selector(enumerator.Current);
                    if (max != null && current != null)
                        max = Math.Max(max.Value, current.Value);
                }

                return max;
            }

            #endregion

            #region double?

            /// <summary>
            /// Finds the maximum of a sequence of double values. A null value is returned if the source sequence is empty or if the source sequence only contains null values.
            /// </summary>
            /// <param name="source">Sequence to compute the maximum for.</param>
            /// <returns>The maximum value encountered while enumerating the source sequence. A null value is returned if the source sequence is empty or if the source sequence only contains null values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double? Max(this IEnumerable<double?> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<double?> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    return null;

                double? max = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    double? current = enumerator.Current;
                    if (max != null && current != null)
                        max = Math.Max(max.Value, current.Value);
                }

                return max;
            }

            /// <summary>
            /// Finds the maximum of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and finding the maximum of the resulting double values. A null value is returned if the source sequence is empty or if the source sequence only contains null values.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the maximum computation on.</param>
            /// <param name="selector">Function returning a double value for each element of the source sequence, used to compute the maximum.</param>
            /// <returns>The maximum <paramref name="selector">selector</paramref> result value encountered while enumerating the source sequence. A null value is returned if the source sequence is empty or if the source sequence only contains null values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double? Max<T>(this IEnumerable<T> source, Func<T, double?> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    return null;

                double? max = selector(enumerator.Current);
                while (enumerator.MoveNext())
                {
                    double? current = selector(enumerator.Current);
                    if (max != null && current != null)
                        max = Math.Max(max.Value, current.Value);
                }

                return max;
            }

            #endregion

            #region decimal?

            /// <summary>
            /// Finds the maximum of a sequence of decimal values. A null value is returned if the source sequence is empty or if the source sequence only contains null values.
            /// </summary>
            /// <param name="source">Sequence to compute the maximum for.</param>
            /// <returns>The maximum value encountered while enumerating the source sequence. A null value is returned if the source sequence is empty or if the source sequence only contains null values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static decimal? Max(this IEnumerable<decimal?> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                IEnumerator<decimal?> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    return null;

                decimal? max = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    decimal? current = enumerator.Current;
                    if (max != null && current != null)
                        max = Math.Max(max.Value, current.Value);
                }

                return max;
            }

            /// <summary>
            /// Finds the maximum of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and finding the maximum of the resulting decimal values. A null value is returned if the source sequence is empty or if the source sequence only contains null values.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the maximum computation on.</param>
            /// <param name="selector">Function returning a decimal value for each element of the source sequence, used to compute the maximum.</param>
            /// <returns>The maximum <paramref name="selector">selector</paramref> result value encountered while enumerating the source sequence. A null value is returned if the source sequence is empty or if the source sequence only contains null values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static decimal? Max<T>(this IEnumerable<T> source, Func<T, decimal?> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    return null;

                decimal? max = selector(enumerator.Current);
                while (enumerator.MoveNext())
                {
                    decimal? current = selector(enumerator.Current);
                    if (max != null && current != null)
                        max = Math.Max(max.Value, current.Value);
                }

                return max;
            }

            #endregion

            #endregion

            #region 1.16.6 Average

            #region int

            /// <summary>
            /// Computes the average of a sequence of integer values. If the sequence is empty, an <c>ArgumentNullException</c> is thrown. If the sum of the elements is too large to represent in a long, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <param name="source">Sequence to compute the average for.</param>
            /// <returns>The average of the source sequence. If the sequence is empty, an <c>ArgumentNullException</c> is thrown. If the sum of the elements is too large to represent in a long, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double Average(this IEnumerable<int> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                checked
                {
                    long sum = 0;
                    long n = 0;
                    foreach (int item in source)
                    {
                        sum += item;
                        n++;
                    }

                    if (n != 0)
                        return (double)sum / n;
                    else
                        throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Computes the average of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and calculating the average of the resulting values. If the sequence is empty, an <c>ArgumentNullException</c> is thrown. If the sum of the result values is too large to represent in a long, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the average computation on.</param>
            /// <param name="selector">Function returning an integer value for each element of the source sequence, used to compute the average.</param>
            /// <returns>The average of the <paramref name="selector">selector</paramref> result values encountered while enumerating the source sequence. If the sequence is empty, an <c>ArgumentNullException</c> is thrown. If the sum of the result values is too large to represent in a long, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double Average<T>(this IEnumerable<T> source, Func<T, int> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                checked
                {
                    long sum = 0;
                    long n = 0;
                    foreach (T item in source)
                    {
                        sum += selector(item);
                        n++;
                    }

                    if (n != 0)
                        return (double)sum / n;
                    else
                        throw new InvalidOperationException();
                }
            }

            #endregion

            #region long

            /// <summary>
            /// Computes the average of a sequence of long values. If the sequence is empty, an <c>ArgumentNullException</c> is thrown. If the sum of the elements is too large to represent in a long, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <param name="source">Sequence to compute the average for.</param>
            /// <returns>The average of the source sequence. If the sequence is empty, an <c>ArgumentNullException</c> is thrown. If the sum of the elements is too large to represent in a long, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double Average(this IEnumerable<long> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                checked
                {
                    long sum = 0;
                    long n = 0;
                    foreach (int item in source)
                    {
                        sum += item;
                        n++;
                    }

                    if (n != 0)
                        return (double)sum / n;
                    else
                        throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Computes the average of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and calculating the average of the resulting values. If the sequence is empty, an <c>ArgumentNullException</c> is thrown. If the sum of the result values is too large to represent in a long, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the average computation on.</param>
            /// <param name="selector">Function returning a long value for each element of the source sequence, used to compute the average.</param>
            /// <returns>The average of the <paramref name="selector">selector</paramref> result values encountered while enumerating the source sequence. If the sequence is empty, an <c>ArgumentNullException</c> is thrown. If the sum of the result values is too large to represent in a long, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double Average<T>(this IEnumerable<T> source, Func<T, long> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                checked
                {
                    long sum = 0;
                    long n = 0;
                    foreach (T item in source)
                    {
                        sum += selector(item);
                        n++;
                    }

                    if (n != 0)
                        return (double)sum / n;
                    else
                        throw new InvalidOperationException();
                }
            }

            #endregion

            #region double

            /// <summary>
            /// Computes the average of a sequence of double values. If the sequence is empty, an <c>ArgumentNullException</c> is thrown.
            /// </summary>
            /// <param name="source">Sequence to compute the average for.</param>
            /// <returns>The average of the source sequence. If the sequence is empty, an <c>ArgumentNullException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double Average(this IEnumerable<double> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                double sum = 0;
                long n = 0;
                foreach (double item in source)
                {
                    sum += item;
                    n++;
                }

                if (n != 0)
                    return sum / n;
                else
                    throw new InvalidOperationException();
            }

            /// <summary>
            /// Computes the average of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and calculating the average of the resulting values. If the sequence is empty, an <c>ArgumentNullException</c> is thrown.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the average computation on.</param>
            /// <param name="selector">Function returning a double value for each element of the source sequence, used to compute the average.</param>
            /// <returns>The average of the <paramref name="selector">selector</paramref> result values encountered while enumerating the source sequence. If the sequence is empty, an <c>ArgumentNullException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double Average<T>(this IEnumerable<T> source, Func<T, double> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                double sum = 0;
                long n = 0;
                foreach (T item in source)
                {
                    sum += selector(item);
                    n++;
                }

                if (n != 0)
                    return sum / n;
                else
                    throw new InvalidOperationException();
            }

            #endregion

            #region decimal

            /// <summary>
            /// Computes the average of a sequence of decimal values.  If the sequence is empty, an <c>ArgumentNullException</c> is thrown.If the sum of the elements is too large to represent in a decimal, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <param name="source">Sequence to compute the average for.</param>
            /// <returns>The average of the source sequence.  If the sequence is empty, an <c>ArgumentNullException</c> is thrown.If the sum of the elements is too large to represent in a decimal, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static decimal Average(this IEnumerable<decimal> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                checked
                {
                    decimal sum = 0;
                    long n = 0;
                    foreach (decimal item in source)
                    {
                        sum += item;
                        n++;
                    }

                    if (n != 0)
                        return sum / n;
                    else
                        throw new InvalidOperationException();
                }
            }

            /// <summary>
            /// Computes the average of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and calculating the average of the resulting values. If the sequence is empty, an <c>ArgumentNullException</c> is thrown. If the sum of the result values is too large to represent in a decimal, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the average computation on.</param>
            /// <param name="selector">Function returning a decimal value for each element of the source sequence, used to compute the average.</param>
            /// <returns>The average of the <paramref name="selector">selector</paramref> result values encountered while enumerating the source sequence. If the sequence is empty, an <c>ArgumentNullException</c> is thrown. If the sum of the result values is too large to represent in a decimal, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static decimal Average<T>(this IEnumerable<T> source, Func<T, decimal> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                checked
                {
                    decimal sum = 0;
                    long n = 0;
                    foreach (T item in source)
                    {
                        sum += selector(item);
                        n++;
                    }

                    if (n != 0)
                        return sum / n;
                    else
                        throw new InvalidOperationException();
                }
            }

            #endregion

            #region int?

            /// <summary>
            /// Computes the average of a sequence of integer values excluding null values. If the sequence is empty or contains only null values, null is returned. If the sum of the elements is too large to represent in a long, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <param name="source">Sequence to compute the average for.</param>
            /// <returns>The average of the source sequence excluding null values, null if the sequence is empty or contains only null values. If the sum of the elements is too large to represent in a long, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double? Average(this IEnumerable<int?> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                checked
                {
                    long sum = 0;
                    long n = 0;
                    foreach (int? item in source)
                    {
                        if (item != null)
                        {
                            sum += item.Value;
                            n++;
                        }
                    }

                    if (n != 0)
                        return (double)sum / n;
                    else
                        return null;
                }
            }

            /// <summary>
            /// Computes the average of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and calculating the average of the non-null resulting values. If the sequence is empty or if the <paramref name="selector">selector</paramref> only returned null values, null is returned. If the sum of the result values is too large to represent in a long, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the average computation on.</param>
            /// <param name="selector">Function returning an integer value for each element of the source sequence, used to compute the average.</param>
            /// <returns>The average of the non-null <paramref name="selector">selector</paramref> result values encountered while enumerating the source sequence. If the sequence is empty or if the <paramref name="selector">selector</paramref> only returned null values, null is returned. If the sum of the result values is too large to represent in a long, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double? Average<T>(this IEnumerable<T> source, Func<T, int?> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                checked
                {
                    long sum = 0;
                    long n = 0;
                    foreach (T item in source)
                    {
                        int? res = selector(item);
                        if (res != null)
                        {
                            sum += res.Value;
                            n++;
                        }
                    }

                    if (n != 0)
                        return (double)sum / n;
                    else
                        return null;
                }
            }

            #endregion

            #region long?

            /// <summary>
            /// Computes the average of a sequence of long values excluding null values. If the sequence is empty or contains only null values, null is returned. If the sum of the elements is too large to represent in a long, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <param name="source">Sequence to compute the average for.</param>
            /// <returns>The average of the source sequence excluding null values, null if the sequence is empty or contains only null values. If the sum of the elements is too large to represent in a long, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double? Average(this IEnumerable<long?> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                checked
                {
                    long sum = 0;
                    long n = 0;
                    foreach (long? item in source)
                    {
                        if (item != null)
                        {
                            sum += item.Value;
                            n++;
                        }
                    }

                    if (n != 0)
                        return (double)sum / n;
                    else
                        return null;
                }
            }

            /// <summary>
            /// Computes the average of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and calculating the average of the non-null resulting values. If the sequence is empty or if the <paramref name="selector">selector</paramref> only returned null values, null is returned. If the sum of the result values is too large to represent in a long, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the average computation on.</param>
            /// <param name="selector">Function returning a long value for each element of the source sequence, used to compute the average.</param>
            /// <returns>The average of the non-null <paramref name="selector">selector</paramref> result values encountered while enumerating the source sequence. If the sequence is empty or if the <paramref name="selector">selector</paramref> only returned null values, null is returned. If the sum of the result values is too large to represent in a long, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double? Average<T>(this IEnumerable<T> source, Func<T, long?> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                checked
                {
                    long sum = 0;
                    long n = 0;
                    foreach (T item in source)
                    {
                        long? res = selector(item);
                        if (res != null)
                        {
                            sum += res.Value;
                            n++;
                        }
                    }

                    if (n != 0)
                        return (double)sum / n;
                    else
                        return null;
                }
            }

            #endregion

            #region double?

            /// <summary>
            /// Computes the average of a sequence of double values excluding null values. If the sequence is empty or contains only null values, null is returned.
            /// </summary>
            /// <param name="source">Sequence to compute the average for.</param>
            /// <returns>The average of the source sequence excluding null values, null if the sequence is empty or contains only null values.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double? Average(this IEnumerable<double?> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                double sum = 0;
                long n = 0;
                foreach (double? item in source)
                {
                    if (item != null)
                    {
                        sum += item.Value;
                        n++;
                    }
                }

                if (n != 0)
                    return sum / n;
                else
                    return null;
            }

            /// <summary>
            /// Computes the average of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and calculating the average of the non-null resulting values. If the sequence is empty or if the <paramref name="selector">selector</paramref> only returned null values, null is returned.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the average computation on.</param>
            /// <param name="selector">Function returning a double value for each element of the source sequence, used to compute the average.</param>
            /// <returns>The average of the non-null <paramref name="selector">selector</paramref> result values encountered while enumerating the source sequence. If the sequence is empty or if the <paramref name="selector">selector</paramref> only returned null values, null is returned.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static double? Average<T>(this IEnumerable<T> source, Func<T, double?> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                double sum = 0;
                long n = 0;
                foreach (T item in source)
                {
                    double? res = selector(item);
                    if (res != null)
                    {
                        sum += res.Value;
                        n++;
                    }
                }

                if (n != 0)
                    return sum / n;
                else
                    return null;
            }

            #endregion

            #region decimal?

            /// <summary>
            /// Computes the average of a sequence of decimal values excluding null values. If the sequence is empty or contains only null values, null is returned. If the sum of the elements is too large to represent in a decimal, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <param name="source">Sequence to compute the average for.</param>
            /// <returns>The average of the source sequence excluding null values, null if the sequence is empty or contains only null values. If the sum of the elements is too large to represent in a decimal, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static decimal? Average(this IEnumerable<decimal?> source)
            {
                if (source == null)
                    throw new ArgumentNullException();

                decimal sum = 0;
                long n = 0;
                foreach (decimal? item in source)
                {
                    if (item != null)
                    {
                        sum += item.Value;
                        n++;
                    }
                }

                if (n != 0)
                    return sum / n;
                else
                    return null;
            }

            /// <summary>
            /// Computes the average of a sequence of by enumerating the sequence, invoking the <paramref name="selector">selector</paramref> function for each element and calculating the average of the non-null resulting values. If the sequence is empty or if the <paramref name="selector">selector</paramref> only returned null values, null is returned. If the sum of the result values is too large to represent in a decimal, an <c>OverflowException</c> is thrown.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to perform the average computation on.</param>
            /// <param name="selector">Function returning a decimal value for each element of the source sequence, used to compute the average.</param>
            /// <returns>The average of the non-null <paramref name="selector">selector</paramref> result values encountered while enumerating the source sequence. If the sequence is empty or if the <paramref name="selector">selector</paramref> only returned null values, null is returned. If the sum of the result values is too large to represent in a decimal, an <c>OverflowException</c> is thrown.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static decimal? Average<T>(this IEnumerable<T> source, Func<T, decimal?> selector)
            {
                if (source == null || selector == null)
                    throw new ArgumentNullException();

                decimal sum = 0;
                long n = 0;
                foreach (T item in source)
                {
                    decimal? res = selector(item);
                    if (res != null)
                    {
                        sum += res.Value;
                        n++;
                    }
                }

                if (n != 0)
                    return sum / n;
                else
                    return null;
            }

            #endregion

            #endregion

            #region 1.16.7 Aggregate

            /// <summary>
            /// Applies an aggregation function over a sequence. The operator uses the first element of the sequence as the seed value which is then assigned to an internal accumulator. It then enumerates the source sequence, repeatedly computing the next accumulator value by invoking the specified <paramref name="func">function</paramref> with the current accumulator value as the first argument and the current sequence element as the second argument. The final accumulator value is returned as the result. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <param name="source">Sequence to compute the aggregation for.</param>
            /// <param name="func">Function to calculate the aggregated value based on the current internal accumulator value and the current element from the source sequence. This function is called repeatedly for each element of the source sequence in order to obtain the aggregation value.</param>
            /// <returns>The aggregated value of the source sequence obtained by calling the <paramref name="func">aggregation function</paramref> repeatedly for each element in the sequence, starting with the first element as a seed value and using an internal accumulator. An <c>InvalidOperationException</c> is thrown if the source sequence is empty.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static T Aggregate<T>(this IEnumerable<T> source, Func<T, T, T> func)
            {
                if (source == null || func == null)
                    throw new ArgumentNullException();

                IEnumerator<T> enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext())
                    throw new InvalidOperationException();

                T result = enumerator.Current;

                while (enumerator.MoveNext())
                    result = func(result, enumerator.Current);

                return result;
            }

            /// <summary>
            /// Applies an aggregation function over a sequence. The operator starts by assigning the <paramref name="seed">seed</paramref> value to an internal accumulator. It then enumerates the source sequence, repeatedly computing the next accumulator value by invoking the specified <paramref name="func">function</paramref> with the current accumulator value as the first argument and the current sequence element as the second argument. The final accumulator value is returned as the result.
            /// </summary>
            /// <typeparam name="T">Type of the elements in the source sequence.</typeparam>
            /// <typeparam name="U">Type of the <paramref name="func">aggregation function</paramref>'s first parameter and the <paramref name="seed">seed</paramref> value.</typeparam>
            /// <param name="source">Sequence to compute the aggregation for.</param>
            /// <param name="seed">Seed value to assign to the internal accumulator before starting the aggregation calculation.</param>
            /// <param name="func">Function to calculate the aggregated value based on the current internal accumulator value and the current element from the source sequence. This function is called repeatedly for each element of the source sequence in order to obtain the aggregation value.</param>
            /// <returns>The aggregated value of the source sequence obtained by calling the <paramref name="func">aggregation function</paramref> repeatedly for each element in the sequence, starting with the specified <paramref name="seed">seed</paramref> value and using an internal accumulator.</returns>
            /// <remarks>Extension method for IEnumerable&lt;<typeparamref name="T">T</typeparamref>&gt;.</remarks>
            public static U Aggregate<T, U>(this IEnumerable<T> source, U seed, Func<U, T, U> func)
            {
                if (source == null || func == null)
                    throw new ArgumentNullException();

                U result = seed;

                foreach (T item in source)
                    result = func(result, item);

                return result;
            }

            #endregion

            #endregion
        }

        #endregion

        #region 1.8.1 OrderBy / ThenBy (bis)

        #region OrderedSequence class

        /// <summary>
        /// Represents a (hierarchically) ordered sequence.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the ordered sequence.</typeparam>
        public class OrderedSequence<T> : System.Collections.Generic.IEnumerable<T>
        {
            /// <summary>
            /// Used when this ordered sequence is at the leaf level. At the leaf level, an ordered sequence contains a list with the ordered groups of elements (each represented as a List&lt;<typeparamref name="T">T</typeparamref>&gt;).
            /// </summary>
            /// <example>
            /// Assume a <c>new List&lt;T&gt; { "AAA", "AAB", "ABA", "ABB", "BAA", "BAB", "BBA", "BBB" }</c> sorted by the first character of the strings. The corresponding <c>OrderedSequence&lt;T&gt;</c> then contains: 
            /// <c>_lst = new List&lt;List&lt;T&gt;&gt; { new List&lt;T&gt; { "AAA", "AAB", "ABA", "ABB" }, new List&lt;T&gt; { "BAA", "BAB", "BBA", "BBB" } }</c>.
            /// </example>
            private IList<List<T>> _lst;

            /// <summary>
            /// Used when this ordered sequence is not at the leaf level. Inside the tree, an ordered sequence contains a list of ordered sequences (each represented as a OrderedSequence&lt;<typeparamref name="T">T</typeparamref>&gt;) one level deeper in the tree.
            /// </summary>
            /// <example>
            /// Assume a <c>new List&lt;T&gt; { "AAA", "AAB", "ABA", "ABB", "BAA", "BAB", "BBA", "BBB" }</c> sorted by the first character of the strings and then by the second character of the strings. The corresponding <c>OrderedSequence&lt;T&gt;</c> then contains: 
            /// <c>_children = new List&lt;OrderedSequence&lt;T&gt;&gt; { a, b }</c> 
            /// where 
            /// <c>a = new List&lt;List&lt;T&gt;&gt; { new List&lt;T&gt; { "AAA", "AAB" }, new List&lt;T&gt; { "ABA", "ABB" } }</c> and <c>b = new List&lt;List&lt;T&gt;&gt; { new List&lt;T&gt; { "BAA", "BAB" }, new List&lt;T&gt; { "BBA", "BBB" } }</c> 
            /// are both leaf-level ordered sequences.
            /// </example>
            private IList<OrderedSequence<T>> _children;

            /// <summary>
            /// Indicates whether this ordered sequence is at the leaf level or not.
            /// </summary>
            private bool _leaf;

            /// <summary>
            /// Creates a leaf-level ordered sequence with the sorted list of element groups.
            /// </summary>
            /// <param name="lst">
            /// Sorted list of element groups.
            /// <example><c>new List&lt;List&lt;T&gt;&gt; { new List&lt;T&gt; { "AAA", "AAB", "ABA", "ABB" }, new List&lt;T&gt; { "BAA", "BAB", "BBA", "BBB" } }</c></example>
            /// </param>
            internal OrderedSequence(IList<List<T>> lst)
            {
                _lst = lst;
                _leaf = true;
            }

            /// <summary>
            /// Creates a non leaf-level ordered sequence with the sorted list of children ordered sequences.
            /// </summary>
            /// <param name="children">
            /// Sorted list of children ordered sequences.
            /// <example><c>new List&lt;OrderedSequence&lt;T&gt;&gt; { a, b }</c> where <c>a</c> and <c>b</c> are both <c>OrderedSequence&lt;T&gt;</c> instances</example>
            /// </param>
            internal OrderedSequence(IList<OrderedSequence<T>> children)
            {
                _children = children;
                _leaf = false;
            }

            /// <summary>
            /// Indicates whether this ordered sequence is at the leaf level.
            /// </summary>
            internal bool IsLeaf
            {
                get { return _leaf; }
            }

            /// <summary>
            /// Returns the sorted list of element groups if the ordered sequence is at the leaf level, null otherwise.
            /// </summary>
            internal IList<List<T>> Items
            {
                get { return _lst; }
            }

            /// <summary>
            /// Returns the sorted list of child OrderedSequence objects if the ordered sequence isn't at the leaf level, null otherwise.
            /// </summary>
            internal IList<OrderedSequence<T>> Children
            {
                get { return _children; }
            }

            /// <summary>
            /// Promotes a leaf level ordered sequence
            /// </summary>
            /// <param name="children"></param>
            /// <example>
            /// Used when the leaf level ordered sequence characterized by 
            /// <c>_lst = new List&lt;List&lt;T&gt;&gt; { new List&lt;T&gt; { "AAA", "AAB", "ABA", "ABB" }, new List&lt;T&gt; { "BAA", "BAB", "BBA", "BBB" } }</c> 
            /// is sorted on the second character of the strings, resulting in 
            /// <c>_children = new List&lt;OrderedSequence&lt;T&gt;&gt; { a, b }</c> with <c>a = new List&lt;List&lt;T&gt;&gt; { new List&lt;T&gt; { "AAA", "AAB" }, new List&lt;T&gt; { "ABA", "ABB" } }</c> and <c>b = new List&lt;List&lt;T&gt;&gt; { new List&lt;T&gt; { "BAA", "BAB" }, new List&lt;T&gt; { "BBA", "BBB" } }</c>, 
            /// where the leaf level ordered sequence was patched to point to two new leaf level ordered sequences <c>a</c> and <c>b</c>.
            /// </example>
            internal void Patch(IList<OrderedSequence<T>> children)
            {
                _children = children;
                _leaf = false;
            }

            /// <summary>
            /// Retrieves the generic enumerator used to enumerate the elements in the ordered sequence by traversing the OrderedSequence&lt;T&gt; tree on the leaf level (using recursion).
            /// </summary>
            /// <returns>Generic enumerator that yields the elements on the leaf level of the ordered sequence.</returns>
            public System.Collections.Generic.IEnumerator<T> GetEnumerator()
            {
                if (_leaf)
                    foreach (IEnumerable<T> item in _lst)
                        foreach (T child in item)
                            yield return child;
                else
                    foreach (IEnumerable<T> item in _children)
                        foreach (T child in item)
                            yield return child;
            }

            /// <summary>
            /// Retrieves the non-generic enumerator.
            /// </summary>
            /// <returns>Non-generic enumerator.</returns>
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            /// <summary>
            /// Makes a clone (deep copy) of the current ordered sequence (using recursion) in order to be able to <see cref="Patch">Patch</see> an <c>OrderedSequence&lt;T&gt;</c> tree without loss of the original tree.
            /// </summary>
            /// <returns>Deep copy of the current ordered sequence.</returns>
            internal OrderedSequence<T> Clone()
            {
                if (_leaf)
                    return new OrderedSequence<T>(new List<List<T>>(_lst));
                else
                {
                    List<OrderedSequence<T>> lst = new List<OrderedSequence<T>>();
                    foreach (OrderedSequence<T> child in _children)
                        lst.Add(child.Clone());
                    return new OrderedSequence<T>(lst);
                }
            }
        }

        #endregion

        #endregion

        #region 1.9.1 GroupBy (bis)

        #region IGrouping interface

        /// <summary>
        /// Represents a grouping of a key element and a sequence of value elements.
        /// </summary>
        /// <typeparam name="K">Type of the key element.</typeparam>
        /// <typeparam name="T">Type of the value sequence elements.</typeparam>
        public interface IGrouping<K, T> : IEnumerable<T>
        {
            /// <summary>
            /// Gets the key of the grouping.
            /// </summary>
            K Key { get; }
        }

        #endregion

        #region IGrouping implementation

        /// <summary>
        /// Implementation of the <see cref="BdsSoft.Query.IGrouping&lt;K, T&gt;">IGrouping</see> interface. Uses a List&lt;<typeparamref name="T">T</typeparamref>&gt; to store the values corresponding to the grouping key.
        /// </summary>
        /// <typeparam name="K">Type of the key element.</typeparam>
        /// <typeparam name="T">Type of the value sequence elements.</typeparam>
        public class Grouping<K, T> : List<T>, IGrouping<K, T>
        {
            /// <summary>
            /// Key of the grouping.
            /// </summary>
            private K _key;

            /// <summary>
            /// Creates a new grouping for the specified key.
            /// </summary>
            /// <param name="key">Key of the grouping.</param>
            internal Grouping(K key)
            {
                _key = key;
            }

            /// <summary>
            /// Gets the key of the grouping.
            /// </summary>
            public K Key
            {
                get { return _key; }
            }
        }

        #endregion

        #endregion

        #region 1.11.5 ToLookup (bis)

        #region Lookup class

        /// <summary>
        /// Implements a one-to-many dictionary that maps keys to sequences of values.
        /// </summary>
        /// <typeparam name="K">Type of the keys.</typeparam>
        /// <typeparam name="T">Type of the values.</typeparam>
        public class Lookup<K, T> : IEnumerable<IGrouping<K, T>>
        {
            /// <summary>
            /// Dictionary mapping key values on an <c>IGrouping&lt;K,T&gt;</c> object containing a sequence of values.
            /// </summary>
            internal Dictionary<K, IGrouping<K, T>> dictionary;

            /// <summary>
            /// Keeps an ordered list of the keys, in order of addition.
            /// </summary>
            internal List<K> keys;

            /// <summary>
            /// Initializes a new one-to-many dictionary using the given <paramref name="comparer">comparer</paramref> for key comparison.
            /// </summary>
            /// <param name="comparer">Comparer used to compare keys when storing elements in the one-to-many dictionary.</param>
            internal Lookup(IEqualityComparer<K> comparer)
            {
                dictionary = new Dictionary<K, IGrouping<K, T>>(comparer);
                keys = new List<K>();
            }

            /// <summary>
            /// Gets the number of <c>IGrouping&lt;K,T&gt;</c> objects stored in the one-to-many dictionary.
            /// </summary>
            public int Count
            {
                get { return dictionary.Count; }
            }

            /// <summary>
            /// Retrieves a sequence of values corresponding to the given key.
            /// </summary>
            /// <param name="key">Key to return the value sequence for.</param>
            /// <returns>Value sequence corresponding to the given key.</returns>
            public IEnumerable<T> this[K key]
            {
                get { return dictionary[key]; }
            }

            /// <summary>
            /// Checks whether the one-to-many dictionary contains the specified key.
            /// </summary>
            /// <param name="key">Key to check for in the one-to-many dictionary.</param>
            /// <returns>True if the key was found in the one-to-many dictionary, false otherwise.</returns>
            public bool Contains(K key)
            {
                return dictionary.ContainsKey(key);
            }

            /// <summary>
            /// Retrieves the generic enumerator used to enumerate the elements in the one-to-many dictionary.
            /// </summary>
            /// <returns>Enumerator yielding the <c>IGrouping&lt;K,T&gt;</c> objects stored in the one-to-many dictionary in the order of addition.</returns>
            public IEnumerator<IGrouping<K, T>> GetEnumerator()
            {
                foreach (K key in keys)
                    yield return dictionary[key];
            }

            /// <summary>
            /// Retrieves the non-generic enumerator used to enumerate the elements in the one-to-many dictionary.
            /// </summary>
            /// <returns>Enumerator yielding the <c>IGrouping&lt;K,T&gt;</c> objects stored in the one-to-many dictionary in the order of addition.</returns>
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            /// <summary>
            /// Adds an <c>IGrouping&lt;K,T&gt;</c> object to the one-to-many dictionary based on its key.
            /// </summary>
            /// <param name="item"><c>IGrouping&lt;K,T&gt;</c> object to be added to the one-to-many dictionary.</param>
            internal void Add(IGrouping<K, T> item)
            {
                K key = item.Key;
                keys.Add(key);
                dictionary.Add(key, item);
            }
        }

        #endregion

        #endregion

        #region Helpers

        /// <summary>
        /// Reverses the comparison of elements to result in a descending order.
        /// </summary>
        /// <typeparam name="T">Type of the elements to compare.</typeparam>
        internal class ReverseComparer<T> : IComparer<T>
        {
            /// <summary>
            /// Original comparer that will be reversed by this ReverseComparer.
            /// </summary>
            private IComparer<T> _comparer;

            /// <summary>
            /// Creates a new reverse comparer based on a given comparer.
            /// </summary>
            /// <param name="comparer">Comparer to reverse the results from.</param>
            public ReverseComparer(IComparer<T> comparer)
            {
                _comparer = comparer;
            }

            /// <summary>
            /// Compares two objects x and y in reverse order as the original comparer.
            /// </summary>
            /// <param name="x">First object to be compared.</param>
            /// <param name="y">Second object to be compared.</param>
            /// <returns>If x &lt; y according to the original comparer, a positve value is returned. If x &gt; y according to the original comparer, a negative value is returned. If x and y are equal according to the original comparer, 0 is returned.</returns>
            public int Compare(T x, T y)
            {
                if (_comparer != null)
                    return _comparer.Compare(y, x);
                else if (x != null && x is IComparable)
                    return -(x as IComparable).CompareTo(y);
                else if (y != null && y is IComparable)
                    return (y as IComparable).CompareTo(x);
                else
                    throw new ArgumentException();
            }
        }

        #endregion
    }
}
