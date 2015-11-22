using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoreServices
{
    public class Md5HashComparer : IFilesComparer
    {
        private Dictionary<String, List<FileInfo>> dict = new Dictionary<String, List<FileInfo>>();
        private readonly Object _lock = new Object();

        public String Name { get { return "MD5 Hash comparer"; } }

        public List<List<FileInfo>> GetDuplicates(List<FileInfo> filesToCheck)
        {
            if (filesToCheck == null)
                throw new ArgumentException(nameof(filesToCheck));

            Parallel.ForEach(filesToCheck, file =>
            {
                var hash = GetHash(file);
                lock (_lock)
                {
                    if (dict.ContainsKey(hash))
                        dict[hash].Add(file);
                    else
                        dict.Add(hash, new List<FileInfo>() { file });
                }
            });

            var res = new List<List<FileInfo>>();
            foreach (var kv in dict)
            {
                if (kv.Value.Count > 1)
                    res.Add(kv.Value);
            }
            dict.Clear();

            return res;
        }

        private String GetHash(FileInfo file)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(file.FullName))
                {
                    var hash = md5.ComputeHash(stream);
                    var hashString = BitConverter.ToString(hash)
                        .Replace("-", String.Empty)
                        .ToLower();

                    return hashString;
                }
            }
        }
    }
}
