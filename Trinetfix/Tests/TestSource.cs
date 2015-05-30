using CommonType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class TestSource : ISource
    {
        private IEnumerable<ScanResult> _scannedFiles;

        public string Destination { get; }
        public string Filter { get; }

        public IEnumerable<ScanResult> GetScannedFiles()
        {
            return _scannedFiles ?? (_scannedFiles = Scan());
        }

        public IEnumerable<ScanResult> Scan()
        {
            _scannedFiles = new List<ScanResult>
            {
                new TestScanResult { Name="t1.txt", Location="txt file", Created=DateTime.Now },
            };

            return _scannedFiles;
        }

        public IEnumerable<Word> WordsInFile(string fileName, string regExp = null)
        {
            var searchItem = GetScannedFiles()
                .FirstOrDefault(f => f.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase));

            return searchItem.GetWords(null, regExp);
        }

        public IEnumerable<WordPosition> WordPositionsInFile(string fileName, string word)
        {
            var words = WordsInFile(fileName);
            var positions = words.Where(w => w.Text.Equals(word, StringComparison.OrdinalIgnoreCase))
                .Select(w => w.Position)
                .ToList();

            return positions;
        }

        public void Dispose()
        {
        }
    }
}
