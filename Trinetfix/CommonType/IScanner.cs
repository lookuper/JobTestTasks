using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonType
{
    public interface IScanner : IDisposable
    {
        IEnumerable<ScanResult> GetScannedFiles();
        IEnumerable<Word> WordsInFile(string fileName, string regExp = null);
        IEnumerable<WordPosition> WordPositionsInFile(string fileName, string word);
    }
}
