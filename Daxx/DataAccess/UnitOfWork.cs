using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork : IDisposable
    {
        DbContext context = new DaxxContext();
        GenericRepository<Province> _provinceRepo;
        GenericRepository<Country> _countryRepo;
        GenericRepository<User> _userRepo;
        private bool _disposed = false;

        public GenericRepository<Province> ProvinceRepository
        {
            get
            {
                _provinceRepo = _provinceRepo ?? new GenericRepository<Province>(context);
                return _provinceRepo;
            }
        }

        public GenericRepository<Country> CountryRepository
        {
            get
            {
                _countryRepo = _countryRepo ?? new GenericRepository<Country>(context);
                return _countryRepo;
            }
        }

        public GenericRepository<User> UserRepository
        {
            get
            {
                _userRepo = _userRepo ?? new GenericRepository<User>(context);
                return _userRepo;
            }
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch(ApplicationException ex)
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
