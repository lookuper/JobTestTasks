using Actum.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actum.DataAccess
{
    public class ActumContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<TrainingVisit> TrainingVisits { get; set; }

        public ActumContext() : base("name=ActumContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
