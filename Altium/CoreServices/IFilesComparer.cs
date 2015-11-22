using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreServices
{
    public interface IFilesComparer
    {
        String Name { get; }
        List<List<FileInfo>> GetDuplicates(List<FileInfo> filesToCheck);
    }
}
