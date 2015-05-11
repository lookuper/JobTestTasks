namespace CommonType
{
    public class WordPosition
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        public WordPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}