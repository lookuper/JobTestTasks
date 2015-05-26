using CommonType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.FolderScanner
{
    public class FolderScanner : ISource
    {
        private IEnumerable<ScanResult> _scanResults;

        public Encoding CurrentEncoding { get; private set; }
        public string Destination { get; private set; }
        public string Filter { get; private set; }

        private IEnumerable<ScanResult> Scan(string destination, string filter)
        {
            if (String.IsNullOrEmpty(destination))
                throw new ArgumentException("destination");

            if (String.IsNullOrEmpty(filter))
                filter = "*.txt";

            Destination = destination;
            Filter = filter;

            if (!Directory.Exists(Destination))
                throw new DirectoryNotFoundException(Destination);

            var files = Directory.GetFiles(Destination, Filter)
                .Select(x => new FileInfo(x))
                .Select(x => new FileScanResult(x.Name, x.FullName))
                .ToList();

            return files;
        }

        public IEnumerable<ScanResult> Scan()
        {
            var scan = Scan(Destination, Filter);
            return scan;
        }

        public FolderScanner(string destination, string filter, Encoding encoding = null)
        {
            CurrentEncoding = encoding ?? Encoding.Default;
            _scanResults = Scan(destination, filter);
        }

        public IEnumerable<ScanResult> GetScannedFiles()
        {
            return _scanResults;
        }

        public IEnumerable<CommonType.Word> WordsInFile(string fileName, string regExp = null)
        {
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentException("fileName");

            var searchItem = GetScannedFiles()
                .FirstOrDefault(f => f.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase));

            if (searchItem == null)
                throw new FileNotFoundException(fileName);

            return searchItem.GetWords(CurrentEncoding, regExp);
        }

        public IEnumerable<WordPosition> WordPositionsInFile(string fileName, string word)
        {
            if (String.IsNullOrEmpty(word))
                throw new ArgumentException("word");

            var words = WordsInFile(fileName);

            var positions = words.Where(w => w.Text.Equals(word, StringComparison.OrdinalIgnoreCase))
                .Select(w => w.Position)
                .ToList();

            return positions;
        }

        public IEnumerable<WordPosition> WordPositionsInFile(ScanResult file, string word)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            return WordPositionsInFile(file.Name, word);
        }

        public string GetContent(string fileName)
        {
            var res = GetScannedFiles().FirstOrDefault(f => f.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase));

            if (res == null)
                throw new FileNotFoundException(fileName);

            return res.GetContent(CurrentEncoding);
        }

        public void Dispose()
        {
            _scanResults = null;
        }
    }
}
