using Actum.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actum.DataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            //var unit = new UnitOfWork();
            //var users = unit.EmployeeRepository.GetQueryable().ToList();
            //var trainings = unit.TrainingRepository.GetQueryable().ToList();
            //var traininsVisits = unit.TrainingVisitsRepository.GetQueryable().ToList();
            //unit.Save();
            var model = new ActumModel();
            var allUsers = model.AvaliableEmployees;
            model.AddEmployee("U1", "S1", DateTime.Now);
            model.AddTraining("T3", "Training -3 ");


            var i = 5;

            using (model) ;
        }
    }

    public class ActumModel : IDisposable
    {
        /*
            need to map all internal entity types to Dto types (AutoMapper can help) 
            user should not work with entity objects, but for development speed will skip this step
        */

        private readonly UnitOfWork _unit = new UnitOfWork();

        public IEnumerable<Employee> AvaliableEmployees
        {
            get { return _unit.EmployeeRepository.GetQueryable().ToList(); }
        }

        public IEnumerable<Training> AvaliableTrainings
        {
            get { return _unit.TrainingRepository.GetQueryable().ToList(); }
        }

        public Employee AddEmployee(string name, string surname, DateTime birthdate)
        { 
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            if (String.IsNullOrEmpty(surname))
                throw new ArgumentException(nameof(surname));

            var user = new Employee() { Name = name, Surname = surname, BirthDate = birthdate };
            _unit.EmployeeRepository.Insert(user);
            _unit.Save();

            return user;
        }

        public Training AddTraining(string name, string description)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            if (String.IsNullOrEmpty(description))
                throw new ArgumentException(nameof(description));

            var training = new Training() { Name = name, Description = description };
            _unit.TrainingRepository.Insert(training);
            _unit.Save();

            return training;
        }

        public void AssignTrainingForUser(Training training, Employee user, DateTime scheduledDate)
        {
            if (training == null)
                throw new ArgumentNullException(nameof(training));

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var visit = new TrainingVisit() { Employee = user, Training = training, When = scheduledDate };
            _unit.TrainingVisitsRepository.Insert(visit);
            _unit.Save();
        }

        public IEnumerable<TrainingVisit> GetLatestTrainings(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            return _unit.TrainingVisitsRepository.GetQueryable()
                .Where(x => x.Employee == employee)
                .OrderBy(x => x.When)
                .ToList();
        }

        public IEnumerable<TrainingVisit> GetEmployeesAttendTraining(Training training)
        {
            if (training == null)
                throw new ArgumentNullException(nameof(training));

            return _unit.TrainingVisitsRepository.GetQueryable()
                .Where(x => x.Training == training)
                .OrderBy(x => x.Employee)
                .ToList();
        }

        public void Dispose()
        {
            using (_unit) ;
        }
    }
}
