using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DaxxInitializer : CreateDatabaseIfNotExists<DaxxContext>
    {
        protected override void Seed(DaxxContext context)
        {
            var countries = new List<Country>
            {
                new Country {Name="CountryA"},
                new Country {Name="CountryB"},
            };

            context.Countries.AddRange(countries);
            context.SaveChanges();

            var provincies = new List<Province>
            {
                new Province {Name = "Province1", CountryId = countries.First().Id},
                new Province {Name = "Province2", CountryId = countries.First().Id },
                new Province {Name = "Province3", CountryId = countries.Last().Id },
            };

            context.Provincies.AddRange(provincies);
            context.SaveChanges();


            var users = new List<User>
            {
                new User {Login="User1", Password="pass1", AgreeToWorkForFood=false, Location = countries.First() },
                new User {Login="User2", Password="pass2", AgreeToWorkForFood=false, Location = countries.Last() },
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
