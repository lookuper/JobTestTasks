using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public interface IBikePart
    {
        String Name { get; }
        String Description { get; }
        int Price { get; set; }
    }
}
