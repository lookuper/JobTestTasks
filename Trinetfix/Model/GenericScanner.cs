using CommonType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GenericScanner : IPagedScanner
    {
        private readonly ISource _source;
        private readonly IStorage _storage;
        public int PageSize { get; private set; }

        public bool SourceAvaliable { get { return _source != null; } }
        public bool StorageAvaliable { get { return _storage != null; } }

        public GenericScanner(ISource source, IStorage storage, int pageSize)
        {
            _source = source;
            _storage = storage;

            if (pageSize <= 0)
                throw new ArgumentException("pageSize should more than 0");

            PageSize = pageSize;
        }

        /// <summary>
        /// Sync file and words from source to storate, long running blocking operation.
        /// </summary>
        public void SyncSourceWithStorage()
        {
            if (!SourceAvaliable)
                throw new ArgumentNullException("Source");

            if (!StorageAvaliable)
                throw new ArgumentNullException("Storage");

            var sourceFiles = _source.GetScannedFiles();
            _storage.Sync(sourceFiles);
        }

        /// <summary>
        /// Get all scanned files from all avaliable source and storage, may have duplicates
        /// </summary>
        public virtual PagingCollection<ScanResult> GetScannedFiles()
        {
            IEnumerable<ScanResult> sourceFiles = new List<ScanResult>();
            IEnumerable<ScanResult> storageFiles = new List<ScanResult>();

            if (SourceAvaliable)
            {
                sourceFiles = _source.Scan();
            }

            if (StorageAvaliable)
            {
                storageFiles = _storage.GetScannedFiles();
            }

            var resultList = new List<ScanResult>();

            resultList.AddRange(sourceFiles);
            resultList.AddRange(storageFiles);

            return new PagingCollection<ScanResult>(resultList, PageSize);
        }

        /// <summary>
        /// Return word with position from data source with latest data in.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="word"></param>
        public virtual PagingCollection<WordPosition> WordPositionsInFile(string fileName, string word)
        {
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentException("fileName");

            if (String.IsNullOrEmpty(word))
                throw new ArgumentException("word");

            var scannerWithLatestData = DetermineScannerWithLatestData(fileName);
            var wordPositions = scannerWithLatestData.WordPositionsInFile(fileName, word);

            return new PagingCollection<WordPosition>(wordPositions, PageSize);
        }

        /// <summary>
        /// Return list of words from file in storage with latest data.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="regExp"></param>
        /// <returns></returns>
        public virtual PagingCollection<Word> WordsInFile(string fileName, string regExp = null)
        {
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentException("fileName");

            var scannerWithLatestData = DetermineScannerWithLatestData(fileName);
            var words = scannerWithLatestData.WordsInFile(fileName, regExp);

            return new PagingCollection<Word>(words, PageSize);
        }

        /// <summary>
        /// Return data source with latest information depending to file name;
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected IScanner DetermineScannerWithLatestData(string fileName)
        {
            if (!SourceAvaliable && !StorageAvaliable)
                throw new ArgumentNullException("Both source and storage in null");

            if (SourceAvaliable && StorageAvaliable)
            {
                var localFile = _source.GetScannedFiles()
                    .Where(f => f.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(f => f.Created)
                    .LastOrDefault();

                var dbFile = _storage.GetScannedFiles()
                    .Where(f => f.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(f => f.Created)
                    .LastOrDefault();

                if (localFile == null && dbFile == null)
                    throw new ArgumentException(String.Format("{0} file is not found nor in database nor on disk", fileName));

                if (localFile != null && dbFile != null)
                {
                    var scannerWithLatestFile = new[] { localFile, dbFile }.Max(d => d.Created);
                    return localFile.Created == scannerWithLatestFile ? _source as IScanner : _storage as IScanner;
                }

                return localFile != null ? _source as IScanner : _storage as IScanner;
            }
            else
                return _source as IScanner ?? _storage as IScanner;
        }

        public void Dispose()
        {
            using (_source) ;
            using (_storage) ;
        }
    }
}
