using CommonType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DbScanner
{
    public class DbScanner : IStorage
    {
        private IEnumerable<ScanResult> _scanResults;
        private readonly UnitOfWork _unit = new UnitOfWork();

        public DbScanner()
        {
            _scanResults = Scan(null, null);
        }

        private IEnumerable<ScanResult> Scan(string destination, string filter)
        {
            return _unit.FileRepository.GetQueryable()
                .Select(x => new DbScanResult
                {
                    Name = x.Name,
                    Location = x.Location,
                    Created = x.CreatedDate
                })
                .ToList()
                .Cast<ScanResult>()
                .ToList();
        }

        public string GetContent(string fileName)
        {
            var scannedFiles = GetScannedFiles();
            var content = scannedFiles.Select(x => x.GetContent(Encoding.Default));

            return String.Join(" ", content);
        }

        public IEnumerable<ScanResult> GetScannedFiles()
        {
            return _scanResults;
        }

        public IEnumerable<CommonType.Word> WordsInFile(ScanResult file, string regExp = null)
        {
            return WordsInFile(file.Name, regExp);
        }

        public void Sync(IEnumerable<ScanResult> inputData)
        {
            // delete part
            var fileNames = inputData.Select(f => f.Name).Distinct();

            var filesToDelete = _unit.FileRepository.GetQueryable()
                .Where(n => fileNames.Contains(n.Name))
                .ToList();

            var idToDelete = filesToDelete.Select(f => f.Id).ToList();
            var wordsToDelete = _unit.WordRepository.GetQueryable()
                .Where(w => idToDelete.Contains(w.FileId))
                .ToList();


            wordsToDelete.ForEach(w => _unit.WordRepository.Delete(w));
            filesToDelete.ForEach(f => _unit.FileRepository.Delete(f));
            _unit.Save();

            // add part
            foreach (var dataItem in inputData)
            {
                var entity = new File()
                {
                    CreatedDate = DateTime.Now,
                    Location = dataItem.Location,
                    Name = dataItem.Name,
                };

                _unit.FileRepository.Insert(entity);
                _unit.Save();

                foreach (var word in dataItem.GetWords())
                {
                    var wordEntity = new Word
                    {
                        CreatedDate = DateTime.Now,
                        Location = word.Location,
                        Text = word.Text,
                        Row = word.Position.Row,
                        Column = word.Position.Column,
                        FileId = entity.Id
                    };

                    _unit.WordRepository.Insert(wordEntity);
                    _unit.Save();
                }
            }
        }

        public IEnumerable<CommonType.Word> WordsInFile(string fileName, string regExp = null)
        {
            var wordListForFile = GetScannedFiles()
                .Where(f => f.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase))
                .Select(f => f.GetWords())
                .ToList();

            var resultCollection = new List<CommonType.Word>();
            foreach (var wordInFile in wordListForFile)
            {
                resultCollection.AddRange(wordInFile);
            }

            return resultCollection;
        }

        public IEnumerable<WordPosition> WordPositionsInFile(ScanResult file, string word)
        {
            return WordPositionsInFile(file.Name, word);
        }

        public IEnumerable<WordPosition> WordPositionsInFile(string fileName, string word)
        {
            var wordsInFile = WordsInFile(fileName, null).Where(f => f.Text.Equals(word, StringComparison.OrdinalIgnoreCase)).ToList();
            var positions = wordsInFile.Select(f => f.Position).ToList();

            return positions;
        }

        public void Dispose()
        {
            _scanResults = null;
            using (_unit) ;
        }
    }
}
