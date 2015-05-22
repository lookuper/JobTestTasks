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
        private readonly DbContext _context = new TrinetfixContext();
        private GenericRepository<File> _fileRepo;
        private GenericRepository<Word> _wordRepo;
        private bool _disposed = false;

        public GenericRepository<File> FileRepository
        {
            get
            {
                _fileRepo = _fileRepo ?? new GenericRepository<File>(_context);
                return _fileRepo;
            }
        }

        public GenericRepository<Word> WordRepository
        {
            get
            {
                _wordRepo = _wordRepo ?? new GenericRepository<Word>(_context);
                return _wordRepo;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
