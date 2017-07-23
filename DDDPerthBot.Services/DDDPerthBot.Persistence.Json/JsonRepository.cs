using System;
using System.Collections.Generic;
using System.Linq;
using DDDPerthBot.Model;

namespace DDDPerthBot.Persistence.Json
{
    public class JsonRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly List<T> _values;

        public JsonRepository(List<T> values)
        {
            _values = values;
        }

        public IQueryable<T> All()
        {
            return _values.AsQueryable();
        }

        public T FindById(Guid id)
        {
            return _values.FirstOrDefault(x => x.Id == id);
        }
    }
}
