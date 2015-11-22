using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreServices
{
    public class CustomComparer : IFilesComparer
    {
        private class UsedLink
        {
            public FileInfo File { get; set; }
            public bool Used { get; set; }
        }

        private readonly int _bytesChunk = sizeof(Int64);

        private bool FilesAreEqual(FileInfo first, FileInfo second)
        {
            if (first.Length != second.Length)
                return false;

            int iterations = (int)Math.Ceiling((double)first.Length / _bytesChunk);

            using (var fs1 = first.OpenRead())
            using (var fs2 = second.OpenRead())
            {
                var one = new byte[_bytesChunk];
                var two = new byte[_bytesChunk];

                for (int i = 0; i < iterations; i++)
                {
                    fs1.Read(one, 0, _bytesChunk);
                    fs2.Read(two, 0, _bytesChunk);

                    if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                        return false;
                }
            }

            return true;
        }

        public String Name { get { return "Custom Int64 chunks comparer"; } }

        public List<List<FileInfo>> GetDuplicates(List<FileInfo> filesToCheck)
        {
            if (filesToCheck == null || !filesToCheck.Any())
                throw new ArgumentException(nameof(filesToCheck));

            var advandecList = filesToCheck.Select(x => new UsedLink() { File = x, Used = false }).ToList();
            var res = new List<List<FileInfo>>();

            for (int i = 0; i < advandecList.Count; i++)
            {
                var elem = advandecList[i];
                if (elem.Used)
                    continue;

                var firstFile = elem.File;
                elem.Used = true;
                var innerList = new List<FileInfo>() { firstFile };

                for (int j = i + 1; j < advandecList.Count; j++)
                {
                    var elemInner = advandecList[j];

                    if (elemInner.Used)
                        continue;

                    var secondFile = elemInner.File;
                    if (FilesAreEqual(firstFile, secondFile))
                    {
                        innerList.Add(secondFile);
                        elemInner.Used = true;
                        continue;
                    }
                }

                res.Add(innerList);
            }

            return res.Where(x => x.Count > 1).ToList();
        }
    }
}
