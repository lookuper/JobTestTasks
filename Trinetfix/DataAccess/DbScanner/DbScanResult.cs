using CommonType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DbScanner
{
    public sealed class DbScanResult : ScanResult
    {
        private UnitOfWork _unit = new UnitOfWork();

        public override long Size { get; set; }

        public override DateTime Created { get; set; }
        public override string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        public override string Location
        {
            get { return base.Location; }
            set { base.Location = value; }
        }

        public DbScanResult() : base("DbScanResult", "Database")
        {

        }

        protected DbScanResult(string name, string location) : base(name, location)
        {
        }

        public override string GetContent(Encoding encoding)
        {
            var textWords = JoinOnFileId().Select(x => x.Text).ToList();
            var joinedString = String.Join(" ", textWords);

            return joinedString;
        }

        public override IEnumerable<CommonType.Word> GetWords(Encoding encoding = null, string regExp = null)
        {
            var wordSequence = JoinOnFileId()
                .Select(w => new CommonType.Word(w.Location, w.Text, w.Row, w.Column))
                .ToList();

            return wordSequence;
        }

        private IEnumerable<Word> JoinOnFileId()
        {
            var fileRecord = _unit.FileRepository.GetQueryable()
                .FirstOrDefault(x => x.Name.Equals(Name, StringComparison.OrdinalIgnoreCase) && x.Location.Equals(Location, StringComparison.OrdinalIgnoreCase));

            if (fileRecord == null)
                throw new ArgumentException("Cannot find such file in database");

            var res = _unit.WordRepository.GetQueryable()
                .Where(x => x.FileId == fileRecord.Id)
                .OrderBy(w => w.Id)
                .ToList();

            return res;
        }


    }
}
