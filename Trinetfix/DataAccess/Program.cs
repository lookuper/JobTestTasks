using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Program
    {
        static void Main(string[] args)
        {
            // required for manual db data cleanup
            var contextForManualReCreation = new TrinetfixContext();
            var filesRepo = new GenericRepository<File>(contextForManualReCreation);
            var wordRepo = new GenericRepository<Word>(contextForManualReCreation);

            var files = filesRepo.GetQueryable().ToList();
            var words = wordRepo.GetQueryable().ToList();
        }
    }
}
