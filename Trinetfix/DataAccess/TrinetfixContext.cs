using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TrinetfixContext : DbContext
    {
        public TrinetfixContext() : base("name=TrinetfixContext")
        {
            Database.SetInitializer<TrinetfixContext>(new CreateDatabaseIfNotExists<TrinetfixContext>());
        }

        public DbSet<File> Files { get; set; }
        public DbSet<Word> Words { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        [Obsolete]
        public void ClearAndWFillWithTestData()
        {
            Files.RemoveRange(Files);
            Words.RemoveRange(Words);

            var files = new List<File>
            {
                new File {Name="test1.txt", Location="nowhere", CreatedDate = DateTime.Now },
            };

            Files.AddRange(files);
            SaveChanges();

            var insertedFile = Files.First();
            var words = new List<Word>
            {
                new Word {Text="Hello", Location="nowhere", Row=1, Column=0, FileId=insertedFile.Id, CreatedDate = DateTime.Now},
                new Word {Text="World", Location="nowhere", Row=1, Column=6, FileId=insertedFile.Id, CreatedDate = DateTime.Now},
            };

            Words.AddRange(words);
            SaveChanges();
        }

    }
}