using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonType
{
    [Serializable]
    public abstract class ScanResult
    {
        public virtual string Name { get; set; }
        public virtual string Location { get; set; }
        public abstract long Size { get; set; }
        public abstract DateTime Created { get; set; }

        protected ScanResult(string name, string location)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("name");

            if (String.IsNullOrEmpty(location))
                throw new ArgumentException("location");

            Name = name;
            Location = location;
        }

        public abstract string GetContent(Encoding encoding);
        public abstract IEnumerable<Word> GetWords(Encoding encoding = null, string regExp = null);

        /// <summary>
        /// Split source string into words sequence with word position index
        /// </summary>
        /// <param name="source">source string</param>
        /// <param name="regExp">regular expression for determig word, by default: "\w+[^\s]*\w+|\w"</param>
        /// <returns></returns>
        protected virtual IEnumerable<Tuple<string, int>> SplitIntoWordsWithIndex(string source, string regExp = null)
        {
            if (String.IsNullOrEmpty(source))
                throw new ArgumentException("source");

            if (String.IsNullOrEmpty(regExp))
                regExp = @"\w+[^\s]*\w+|\w";

            var words = Regex.Matches(source, regExp)
                .Cast<Match>()
                .Select(m => new Tuple<string, int>(m.Value, m.Index))
                .ToList();

            return words;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var second = obj as ScanResult;
            if (second == null)
                return false;

            return Name == second.Name &&
                   Location == second.Location &&
                   Created == second.Created;
        }
    }
}
