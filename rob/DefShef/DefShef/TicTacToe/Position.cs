namespace DefShef.TicTacToe
{
    /// <summary>
    /// An immutable struct to represent any given Position on a tic-tac-toe board
    /// </summary>
    public struct Position
    {
        /// <summary>
        /// The state of the position
        /// </summary>
        public Side Value { get; private set; }

        /// <summary>
        /// The row index of the position
        /// </summary>
        public int RowIndex { get; private set; }

        /// <summary>
        /// The column index of the position
        /// </summary>
        public int ColumnIndex { get; private set; }

        /// <summary>
        /// The left diagonal index of the position
        /// </summary>
        public int LeftDiagonalIndex { get; private set; }

        /// <summary>
        /// The right diagonal index of the position
        /// </summary>
        public int RightDiagonalIndex { get; private set; }

        /// <summary>
        /// Basic constructor for a Position
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="leftDiagonalIndex"></param>
        /// <param name="rightDiagonalIndex"></param>
        public Position(int rowIndex, 
                        int columnIndex,
                        int leftDiagonalIndex,
                        int rightDiagonalIndex) 
            : this()
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            LeftDiagonalIndex = leftDiagonalIndex;
            RightDiagonalIndex = rightDiagonalIndex;
        }

        /// <summary>
        /// Advanced constructor for a Position that also defines its state
        /// </summary>
        /// <param name="initialValue"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="leftDiagonalIndex"></param>
        /// <param name="rightDiagonalIndex"></param>
        public Position(Side initialValue,
                        int rowIndex,
                        int columnIndex,
                        int leftDiagonalIndex,
                        int rightDiagonalIndex)
            : this(rowIndex, 
                   columnIndex, 
                   leftDiagonalIndex, 
                   rightDiagonalIndex)
        {
            Value = initialValue;
        }
    }
}
