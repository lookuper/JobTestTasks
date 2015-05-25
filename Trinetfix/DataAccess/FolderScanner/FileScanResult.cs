using CommonType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.FolderScanner
{
    [Serializable]
    public sealed class FileScanResult : ScanResult
    {
        public override DateTime Created { get; set; }
        public override long Size { get; set; }

        public FileScanResult(string name, string location) : base(name, location)
        {
            var info = new FileInfo(location);
            if (!info.Exists)
                throw new FileNotFoundException(location);

            Size = info.Length;
            Created = info.CreationTime;
        }

        public override string GetContent(Encoding encoding)
        {
            return System.IO.File.ReadAllText(Location, encoding ?? Encoding.Default);
        }

        public override IEnumerable<CommonType.Word> GetWords(Encoding encoding, string regExp = null)
        {
            var allLines = System.IO.File.ReadAllLines(Location, encoding ?? Encoding.Default);
            var allWordsInFile = new List<CommonType.Word>();
            int lineNumber = 0;

            foreach (var line in allLines)
            {
                lineNumber++;

                if (String.IsNullOrEmpty(line))
                {
                    continue;
                }

                var words = SplitIntoWordsWithIndex(line, regExp)
                    .Select(w => new CommonType.Word(Location, w.Item1, lineNumber, w.Item2))
                    .ToList();

                allWordsInFile.AddRange(words);
            }

            return allWordsInFile;
        }
    }
}
