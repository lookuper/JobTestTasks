using System;
using CommonType;

namespace Tests
{
    internal class TestScanResult : ScanResult
    {
        public DateTime Created { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
    }
}