using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DefShef.TicTacToe;

namespace DefShef
{
    public static class Extensions
    {
        #region Warmup Extensions

        /// <summary>
        /// Simple extension method replacement for Enumerable.Range(int, int)
        /// </summary>
        /// <param name="start">
        /// First digit of sequence
        /// </param>
        /// <param name="end">
        /// Last digit of sequence
        /// </param>
        /// <returns>
        /// A sequence of consecutive integers running from start to end
        /// </returns>
        /// <remarks>
        /// I don't particularly like the Enumerable.Range() syntax, so I wrote a simple extension 
        /// method that accomplishes the same result (a lazily evaluated IEnumerable&lt;int&gt;) 
        /// </remarks>
        public static IEnumerable<int> UpTo(this int start, int end)
        {
            while (start <= end)
            {
                yield return start++;
            }
        }

        /// <summary>
        /// Simple extension method that tests the divisibility of one integer by another
        /// </summary>
        /// <param name="dividend">
        /// The dividend of the modulus operation
        /// </param>
        /// <param name="divisor">
        /// The divisor of the modulus operation
        /// </param>
        /// <returns>
        /// <code>System.Boolean.True</code> if dividend is divisible by divisor, <code>System.Boolean.False</code> otherwise
        /// </returns>
        /// <remarks>
        /// Again, just a simple extension method that can be used to represent the standard divisibility test in C#,
        /// it just looks ugly! :)
        /// </remarks>
        public static bool IsDivisibleBy(this int dividend, int divisor)
        {
            return dividend % divisor == 0;
        }

        /// <summary>
        /// Simple extension method to perform the dot product operation on a vector 
        /// of integers
        /// </summary>
        /// <param name="vector">
        /// A collection of integers
        /// </param>
        /// <returns>
        /// The dot product of the values in the vector
        /// </returns>
        /// <remarks>
        /// The first non-obvious extension method. It uses the IEnumerable&lt;int&gt;.Aggregate(int, 
        /// Func&lt;int, int, int&gt;) extension method to multiply each value in the collection 
        /// with the running multiplicand of previous values in the collection. Note that 
        /// .Aggregate() must be passed an initial value that is passed to the function with the 
        /// first item in the collection
        /// </remarks> 
        public static int DotProduct(this IEnumerable<int> vector)
        {
            return vector.Aggregate(1, (seed, value) => seed * value);
        }

        /// <summary>
        /// A simple extension method to return the nth element of a collection
        /// </summary>
        /// <typeparam name="T">
        /// The type of the items in the collection
        /// </typeparam>
        /// <param name="values">
        /// The collection to be operated on
        /// </param>
        /// <param name="n">
        /// The 1-based position of the element to be extracted from the collection
        /// </param>
        /// <returns>
        /// A single element of the collection
        /// </returns>
        /// <remarks>
        /// Ordinarily, I'd have made this method 0-based, as collections in C# are 0-based,
        /// but the example on the DefShef.GitHub.IO website expected to deal with a 1-based 
        /// collection... ;)
        /// 
        /// It also marks my first use of the .First() method! The First method is the 
        /// same thing as head in some other functional languages and simply returns the
        /// very first item in any given collection. It has a corresponding partner, .Last()
        /// that does the same thing as tail and returns the last item in any given collection.
        /// Both of these methods will throw an InvalidOperationException if their
        /// collections are empty, so be careful!
        /// 
        /// As an aside, it's worth noting that there are also .FirstOrDefault() and 
        /// .LastOrDefault() that do the same things as .First() and .Last() respectively, but 
        /// will return the default value for the collection's given type in the event that the 
        /// collection is empty. Just remember that value types' default values are specified
        /// in C# (e.g. 0 for numerical types, string.Empty for a string and so on), but instance 
        /// types' typically default to null! This may sound annoying, but can be a blessing or 
        /// a curse, depending on your situation
        /// </remarks> 
        public static T GetNthItem<T>(this IEnumerable<T> values, int n)
        {
            return values.Skip(n - 1).First();
        }

        #endregion

        #region Beginner Extensions

        /// <summary>
        /// A simple extension method to extract a particular contiguous subset of a collection
        /// </summary>
        /// <typeparam name="T">
        /// The type of the items in the collection
        /// </typeparam>
        /// <param name="values">
        /// The collection to be operated on
        /// </param>
        /// <param name="skip">
        /// The number of elements to skip past before accepting the first element of the desired
        /// subset
        /// </param>
        /// <param name="take">
        /// The number of elements to take from the collection
        /// </param>
        /// <returns>
        /// A subset of items from the input collection
        /// </returns>
        /// <remarks>
        /// Another simple extension method to wrap calls to IEnumerable&lt;T&gt;.Skip(int) and
        /// IEnumerable&lt;T&gt;.Take(int) that replicates the functionality of the Slice method
        /// in other functional languages
        /// </remarks>
        public static IEnumerable<T> Slice<T>(this IEnumerable<T> values, int skip, int take)
        {
            return values.Skip(skip).Take(take);
        }

        /// <summary>
        /// A naive recursive implementation of the Fibonacci Number Generator
        /// </summary>
        /// <param name="n">
        /// The number of the term to be returned
        /// </param>
        /// <returns>
        /// The nth Fibonacci number
        /// </returns>
        /// <remarks>
        /// At last, the first recursive functional method! Just don't ask for a term higher 
        /// than about 44!
        /// </remarks> 
        public static int Fibonacci(this int n)
        {
            return n <= 2 ? 1 : Fibonacci(n - 2) + Fibonacci(n - 1);
        }

        /// <summary>
        /// A straightforward, if suboptimal, memoization routine
        /// </summary>
        /// <typeparam name="T">
        /// The type of the input to the function to be memoized
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the output of the function to be memoized
        /// </typeparam>
        /// <param name="function">
        /// The function to be memoized
        /// </param>
        /// <returns>
        /// The same output as the original function
        /// </returns>
        /// <remarks>
        /// Nice and simple implementation of memoization in C#. 
        /// 
        /// I only say it's suboptimal because it relies on a C# Dictionary collection to store 
        /// the values in. Since Dictionaries in C# are implemented under the bonnet as a hash 
        /// table, the lookup speed is high, close to O(1), but they obviously cost more to 
        /// insert each value into, and also have a higher memory footprint due to the need to 
        /// store the hash map as well as the values to be stored themselves.
        /// 
        /// For most purposes, this is absolutely fine, and has the added benefit of being able
        /// to memoize any object passed to it since all objects in C# *must* inherit from 
        /// System.Object which implements a (simple) .GetHashCode() method.
        /// 
        /// However, better performance can be attained by customizing the memoization based on its 
        /// intended use. For example, in our case we could store the Fibonacci numbers in an array
        /// of integers of maximum capacity equal to the term requested + 1, using the term value
        /// to select the index of the value in the array, and modifying the Fibonacci function 
        /// to return 0 when the 0th term is requested. This has O(1) lookup and insert speed, and 
        /// minimal memory footprint.
        /// </remarks>
        public static Func<T, TResult> Memoize<T, TResult>(this Func<T, TResult> function)
        {
            Dictionary<T, TResult> cache = new Dictionary<T, TResult>();
            return argument =>
            {
                TResult result;
                if (cache.TryGetValue(argument, out result))
                {
                    return result;
                }
                result = function(argument);
                cache.Add(argument, result);
                return result;
            };
        }

        #endregion

        #region Experienced Extensions

        /// <summary>
        /// Extension method that accepts a nested collection of integers as objects, and flattens 
        /// it into a single collection of integers as objects
        /// </summary>
        /// <param name="input">
        /// An object that can actually be a single integer, or a collection of integers, or any 
        /// combination of integers and collections of integers, with any* level of nesting
        /// </param>
        /// <returns>
        /// A flat (non-nested) collection of integers as objects
        /// </returns>
        /// <remarks>
        /// Ugh. Dealing with objects that might be one of several types is pretty nasty in C#, as you will see.
        /// 
        /// Note the asterisk in the input parameter description above. Since this is a fairly complex recursive 
        /// function, it's pretty likely that the compiler will be unable to apply any tail-call optimization to 
        /// it, so a stack overflow exception will occur if you passed a collection with sufficient levels of 
        /// nesting in it.
        /// 
        /// Also, the function only handles integers and collections of integers in its current format. Either 
        /// further cases would be needed to handle each type you want it to handle, or it would need refactoring 
        /// to work out out of each item is actually a collection and recurse for that item's contents and just 
        /// add the object if it isn't a collection. Hmm...
        /// </remarks>
        public static IEnumerable<object> Flatten(this object input)
        {
            object[] inputAsObjectArray = input as object[];

            if (inputAsObjectArray == null)
            {
                throw new ArgumentException("input must contain at least one object", "input");
            }

            List<object> output = new List<object>();

            foreach (object obj in inputAsObjectArray)
            {
                if (obj is int)
                {
                    output.Add((int)obj);
                    continue;
                }

                int[] objAsIntArray = obj as int[];

                if (objAsIntArray != null)
                {
                    output.AddRange(objAsIntArray.Cast<object>());
                    continue;
                }

                output.AddRange(obj.Flatten());
            }

            return output;
        }

        /// <summary>
        /// Extension method to handle nested collections of objects, finding objects of a 
        /// given type and value and replacing them with another object
        /// </summary>
        /// <param name="input">
        /// An object that can actually be a single object, or a collection of objects, or any 
        /// combination of integers and collections of objects, with any level of nesting
        /// </param>
        /// <param name="find">The object to be found</param>
        /// <param name="replace">The object to replace found instances with</param>
        /// <returns>
        /// An object with exactly the same relative structure that input had, but where all instances
        /// of the object to be found has been replaced by the object it should be replaced by
        /// </returns>
        /// <remarks>
        /// That's better, let's work out if what we're looking for is the right type dynamically and switch 
        /// it with another object. 
        /// 
        /// All the objects can be different types than each other, even the type of the object to find can 
        /// be different to that of the object it should be replaced with! Now that's /// a proper mindf**k 
        /// in a strongly-typed language :D
        /// 
        /// Two things of note:
        /// 1) The performance hit of having to call on Reflection to work out the type of each object as we 
        ///    go. That would soon add up in real-world data sets!
        /// 2) The function would work best with the objects to find and replace being value-typed (strings,
        ///    integers, etc.).
        /// 
        ///    Using something that's instance-typed for the object to find would only match if it found a 
        ///    direct reference match (why would you do this? why??), or if the underlying type has an Equals 
        ///    method of the correct signature (usually by implementing IEquatable for the given type) and 
        ///    that method correctly performs equality testing via property comparisons.
        /// 
        ///    Alternatively, using something that's instance-typed for the object to replace would result 
        ///    in the output collection containing one or more references to the exact same object it was 
        ///    passed. Again this probably unlikely to be the desired outcome, and would be likely to cause 
        ///    repercussions in terms of the lifetime of the replace object, weird bugs if something outside
        ///    the scope of this function attempted to dispose of the replace object, etc. Cloning code to 
        ///    handle that use case would be a good idea. Or just don't do it!
        /// 
        ///    Unfortunately, I'm not aware of anything in C# that would allow you to dictate ahead of time 
        ///    that the objects allowed must be value-typed, other than additional boilerplate code to work 
        ///    it out and bail early with an ArgumentException :(
        /// </remarks>
        public static IEnumerable<object> NestedReplace(this object input, object find, object replace)
        {
            object[] objAsObjectArray = input as object[];

            if (objAsObjectArray == null)
            {
                throw new ArgumentException("input must contain at least one element", "input");
            }

            List<object> output = new List<object>();

            foreach (object obj in objAsObjectArray)
            {
                if (obj.GetType() == find.GetType())
                {
                    Type objType = obj.GetType();
                    MethodInfo equalsMethodInfo = objType.GetMethod("Equals", new[] { objType });
                    if ((bool)equalsMethodInfo.Invoke(obj, new[] { find }))
                    {
                        output.Add(replace);
                    }
                    else
                    {
                        output.Add(obj);
                    }
                    continue;
                }

                output.Add(obj.NestedReplace(find, replace));
            }

            return output;
        }

        /// <summary>
        /// Extension method to handle nested collections of objects, applying a filter function to 
        /// valid elements inside the nested collection
        /// </summary>
        /// <param name="input">
        /// An object that can actually be a single object, or a collection of objects, or any 
        /// combination of objects and collections of objects, with any level of nesting
        /// </param>
        /// <param name="type">
        /// The type of the input to the filter function
        /// </param>
        /// <param name="filter">
        /// The filter function being applied to elements of the collection
        /// </param>
        /// <returns>
        /// An object with exactly the same relative structure that input had, but where all instances
        /// of objects that do not meet the filter function's criteria have been excluded
        /// </returns>
        /// <remarks>
        /// This time, we have to specify at compile-time what the type is of the object that should be 
        /// passed into the filter function, because otherwise you could attempt to pass in objects of the 
        /// wrong type and cause the filter function to fail.
        /// </remarks>
        public static IEnumerable<object> DeepFilter(this object input, Type type, Func<object, bool> filter)
        {
            object[] objAsObjectArray = input as object[];

            if (objAsObjectArray == null)
            {
                throw new ArgumentException("input must contain at least one element", "input");
            }

            List<object> output = new List<object>();

            foreach (object obj in objAsObjectArray)
            {
                if (obj.GetType() == type)
                {
                    if (filter(obj))
                    {
                        output.Add(obj);
                    }
                }
                else
                {
                    output.Add(obj.DeepFilter(type, filter));
                }
            }

            return output.ToArray();
        }

        /// <summary>
        /// Extension method to handle nested collections of objects, applying a transformative function to 
        /// valid elements inside the nested collection
        /// </summary>
        /// <param name="input">
        /// An object that can actually be a single object, or a collection of objects, or any 
        /// combination of objects and collections of objects, with any level of nesting
        /// </param>
        /// <param name="type">
        /// The type of the input to the transformative function
        /// </param>
        /// <param name="mapFunc">
        /// The transformative function being applied to elements of the collection
        /// </param>
        /// <returns>
        /// An object with exactly the same relative structure that input had, but where all instances
        /// of valid objects have been transformed as defined
        /// </returns>
        /// <remarks>
        /// Again, due to complications of not knowing ahead of time what transformative function you might
        /// supply, this extension requires you to specify at compile time what you expect to be handling in 
        /// the transformative function. After all, (string)a * (string)a is undefined in C#, so would cause
        /// a compile-time error since the compiler would not be able to find a valid overload of the * operator
        /// with those input types.
        /// 
        /// This is also the first appearance of the .Select() method, which allows you to apply a transformation
        /// function to every element in a collection, the same as map in other functional languages.
        /// 
        /// In this instance, we're only using it's basic form that applies the given function to each object.
        /// However there is a second overload that will allow you to have access to both the item from the 
        /// collection as well as its index in the collection.
        /// </remarks>
        public static IEnumerable<object> DeepMap(this object input, Type type, Func<object, object> mapFunc)
        {
            object[] objAsObjectArray = input as object[];

            if (objAsObjectArray == null)
            {
                throw new ArgumentException("input must contain at least one element", "input");
            }

            return objAsObjectArray.Select(obj => obj.GetType() == type 
                                                      ? mapFunc(obj) 
                                                      : obj.DeepMap(type, mapFunc))
                                   .ToArray();
        }

        /// <summary>
        /// Extension method to handle nested collections of objects, applying an aggregating function to 
        /// valid elements inside the nested collection
        /// </summary>
        /// <param name="input">
        /// An object that can actually be a single object, or a collection of objects, or any 
        /// combination of objects and collections of objects, with any level of nesting
        /// </param>
        /// <returns>
        /// The result of the aggregating function over all valid elements in input
        /// </returns>
        /// <remarks>
        /// Here, I've simplified the function to only expect to handle integers or nested collections of 
        /// integers. Anything more complex gets messy and buggy really fast!
        /// 
        /// Hopefully, you can see how it parallels the DotProduct extension defined above though.
        /// </remarks>
        public static int DeepReduce(this object input)
        {
            object[] objAsObjectArray = input as object[];

            if (objAsObjectArray == null)
            {
                throw new ArgumentException("input must contain at least one element", "input");
            }

            return objAsObjectArray.Aggregate(0, (seed, value) => seed + (value is int ? (int)value : value.DeepReduce()));
        }

        #endregion
 
        #region Expert Extensions

        /// <summary>
        /// Extension method that determines which value from the Side enum a given string should be
        /// </summary>
        /// <param name="side">
        /// The string to be tested
        /// </param>
        /// <returns>
        /// The corresponding value from the Side enum
        /// </returns>
        /// <remarks>
        /// I made two implementation calls here: 
        /// 1) Since C# doesn't seem to let you define additional static methods for an enum, I 
        ///    chose to implement this method as a helper extension method.
        /// 2) I'm arbitrarily saying "x" and "o" are valid inputs but any other string will be
        ///    treated as an empty space
        /// </remarks> 
        public static Side MakeSide(this string side)
        {
            switch (side)
            {
                case "x":
                    return Side.Cross;
                case "o":
                    return Side.Naught;
                default:
                    return Side.Empty;
            }
        }

        /// <summary>
        /// Extension method that determines if there is a distinct winner for a given 
        /// tic-tac-toe board
        /// </summary>
        /// <param name="board">
        /// The tic-tac-toe board to be tested
        /// </param>
        /// <returns>
        /// If crosses have won, <c>Side.Cross</c> is returned. If naughts have won, 
        /// <c>Side.Cross</c> is returned. In any other case, <c>Side.Empty</c> is returned to
        /// indicate no winner.
        /// </returns>
        /// <remarks>
        /// It will probably help to look at the classes in the DefShef.TicTacToe namespace before
        /// reading the rest of this remark... ;)
        /// 
        /// Note that I've purposefully left the various parts of the algorithm in a more separated-out
        /// structure to aid comprehension. Normally, I'd just inline as much as possible to reduce 
        /// the amount of space the code takes up, but then I'm used to reading LINQ :)
        /// 
        /// Given that the tic-tac-toe board is a square that is n squares high and n squares wide, my 
        /// algorithm for determining a winner is simple:
        /// - For each valid full-length vector (i.e. each row, column and both corner-to-corner left- and 
        ///   right-diagonals in the given board
        /// - If the values of the vector are grouped by their value (Side.Cross/Side.Naught/Side.Empty)
        /// - And we ignore the group of positions that are empty
        /// - And one of the groups contains the same number of positions as is the length of the vector
        ///   (which by definition must be the same as the size of either the width or height of the board,
        ///   i.e. n)
        /// - Then that side is a winner of that board
        /// - And if no vector satisfies those rules, the board is still unclaimed
        ///  
        /// Ok, this is where LINQ tends to separate the strong from the weak...
        /// 
        /// LINQ supports the .GroupBy() method, which in its most basic form takes two inputs:
        /// 1) A collection
        /// 2) A function used to select the value to be used as the grouping key
        /// 
        /// The output of a GroupBy method is an IGrouping, which is basically a key value and an 
        /// associated sub-collection of elements from the input collection that meet the key selection 
        /// function's criteria.
        /// 
        /// In this case, we need to use the GroupBy method in a couple of ways.
        /// 
        /// Firstly, we need to use it to take the collection of positions from the board and group them
        /// by their assorted index properties. This builds our sets of vectors to test, i.e. the rows, 
        /// columns and diagonals.
        /// 
        /// Secondly, we need to use it to take the groupings that are our vectors and further group the
        /// values in each vector so that we can discard the group (if it exists) that corresponds to any
        /// empty squares in that vector, and test how many groupings of Side values we have. If there is
        /// only a single grouping of values in that vector, and its key value is not equal to Side.Empty,
        /// then that vector contains a winning line, so declare the grouping's key value as the winner.
        /// 
        /// With this approach, we handle the current case that tic-tac-toe is played on a 3x3 square board
        /// by two players. But we also support any other case of board size and player count, so long as 
        /// the winning criterion is for a single player to completely fill one of the full-length vectors 
        /// on the board. I like forward-compatibility! :D
        /// 
        /// The eagle-eyed will also spot some other new methods in use: 
        /// .Count():
        /// The Count method simply returns the number of elements that a collection contains. Note that 
        /// this is the *number of elements* and not the capacity of the collection!
        /// 
        /// .OrderByDescending(): 
        /// The OrderByDescending method, and its opposite partner, the .OrderBy() method, take a 
        /// collection and a selector function used to define what will be used to define the ordering 
        /// key for the operation. The basic form relies on the type of the collection to have a default 
        /// Comparer implementation (i.e. it implements the IComparer interface), but a second overload 
        /// allows you to supply your own Comparer method if you want to. The two methods simply differ in
        /// their choice of comparison value direction. OrderBy orders in ascending comparison order, and
        /// OrderByDescending orders in descending comparison order.
        /// 
        /// If you had more complex ordering needs and needed to order by one property and then by another
        /// property, there are the .ThenBy() and .ThenByDescending() methods that act in exactly the same 
        /// way as their OrderBy counterparts.
        /// 
        /// The algorithm below makes use of OrderByDescending and First together to perform a very simple 
        /// optimization:
        /// For a given n x n square of cells, there will be (2 * n) - 1 of both left diagonals and right 
        /// diagonals. However, our winning criteria above specifies that only a full-length vector counts.
        /// Let's examine the left diagonal indexes of the cells of a 3 x 3 board:
        /// 
        ///             3 4 5
        ///             2 3 4
        ///             1 2 3
        /// 
        /// It's patently obvious that only one index contains the maximum number of cells in its 
        /// corresponding vector. This means that we can simply order the diagonals in descending size 
        /// order and take the first vector for each collection of diagonals to get the two diagonals we
        /// are interested in. 
        /// </remarks>
        public static Side DeclareWinner(this Board board)
        {
            var rows = board.Positions.GroupBy(position => position.RowIndex)
                                      .Select(row => row.Select(position => position.Value));

            var columns = board.Positions.GroupBy(position => position.ColumnIndex)
                                         .Select(column => column.Select(position => position.Value));

            var leftDiagonal = board.Positions.GroupBy(position => position.LeftDiagonalIndex)
                                              .OrderByDescending(diagonal => diagonal.Count())
                                              .First()
                                              .Select(position => position.Value);

            var rightDiagonal = board.Positions.GroupBy(position => position.RightDiagonalIndex)
                                               .OrderByDescending(diagonal => diagonal.Count())
                                               .First()
                                               .Select(position => position.Value);

            foreach (var row in rows)
            {
                var rowAsWin = row.GroupBy(value => value)
                                  .ToArray();

                if (rowAsWin.Count() == 1 && rowAsWin.First().Key != Side.Empty)
                {
                    return rowAsWin.First().Key;
                }
            }

            foreach (var column in columns)
            {
                var columnAsWin = column.GroupBy(value => value)
                                        .ToArray();

                if (columnAsWin.Count() == 1 && columnAsWin.First().Key != Side.Empty)
                {
                    return columnAsWin.First().First();
                }
            }

            var leftDiagonalAsWin = leftDiagonal.GroupBy(value => value)
                                                .ToArray();
            if (leftDiagonalAsWin.Count() == 1 && leftDiagonalAsWin.First().Key != Side.Empty)
            {
                return leftDiagonalAsWin.First().Key;
            }

            var rightDiagonalAsWin = rightDiagonal.GroupBy(value => value)
                                                  .ToArray();

            if (rightDiagonalAsWin.Count() == 1 && rightDiagonalAsWin.First().Key != Side.Empty)
            {
                return rightDiagonalAsWin.First().Key;
            }

            return Side.Empty;
        }

        /// <summary>
        /// Extension method to act as DSL element "Cowering"
        /// </summary>
        /// <param name="n">
        /// The value to be "Cowering"'d
        /// </param>
        /// <returns>
        /// The "Cowering"'d value
        /// </returns>
        public static int Cowering(this int n)
        {
            return n * 2;
        }

        /// <summary>
        /// Extension method to act as DSL element "Burrito"
        /// </summary>
        /// <typeparam name="T">
        /// The type of the input to be "Burrito"'d
        /// </typeparam>
        /// <param name="n">
        /// The input to be "Burrito"'d
        /// </param>
        /// <param name="m">
        /// The number of times that the input will be "Burrito"'d
        /// </param>
        /// <returns>
        /// A collection of m copies of n
        /// </returns>
        public static T[] Burrito<T>(this T n, int m)
        {
            return 1.UpTo(m)
                    .Select(i => n)
                    .ToArray();
        }

        /// <summary>
        /// Extension method to act as DSL element "Tap"
        /// </summary>
        /// <param name="n">
        /// An integer to be "Tap"'d
        /// </param>
        /// <returns>
        /// The "Tap"'d integer
        /// </returns>
        public static int Tap(this int n)
        {
            return ++n;
        }

        /// <summary>
        /// Extension method to act as DSL element "Steel"
        /// </summary>
        /// <param name="n">
        /// An integer to be "Steel"'d
        /// </param>
        /// <returns>
        /// The "Steel"'d integer
        /// </returns>
        public static int Steel(this int n)
        {
            return --n;
        }

        /// <summary>
        /// Extension method to act as DSL element "Sheffield"
        /// </summary>
        /// <typeparam name="T">
        /// The type of the input to be "Sheffield"'d
        /// </typeparam>
        /// <param name="l">
        /// A collection of type T
        /// </param>
        /// <param name="n">
        /// An object of type T
        /// </param>
        /// <returns>
        /// The "Sheffield"'d collection
        /// </returns>
        public static T[] Sheffield<T>(this IEnumerable<T> l, T n)
        {
            return l.Concat(new[] { n })
                    .ToArray();
        }

        /// <summary>
        /// Extension method to act as DSL element "Meatspace"
        /// </summary>
        /// <typeparam name="T">
        /// The type of the collection to be "Meatspace"'d
        /// </typeparam>
        /// <param name="l">
        /// The collection to be "Meatspace"'d
        /// </param>
        /// <returns>
        /// The "Meatspace"'d collection element
        /// </returns>
        public static T Meatspace<T>(this IEnumerable<T> l)
        {
            return l.FirstOrDefault();
        }

        /// <summary>
        /// Extension method to act as DSL element "Geek"
        /// </summary>
        /// <typeparam name="T">
        /// The type of the collection to be "Geek"'d
        /// </typeparam>
        /// <param name="l1">
        /// The first collection to be "Geek"'d
        /// </param>
        /// <param name="l2">
        /// The second collection to be "Geek"'d
        /// </param>
        /// <returns>
        /// The "Geek"'d collection
        /// </returns>
        public static T[] Geek<T>(this IEnumerable<T> l1, IEnumerable<T> l2)
        {
            return l1.Concat(l2)
                     .ToArray();
        }

        #endregion
    }
}
