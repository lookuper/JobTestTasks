using Actum.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actum.DataAccess
{
    public class ActumModel : IDisposable
    {
        /*
            need to map all internal entity types to DTO types (AutoMapper can help) 
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

            var employee = new Employee() { Name = name, Surname = surname, BirthDate = birthdate };
            _unit.EmployeeRepository.Insert(employee);
            _unit.Save();

            return employee;
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

        public void AssignTrainingForEmployee(Training training, Employee employee, DateTime scheduledDate)
        {
            if (training == null)
                throw new ArgumentNullException(nameof(training));

            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            var visit = new TrainingVisit() { Employee = employee, Training = training, When = scheduledDate };
            _unit.TrainingVisitsRepository.Insert(visit);
            _unit.Save();
        }

        public IEnumerable<TrainingVisit> GetLatestTrainings(Employee employee) // better name would be GetTrainingsForEmployee
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            return _unit.TrainingVisitsRepository.GetQueryable()
                .Where(x => x.Employee.Id == employee.Id) // it will be converted into join via enity framework because IQueryable using istead IEnumberable
                .OrderBy(x => x.When)
                .ToList();
        }

        public IEnumerable<TrainingVisit> GetEmployeesAttendTraining(Training training)
        {
            if (training == null)
                throw new ArgumentNullException(nameof(training));

            return _unit.TrainingVisitsRepository.GetQueryable()
                .Where(x => x.Training.Id == training.Id) 
                .OrderBy(x => x.Employee.Name)
                .ThenBy(x => x.Employee.Surname)
                .ToList();
        }

        public void Dispose()
        {
            using (_unit) ;
        }
    }
}
