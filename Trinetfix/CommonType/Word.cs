namespace CommonType
{
    public class Word
    {
        public string Location { get; private set; }
        public string Text { get; private set; }
        public WordPosition Position { get; private set; }

        public Word(string location, string word, int row, int column)
        {
            Location = location;
            Text = word;
            Position = new WordPosition(row, column);
        }
    }
}