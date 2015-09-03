using DataAccess.Entity;
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
            var contextForManualReCreation = new DaxxContext();
            var provRepo = new GenericRepository<Province>(contextForManualReCreation);
            var countryRepo = new GenericRepository<Country>(contextForManualReCreation);
            var userRepo = new GenericRepository<User>(contextForManualReCreation);

            var prov = provRepo.GetQueryable().ToList();
            var countries = countryRepo.GetQueryable().ToList();
            var users = userRepo.GetQueryable().ToList();
        }
    }
}
