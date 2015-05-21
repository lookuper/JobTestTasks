using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TrinetfixInitializer : CreateDatabaseIfNotExists<TrinetfixContext>
    {
        protected override void Seed(TrinetfixContext context)
        {
            var files = new List<File>
            {
                new File {Name="test1.txt", Location="nowhere", CreatedDate = DateTime.Now },
                new File {Name="test2.txt", Location="nowhere", CreatedDate = DateTime.Now },
            };

            files.ForEach(f => context.Files.Add(f));
            context.SaveChanges();

            var words = new List<Word>
            {
                new Word {Text="Hello", Location="nowhere", Row=1, Column=0, FileId=1, CreatedDate = DateTime.Now},
                new Word {Text="World", Location="nowhere", Row=1, Column=6, FileId=1, CreatedDate = DateTime.Now},
            };

            words.ForEach(w => context.Words.Add(w));
            context.SaveChanges();
        }
    }
}
