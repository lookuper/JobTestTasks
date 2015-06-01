using CommonType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class TestStorage : IStorage
    {
        private IEnumerable<ScanResult> _scannedFiles;

        public IEnumerable<ScanResult> GetScannedFiles()
        {
            return _scannedFiles ?? (_scannedFiles = Scan());
        }

        private IEnumerable<ScanResult> Scan()
        {
            _scannedFiles = new List<ScanResult>
            {
                new TestScanResult { Name="t2.txt", Location="txt file", Created=DateTime.Now },
            };

            return _scannedFiles;
        }

        public void Sync(IEnumerable<ScanResult> scanResult)
        {
            var local = _scannedFiles.ToList();
            var remove = local.Where(l => scanResult.Contains(l));
            local.RemoveAll(x => remove.Contains(x));

            local.AddRange(scanResult);
            _scannedFiles = local;
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
