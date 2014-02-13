using System;
using System.Linq;

namespace DefShef.TicTacToe
{
    /// <summary>
    /// An immutable class that represents a given state of a tic-tac-toe board
    /// </summary>
    public class Board
    {
        /// <summary>
        /// The collection of Positions that the board consists of
        /// </summary>
        public Position[] Positions { get; private set; }

        /// <summary>
        /// Base private constructor for a Board
        /// </summary>
        /// <param name="positionCount">
        /// The number of Positions that the board must contain
        /// </param>
        /// <remarks>
        /// This constructor is the key to the algorithm inside the DefShef.Extensions.DeclareWinner
        /// extension method.
        /// 
        /// By assuming it will be dealing only with square boards, it uses the understanding that:
        /// - The rows' index values follow the pattern of: 
        ///   Position index / square root of positionCount, where / represents integer division
        /// - The columns' index values follow the pattern of: 
        ///   Position index modulo square root of positionCount
        /// - The left diagonals' index values follow the pattern of: 
        ///   square root of positionCount / 2 - row index of position + column index of position, again where / represents integer division
        /// - The right diagonals' index values follow the pattern of: 
        ///   (2 * square root of positionCount) - 1 - row index of position - column index of position
        /// </remarks>
        private Board(int positionCount)
        {
            int limit = (int)Math.Sqrt(positionCount);
            Positions = 0.UpTo(positionCount - 1)
                         .Select(i => new { RowIndex = i / limit, 
                                            ColumnIndex = i % limit })
                         .Select(position => new { position.RowIndex,
                                                   position.ColumnIndex,
                                                   LeftDiagonalIndex = limit / 2 - position.RowIndex + position.ColumnIndex,
                                                   RightDiagonalIndex = 2 * limit - 1 - position.RowIndex - position.ColumnIndex
                         })
                         .Select(position => new Position(position.RowIndex, 
                                                          position.ColumnIndex, 
                                                          position.LeftDiagonalIndex, 
                                                          position.RightDiagonalIndex))
                         .ToArray();
        }
        
        /// <summary>
        /// Private constructor for a Board that uses Board(int) to pre-populate the Board's 
        /// Positions, sans values
        /// </summary>
        /// <param name="givenPositions">
        /// An array of strings that define the state of the Board's Positions
        /// </param>
        /// <remarks>
        /// The .Join() method is used here to correlate each Position element in the Board with 
        /// its corresponding element from the supplied array of strings that define their states.
        /// 
        /// We make use of the Select method overload that provides access to both the elements 
        /// and their associated indexes of the two collections (Positions and givenPositions) to
        /// create anonymous types that hold both sets of information for each element.
        /// 
        /// Those anonymous types are then joined on their index values so that the various index 
        /// properties of each Position element and be tied to their corresponding state to form
        /// the final state of the Board.
        /// </remarks>
        private Board(string[] givenPositions)
            : this(givenPositions.Count())
        {
            Positions = Positions.Select((position, index) => new { Position = position, Index = index })
                                 .Join(givenPositions.Select((position, index) => new { Side = position.MakeSide(), Index = index }), 
                                       a => a.Index,
                                       b => b.Index,
                                       (a, b) => new Position(b.Side, a.Position.RowIndex, a.Position.ColumnIndex, a.Position.LeftDiagonalIndex, a.Position.RightDiagonalIndex))
                                 .ToArray();
        }

        /// <summary>
        /// Public constructor for a Board
        /// </summary>
        /// <param name="givenPositionSet">
        /// A string that contains a given board state, in the form:
        /// " ,o,x,x,o,o, ,x, "
        /// which corresponds to a board that looks like:
        ///     o x
        ///   x o o
        ///     x 
        /// </param>
        public Board(string givenPositionSet)
            : this(givenPositionSet.Split(new[] { ',' }, StringSplitOptions.None).ToArray()) { }
    }
}
