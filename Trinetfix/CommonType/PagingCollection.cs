using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonType
{
    public sealed class PagingCollection<T> : IEnumerable<T>
    {
        private int _pageSize = 100;
        private IEnumerable<T> _collection;

        public int PageSize
        {
            get { return _pageSize; }
            set { if (value <= 0) throw new ArgumentException("value"); _pageSize = value; }
        }

        public int PageCount
        {
            get { return (int)Math.Ceiling(_collection.Count() / (decimal)PageSize); }
        }

        public PagingCollection(IEnumerable<T> source, int pageSize)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            _collection = source;
            PageSize = pageSize;
        }

        public IEnumerable<T> GetDataFromPage(int pageNumber)
        {
            if (pageNumber <= 0 || pageNumber > PageCount)
                return new T[] { };

            int offset = (pageNumber - 1) * PageSize;
            return _collection.Skip(offset).Take(PageSize).ToList();
        }

        public int GetCount(int pageNumber)
        {
            return GetDataFromPage(pageNumber).Count();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
