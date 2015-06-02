using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonType;


namespace Tests
{
    [TestClass()]
    public class GenericScannerTests
    {
        ISource source = new TestSource();
        IStorage storage = new TestStorage();
        GenericScanner scanner = null;
        string fileName = "t1.txt";

        [TestMethod()]
        public void GenericScannerStorageTest()
        {
            using (scanner = new GenericScanner(null, storage, 5))
            {
                var files = scanner.GetScannedFiles().ToList();
                var words = scanner.WordsInFile("t2.txt").ToList();
                var positions = scanner.WordPositionsInFile("t2.txt", "test").ToList();

                Assert.IsTrue(files.Count > 0);
                Assert.IsTrue(words.Count > 0);
                Assert.IsTrue(positions.Count == 0);
            }
        }

        [TestMethod]
        public void GenericScannerWordPositionsTest()
        {
            using (scanner = new GenericScanner(source, storage, 5))
            {
                var res = scanner.WordPositionsInFile("t2.txt", "Alice");

                Assert.IsTrue(res.Count() != 0);
                Assert.IsTrue(res.First().Row == 1 && res.First().Column == 50);
            }
        }
        [TestMethod]
        public void GenericScannerSyncTest()
        {
            using (scanner = new GenericScanner(source, storage, 5))
            {
                var all = scanner.GetScannedFiles().ToList();
                scanner.SyncSourceWithStorage();
            }
        }


        [TestMethod()]
        public void GenericScannerTest()
        {
            using (scanner = new GenericScanner(source, null, 5))
            {
                Assert.IsTrue(scanner.SourceAvaliable);
                Assert.IsFalse(scanner.StorageAvaliable);
            };
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SyncSourceWithStorageTest()
        {
            using (scanner = new GenericScanner(source, null, 5))
            {
                scanner.SyncSourceWithStorage();
            };
        }

        [TestMethod()]
        public void GetScannedFilesTest()
        {
            using (scanner = new GenericScanner(source, null, 5))
            {
                Assert.IsTrue(scanner.GetScannedFiles().Count() > 0);
                Assert.IsTrue(scanner.GetScannedFiles().GetDataFromPage(1).Count() == 1);
            };
        }

        [TestMethod()]
        public void WordPositionsInFileTest()
        {
            using (scanner = new GenericScanner(source, null, 5))
            {
                var result = scanner.WordPositionsInFile(fileName, "Alice");
                Assert.IsNotNull(result);

                result = scanner.WordPositionsInFile(fileName, "ABCDEFJ");
                Assert.IsTrue(result.Count() == 0);
            }
        }

        [TestMethod()]
        public void WordsInFileTest()
        {
            using (scanner = new GenericScanner(source, null, 5))
            {
                var words = scanner.WordsInFile(fileName).ToList();
                Assert.IsNotNull(words);
            };
        }
    }
}