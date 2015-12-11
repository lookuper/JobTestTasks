using Actum.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actum.DataAccess
{
    public class ActumInitializer : DropCreateDatabaseAlways<ActumContext> // CreateDatabaseIfNotExists<ActumContext>
    {
        protected override void Seed(ActumContext context)
        {
            var users = new List<Employee>
            {
                new Employee() {Name = "Em1", Surname = "Sr1", BirthDate = DateTime.Now },
                new Employee() {Name = "Em2", Surname = "Sr2", BirthDate = DateTime.Now },
            };

            context.Employees.AddRange(users);
            context.SaveChanges();

            var trainings = new List<Training>
            {
                new Training() {Name = "T1", Description = "Training 1" },
                new Training() {Name = "T2", Description = "Training 2" },
            };

            context.Training.AddRange(trainings);
            context.SaveChanges();

            var visits = new List<TrainingVisit>
            {
                new TrainingVisit() {Employee = context.Employees.First(), Training = context.Training.OrderBy(x => x.Id).First(), When = DateTime.Now },
                new TrainingVisit() {Employee = context.Employees.First(), Training = context.Training.OrderByDescending(x => x.Id).First(), When = DateTime.Now }, // never use last with sql
                new TrainingVisit() {Employee = context.Employees.OrderByDescending(x => x.Id).First(), Training = context.Training.OrderBy(x => x.Id).First(), When = DateTime.Now },
                new TrainingVisit() {Employee = context.Employees.OrderByDescending(x => x.Id).First(), Training = context.Training.OrderByDescending(x => x.Id).First(), When = DateTime.Now },
            };
            context.TrainingVisits.AddRange(visits);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
