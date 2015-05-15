using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonType
{
    public interface IStorage : IScanner
    {
        void Sync(IEnumerable<ScanResult> scanResult);
    }
}
