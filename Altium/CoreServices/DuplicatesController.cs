using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace CoreServices
{
    public class DuplicatesController
    {
        private IFilesComparer _comparer;
        public String FolderPath { get; private set; }
        public List<FileInfo> Files { get; private set; }
        public List<FileInfo> LockedFiles { get; private set; }
        public static List<IFilesComparer> AvaliableComparers
        {
            get { return new List<IFilesComparer>() { new Md5HashComparer(), new CustomComparer() }; }
        }

        public DuplicatesController(string folderPath)
        {
            if (String.IsNullOrEmpty(folderPath))
                throw new ArgumentException(nameof(folderPath));

            FolderPath = folderPath;
            Files = new List<FileInfo>();
            LockedFiles = new List<FileInfo>();
            _comparer = AvaliableComparers.First();

            ProcessFiles();
        }

        public void InjectComparer(IFilesComparer comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            _comparer = comparer;
        }

        public List<List<FileInfo>> GetDuplicates()
        {
            return _comparer.GetDuplicates(Files);
        }

        private void ProcessFiles()
        {
            var folderPermission = new FileIOPermission(FileIOPermissionAccess.Read, FolderPath);
            folderPermission.Demand();

            foreach (var fileName in Directory.GetFiles(FolderPath))
            {
                try
                {
                    using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)) ;
                    Files.Add(new FileInfo(fileName));
                }
                catch (IOException)
                {
                    LockedFiles.Add(new FileInfo(fileName));
                }
            }
        }
    }
}
