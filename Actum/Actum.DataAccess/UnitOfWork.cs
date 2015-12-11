using Actum.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actum.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private DbContext context = new ActumContext();
        private GenericRepository<Employee> _employeeRepo;
        private GenericRepository<Training> _trainingRepo;
        private GenericRepository<TrainingVisit> _trainingVisitsRepo;
        private bool _disposed = false;

        public GenericRepository<Employee> EmployeeRepository
        {
            get
            {
                _employeeRepo = _employeeRepo ?? new GenericRepository<Employee>(context);
                return _employeeRepo;
            }
        }

        public GenericRepository<Training> TrainingRepository
        {
            get
            {
                _trainingRepo = _trainingRepo ?? new GenericRepository<Training>(context);
                return _trainingRepo;
            }
        }

        public GenericRepository<TrainingVisit> TrainingVisitsRepository
        {
            get
            {
                _trainingVisitsRepo = _trainingVisitsRepo ?? new GenericRepository<TrainingVisit>(context);
                return _trainingVisitsRepo;
            }
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (ApplicationException ex)
            {
                throw;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
