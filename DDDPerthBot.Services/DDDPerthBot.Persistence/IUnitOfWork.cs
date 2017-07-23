using System;
using System.Collections.Generic;
using System.Text;
using DDDPerthBot.Model;

namespace DDDPerthBot.Persistence
{
    public interface IUnitOfWork
    {
        IRepository<Session> Sessions { get; }

        IRepository<Speaker> Speakers { get; }
    }
}
