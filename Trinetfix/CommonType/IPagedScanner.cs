using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonType
{
    public interface IPagedScanner : IDisposable
    {
        /// <summary>
        /// Get all scanned from all avaliable source and storages
        /// </summary>
        PagingCollection<ScanResult> GetScannedFiles();

        /// <summary>
        /// Get all words from file, spliting by regexp, default: "\w+[^\s]*\w+|\w"
        /// </summary>
        PagingCollection<Word> WordsInFile(string fileName, string regExp = null);

        /// <summary>
        /// Get all word positions from file
        /// </summary>
        PagingCollection<WordPosition> WordPositionsInFile(string fileName, string word);
    }
}
