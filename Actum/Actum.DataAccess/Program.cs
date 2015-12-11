using Actum.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actum.DataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new ActumModel(); // two users and two training already here because initialized, can be easly switched off
            var allUsers = model.AvaliableEmployees;
            Debug.Assert(allUsers.Count() == 2);

            var oldEmployee = allUsers.First();
            var newEmployee = model.AddEmployee("U1", "S1", DateTime.Now);
            Debug.Assert(model.AvaliableEmployees.Count() == 3);

            var newTraining = model.AddTraining("T3", "Training -3 ");
            model.AssignTrainingForEmployee(newTraining, newEmployee, DateTime.Now);
            model.AssignTrainingForEmployee(newTraining, oldEmployee, DateTime.Now);

            var latestTrainingForNewUser = model.GetLatestTrainings(newEmployee);
            Debug.Assert(latestTrainingForNewUser.Count() == 1);
            Debug.Assert(model.GetLatestTrainings(oldEmployee).Count() == 3);

            var allUsersAttendsToNewTraining = model.GetEmployeesAttendTraining(newTraining);
            Debug.Assert(allUsersAttendsToNewTraining.Count() == 2);

            using (model) ;
        }
    }
}
