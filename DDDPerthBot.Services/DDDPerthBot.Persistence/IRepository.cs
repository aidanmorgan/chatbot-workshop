using System;
using System.Linq;
using System.Threading.Tasks;
using DDDPerthBot.Model;

namespace DDDPerthBot.Persistence
{
    public interface IRepository<T> where T: class, IEntity
    {
        IQueryable<T> All();

        T FindById(Guid id);
    }
}
