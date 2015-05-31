using System;
using CommonType;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class TestScanResult : ScanResult
    {
        public TestScanResult() : base("test name", "test location")
        {

        }

        protected TestScanResult(string name, string location) : base(name, location)
        {
        }

        public override DateTime Created { get; set; }
        public override long Size { get; set; }

        public override string GetContent(Encoding encoding)
        {
            return @"I'm sure those are not the right words, said poor Alice";
        }

        public override IEnumerable<Word> GetWords(Encoding encoding = null, string regExp = null)
        {
            var words = SplitIntoWordsWithIndex(GetContent(null), regExp)
                    .Select(w => new Word(Location, w.Item1, 1, w.Item2))
                    .ToList();

            return words;
        }
    }
}
