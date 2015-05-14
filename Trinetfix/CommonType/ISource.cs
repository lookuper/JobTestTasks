using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonType
{
    public interface ISource : IScanner
    {
        string Destination { get; }
        string Filter { get; }

        IEnumerable<ScanResult> Scan();
    }
}
